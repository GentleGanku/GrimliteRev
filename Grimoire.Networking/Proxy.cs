using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Networking
{
    public class Proxy
    {
        public delegate void Receive(Message message);

        private readonly List<IJsonMessageHandler> _handlersJson;

        private readonly List<IXtMessageHandler> _handlersXt;

        private readonly List<IXmlMessageHandler> _handlersXml;

        public static readonly CancellationTokenSource AppClosingToken;

        public bool _catchXtPackets { get; set; } = false;

        public bool _catchAllPackets { get; set; } = false;

        public static string serverIP { get; set; } = AutoRelogin.ServerIP;

        public static Proxy Instance
        {
            get;
            set;
        }

        public int ListenerPort
        {
            get;
            set;
        }

        public static event Receive ReceivedFromServer;

        private Proxy()
        {
            _handlersJson = new List<IJsonMessageHandler>
            {
                new HandlerDropItem(),
                new HandlerGetQuests(),
                new HandlerQuestComplete(),
                new HandlerLoadShop()
            };
            _handlersXt = new List<IXtMessageHandler>
            {
                new HandlerWarningsXt(),
                new HandlerLogin(),
                new HandlerChat(),
                new HandlerXtJoin()
            };
            _handlersXml = new List<IXmlMessageHandler>
            {
                new HandlerPolicy(),
            };
            ReceivedFromServer += ProcessMessage;
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

        private void RegisterHandler<T>(T handler, List<T> list)
        {
            if (!list.Contains(handler))
            {
                list.Add(handler);
            }
        }

        public async Task SendToServer(string data)
        {
            string text = data.Replace("{ROOM_ID}", World.RoomId.ToString());
            if (text != null && text.Length > 0)
            {
                bool json = data.StartsWith("{");
                World.SendPacket(data, json ? "Json" : "String");
            }
        }

        public async Task SendToClient(string data)
        {
            string text = data;
            if (text != null && text.Length > 0)
            {
                bool json = data.StartsWith("{");
                World.SendClientPacket(data, json ? "json" : "str");
            }
        }

        private void ProcessMessage(Message message)
        {
            try
            {
                switch (message)
                {
                    case XtMessage xtMessage:
                        foreach (IXtMessageHandler item in _handlersXt.Where((IXtMessageHandler h) => h.HandledCommands.Contains(xtMessage.Command)))
                        {
                            item.Handle(xtMessage);
                        }
                        break;

                    case JsonMessage jsonMessage:
                        foreach (IJsonMessageHandler item in _handlersJson.Where((IJsonMessageHandler h) => h.HandledCommands.Contains(jsonMessage.Command)))
                        {
                            item.Handle(jsonMessage);
                        }
                        break;

                    case XmlMessage xmlMessage:
                        foreach (IXmlMessageHandler item in _handlersXml.Where((IXmlMessageHandler h) => h.HandledCommands.Contains(xmlMessage.Command)))
                        {
                            item.Handle(xmlMessage);
                        }
                        break;
                }
            }
            catch { }
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

        static Proxy()
        {
            Instance = new Proxy();
            AppClosingToken = new CancellationTokenSource();
        }

        public async Task ClientExecute()
        {
            while (!AppClosingToken.IsCancellationRequested)
            {
                if (Player.IsLoggedIn)
                {
                    Flash.Call("getRange", new string[0]);
                    if (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.EnableRejection && !World.VisibleDropUI)
                    {
                        Flash.Call("dropUIOpt", false);
                    }
                    if (Root.Instance.rtbPing.Visible)
                    {
                        string sIP = AutoRelogin.ServerIP;
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(sIP);
                        if (reply.RoundtripTime >= 0 && reply.RoundtripTime <= 50)
                        {
                            Root.Instance.rtbPing.ForeColor = System.Drawing.Color.LimeGreen;
                        }
                        else if (reply.RoundtripTime >= 51 && reply.RoundtripTime <= 100)
                        {
                            Root.Instance.rtbPing.ForeColor = System.Drawing.Color.Orange;
                        }
                        else if (reply.RoundtripTime >= 101)
                        {
                            Root.Instance.rtbPing.ForeColor = System.Drawing.Color.Red;
                        }
                        Root.Instance.rtbPing.Clear();
                        Root.Instance.rtbPing.AppendText($"⬤  {reply.RoundtripTime} ms");
                        await Task.Delay(100);
                    }
                }
                else if (Root.Instance.rtbPing.Text != "")
                    Root.Instance.rtbPing.Clear();
                await Task.Delay(100);
            }
        }

        public void LogPackets(string text, string text2)
        {
            Message message = CreateMessage(text2);
            ReceivedFromServer?.Invoke(message);
            if (_catchXtPackets && (text == "xtPacket"))
            {
                PacketLogger.Instance.txtPackets.AppendText(text2 + Environment.NewLine);
            }
            else if (_catchAllPackets && (text == "packet"))
            {
                if (text2.StartsWith("%xt%zm%"))
                {
                    PacketTamperer.Instance.txtReceive.AppendText("From client: " + text2 + Environment.NewLine);
                }
                else
                {
                    PacketTamperer.Instance.txtReceive.AppendText("From server: " + text2 + Environment.NewLine + Environment.NewLine);
                }
            }
        }

    }
}