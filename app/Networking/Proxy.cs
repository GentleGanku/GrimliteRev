using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Networking
{
    public class Proxy
    {
        #region Fields

        public static CancellationTokenSource AppClosingToken;
        private List<IJsonMessageHandler> _handlersJson;
        private List<IXtMessageHandler> _handlersXt;
        private List<IXmlMessageHandler> _handlersXml;

        #endregion

        #region Constructors

        static Proxy()
        {
        }

        private Proxy()
        {
        }

        #endregion

        #region Delegates

        public delegate void Receive(Message message);

        #endregion

        #region Events

        public static event Receive ReceivedFromGame;

        #endregion

        #region Properties

        public static Proxy Instance
        {
            get;
            set;
        }

        public bool _catchXtPackets
        {
            get;
            set;
        } = false;

        public bool _catchAllPackets
        {
            get;
            set;
        } = false;

        public bool firstJoin
        {
            get;
            set;
        } = true;

        #endregion

        #region Methods

        public static async void DisableBotSkill()
        {
            try
            {
                if (BotUtilities.ShouldUseSkill)
                {
                    await Task.Delay(10);
                    BotUtilities.ShouldUseSkill = false;
                    Player.CancelAutoAttack();
                    World.GamePopup("Disabled the bot's ability to attack.");
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running Proxy.DisableBotSkill", ex);
            }
        }

        public static async void EnableBotSkill()
        {
            try
            {
                if (!BotUtilities.ShouldUseSkill)
                {
                    await Task.Delay(10);
                    BotUtilities.ShouldUseSkill = true;
                    World.GamePopup("Enabled the bot's ability to attack.");
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running Proxy.EnableBotSkill", ex);
            }
        }

        public static void OnPingLatency(int latency)
        {
            if (Root.Instance.rtbPing.Visible)
            {
                if (latency >= 0 && latency <= 100)
                {
                    Root.Instance.rtbPing.ForeColor = System.Drawing.Color.LimeGreen;
                }
                else if (latency >= 101 && latency <= 200)
                {
                    Root.Instance.rtbPing.ForeColor = System.Drawing.Color.Orange;
                }
                else if (latency >= 201)
                {
                    Root.Instance.rtbPing.ForeColor = System.Drawing.Color.Red;
                }
                Root.Instance.rtbPing.Clear();
                Root.Instance.rtbPing.AppendText($"⬤  {latency} ms");
            }
        }

        public void RegisterHandler(IJsonMessageHandler handler)
        {
            RegisterHandler(handler, _handlersJson);
        }

        public void RegisterHandler(IXmlMessageHandler handler)
        {
            RegisterHandler(handler, _handlersXml);
        }

        public void RegisterHandler(IXtMessageHandler handler)
        {
            RegisterHandler(handler, _handlersXt);
        }

        public void UnregisterHandler(IJsonMessageHandler handler)
        {
            _handlersJson.Remove(handler);
        }

        public void UnregisterHandler(IXmlMessageHandler handler)
        {
            _handlersXml.Remove(handler);
        }

        public void UnregisterHandler(IXtMessageHandler handler)
        {
            _handlersXt.Remove(handler);
        }

        public async Task SendToServer(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }
            string packet = data.Replace("{ROOM_ID}", World.RoomId.ToString());
            bool json = packet.StartsWith("{");
            World.SendPacket(packet, json ? "Json" : "String");
        }

        public async Task SendToClient(string data)
        {
            string packet = data;
            if (packet != null && packet.Length > 0)
            {
                bool json = data.StartsWith("{");
                World.SendClientPacket(data, json ? "json" : "str");
            }
        }

        public async Task ClientExecute()
        {
            while (!AppClosingToken.IsCancellationRequested)
            {
                if (Player.IsLoggedIn)
                {
                    if (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.EnableRejection && !World.VisibleDropUI)
                    {
                        Flash.Call("dropUIOpt", false);
                    }
                }
                else if (Root.Instance.rtbPing.Text != "")
                    Root.Instance.rtbPing.Clear();
                await Task.Delay(100);
            }
        }

        public void LogPackets(string text, string text2)
        {
            switch (text)
            {
                case "packet":
                    ReceivedFromGame.Invoke(CreateMessage(text2));
                    if (text2.Contains("<msg t='sys'><body action='logout' r='0'></body></msg>"))
                    {
                        LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Safely logged out from the game.\r\n");
                        Player.IsLoggedIn = false;
                    }
                    if (_catchAllPackets)
                    {
                        PacketTamperer.Instance.txtReceive.AppendText((text2.StartsWith("%xt%zm%") ? "From client: " : "From server: ") + text2 + "\r\n" + (text2.StartsWith("%xt%zm%") ? "" : "\r\n"));
                    }
                    break;
                case "xtPacket":
                    if (_catchXtPackets)
                    {
                        PacketLogger.Instance.txtPackets.AppendText(text2 + "\r\n");
                    }
                    break;
                case "ping":
                    if (int.TryParse(text2, out int latency))
                    {
                        OnPingLatency(latency);
                    }
                    break;
                case "chatCommand":
                    ChatCommand.ExecuteCommand(text2);
                    break;
                case "openWebsite":
                    SafeLinks.Open(text2);
                    break;
            }
        }

        internal protected static void InitializeProxyHandlers()
        {
            Instance = new Proxy();
            AppClosingToken = new CancellationTokenSource();
            Instance._handlersJson = new List<IJsonMessageHandler>
            {
                new HandlerDrops(),
                new HandlerGetQuests(),
                new HandlerQuestComplete(),
                new HandlerLoadShop(),
                new HandlerBuyItem(),
                new HandlerSkills(),
                new HandlerArea(),
                new HandlerBotSkills()
            };
            Instance._handlersXt = new List<IXtMessageHandler>
            {
                new HandlerAFK(),
                new HandlerWarningsXt(),
                new HandlerLogin(),
                new HandlerChat(),
                new HandlerXtJoin(),
                new HandlerXtCellJoin(),
                new HandlerConnection()
            };
            Instance._handlersXml = new List<IXmlMessageHandler>
            {
            };
            ReceivedFromGame += Instance.ProcessMessage;
        }

        private void RegisterHandler<T>(T handler, List<T> list)
        {
            if (!list.Contains(handler))
            {
                list.Add(handler);
            }
        }

        // Each handler runs on its own so one failure doesn't skip the handlers after it (plugins included), and the
        // lists are copied first because options and plugins register and unregister handlers at runtime.
        private void ProcessMessage(Message message)
        {
            switch (message)
            {
                case XtMessage xtMessage:
                    foreach (IXtMessageHandler item in Instance._handlersXt.ToArray().Where(h => h != null && h.HandledCommands.Contains(xtMessage.Command)))
                    {
                        Dispatch(item, xtMessage.Command, () => item.Handle(xtMessage));
                    }
                    break;

                case JsonMessage jsonMessage:
                    foreach (IJsonMessageHandler item in Instance._handlersJson.ToArray().Where(h => h != null && h.HandledCommands.Contains(jsonMessage.Command)))
                    {
                        Dispatch(item, jsonMessage.Command, () => item.Handle(jsonMessage));
                    }
                    break;

                case XmlMessage xmlMessage:
                    foreach (IXmlMessageHandler item in Instance._handlersXml.ToArray().Where(h => h != null && h.HandledCommands.Contains(xmlMessage.Command)))
                    {
                        Dispatch(item, xmlMessage.Command, () => item.Handle(xmlMessage));
                    }
                    break;
            }
        }

        private static void Dispatch(object handler, string command, Action handle)
        {
            try
            {
                handle();
            }
            catch (Exception ex)
            {
                ErrorReporter.Log($"handling the '{command}' packet in {handler.GetType().Name}", ex);
            }
        }

        private Message CreateMessage(string raw)
        {
            if (raw != null && raw.Length > 0)
            {
                switch (raw[0])
                {
                    case '<':
                        return new XmlMessage(raw);

                    case '%':
                        return new XtMessage(raw);

                    case '{':
                        return new JsonMessage(raw);
                }
            }
            return null;
        }

        #endregion
    }
}