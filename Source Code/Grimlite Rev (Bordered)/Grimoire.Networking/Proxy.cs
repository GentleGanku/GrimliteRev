using Grimoire.Game;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Grimoire.Networking
{
    public class Proxy
    {
        public delegate void Receive(Message message);

        private readonly List<IJsonMessageHandler> _handlersJson;

        private readonly List<IXtMessageHandler> _handlersXt;

        private readonly List<IXmlMessageHandler> _handlersXml;

        private TcpClient _client;

        private TcpClient _server;

        private TcpListener _listener;

        private List<byte> _bufferClient;

        private List<byte> _bufferServer;

        private const int MaxBufferSize = 1024;

        private static readonly CancellationTokenSource AppClosingToken;

        private bool _shouldConnect;

        private bool _policyReceived;

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

        public event Receive ReceivedFromClient;

        public event Receive ReceivedFromServer;

        private Proxy()
        {
            _handlersJson = new List<IJsonMessageHandler>
            {
                new HandlerSkills(),
                new HandlerDPS(),
                new HandlerDropItem(),
                new HandlerGetQuests(),
                new HandlerQuestComplete(),
                //new HandlerMapJoin(),
                //new HandlerLoadBank(),
                new HandlerLoadShop()
            };
            _handlersXt = new List<IXtMessageHandler>
            {
                new HandlerWarningsXt(),
                new HandlerLogin(),
                //new HandlerAFK(),
                new HandlerChat(),
                new HandlerXtJoin(),
                //new HandlerXtCellJoin()
            };
            _handlersXml = new List<IXmlMessageHandler>
            {
                new HandlerPolicy(),
                new HandlerWarningsXml()
            };
            _shouldConnect = true;
            ReceivedFromServer += ProcessMessage;
            ReceivedFromClient += ProcessMessage;
            _bufferClient = new List<byte>();
            _bufferServer = new List<byte>();
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

        public async Task Start()
        {
            if (_listener == null)
            {
                _listener = new TcpListener(IPAddress.Loopback, ListenerPort);
            }
            while (!AppClosingToken.IsCancellationRequested)
            {
                if (_shouldConnect)
                {
                    try
                    {
                        await AcceptAndConnect();
                        _shouldConnect = false;
                    }
                    catch
                    {

                    }
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }

        private async Task AcceptAndConnect()
        {
            _listener.Start();
            _client = await _listener.AcceptTcpClientAsync();
            _server = new TcpClient();
            string text = Flash.Call<string>("RealAddress", new string[0]);
            IPAddress gameServerAddress;
            try
            {
                gameServerAddress = IPAddress.Parse(text);
            }
            catch (Exception)
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(text);
                gameServerAddress = hostEntry.AddressList[0];
            }
            if (_policyReceived)
            {
                await _server.ConnectAsync(gameServerAddress, Flash.Call<int>("RealPort", new string[0]));
            }
            else
            {
                byte[] cbuffer2 = new byte[1024];
                byte[] sbuffer2 = new byte[1024];
                byte[] buffer = cbuffer2;
                cbuffer2 = ReceiveOnce(buffer, await _client.GetStream().ReadAsync(cbuffer2, 0, 1024));
                await _server.ConnectAsync(gameServerAddress, Flash.Call<int>("RealPort", new string[0]));
                await SendToServer(cbuffer2);
                buffer = sbuffer2;
                sbuffer2 = ReceiveOnce(buffer, await _server.GetStream().ReadAsync(sbuffer2, 0, 1024));
                await SendToClient(ModifyDomainPolicy(sbuffer2));
                _client.Close();
                _client = await _listener.AcceptTcpClientAsync();
                _policyReceived = true;
            }
            _listener.Stop();
            Task.Factory.StartNew(ReceiveFromClient, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(ReceiveFromServer, TaskCreationOptions.LongRunning);
        }

        private byte[] ModifyDomainPolicy(byte[] policy)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Encoding.UTF8.GetString(policy));
            xmlDocument["cross-domain-policy"]["allow-access-from"].Attributes["to-ports"].Value = ListenerPort.ToString();
            return Encoding.UTF8.GetBytes(xmlDocument.OuterXml);
        }

        private byte[] ReceiveOnce(byte[] buffer, int read)
        {
            byte[] array = new byte[read];
            Array.Copy(buffer, array, read);
            return array;
        }

        public void Stop(bool appClosing)
        {
            if (!_shouldConnect)
            {
                if (appClosing)
                {
                    AppClosingToken.Cancel();
                }
                _server?.Close();
                _client?.Close();
                _listener.Stop();
                _shouldConnect = true;
            }
        }

        private async Task ReceiveFromClient()
        {
            while (!AppClosingToken.IsCancellationRequested)
            {
                try
                {
                    NetworkStream stream = _client.GetStream();
                    if (!_shouldConnect && stream.CanRead)
                    {
                        byte[] buffer = new byte[1024];
                        int num;
                        int read = num = await stream.ReadAsync(buffer, 0, 1024);
                        if (num == 0)
                        {
                            Stop(appClosing: false);
                            return;
                        }
                        int i = 0;
                        while (true)
                        {
                            int num2 = read - 1;
                            read = num2;
                            if (num2 < 0)
                            {
                                break;
                            }
                            byte b = buffer[i++];
                            if (b != 0)
                            {
                                _bufferClient.Add(b);
                            }
                            else
                            {
                                byte[] bytes = _bufferClient.ToArray();
                                Message message = CreateMessage(Encoding.UTF8.GetString(bytes));
                                this.ReceivedFromClient?.Invoke(message);
                                if (message.Send)
                                {
                                    await SendToServer(message.ToString());
                                }
                                _bufferClient = new List<byte>();
                            }
                        }
                    }
                }
                catch
                {
                    Stop(appClosing: false);
                    return;
                }
            }
        }

        private async Task ReceiveFromServer()
        {
            while (!AppClosingToken.IsCancellationRequested)
            {
                try
                {
                    NetworkStream stream = _server.GetStream();
                    if (!_shouldConnect && stream.CanRead)
                    {
                        byte[] buffer = new byte[1024];
                        int num;
                        int read = num = await stream.ReadAsync(buffer, 0, 1024);
                        if (num == 0)
                        {
                            Stop(appClosing: false);
                            return;
                        }
                        int i = 0;
                        while (true)
                        {
                            int num2 = read - 1;
                            read = num2;
                            if (num2 < 0)
                            {
                                break;
                            }
                            byte b = buffer[i++];
                            if (b != 0)
                            {
                                _bufferServer.Add(b);
                            }
                            else
                            {
                                byte[] bytes = _bufferServer.ToArray();
                                Message message = CreateMessage(Encoding.UTF8.GetString(bytes));
                                this.ReceivedFromServer?.Invoke(message);
                                if (message.Send)
                                {
                                    await SendToClient(message.ToString());
                                }
                                _bufferServer = new List<byte>();
                            }
                        }
                    }
                }
                catch
                {
                    Stop(appClosing: false);
                    return;
                }
            }
        }

        public async Task SendToServer(string data)
        {
            string text = data.Replace("{ROOM_ID}", World.RoomId.ToString());
            if (text != null && text.Length > 0)
            {
                if (text[text.Length - 1] != 0)
                {
                    text += "\0";
                }
                await SendToServer(Encoding.UTF8.GetBytes(text));
            }
        }

        public async Task SendToServer(byte[] data)
        {
            NetworkStream stream = _server.GetStream();
            if (stream.CanWrite)
            {
                try
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }
                catch
                {
                    Stop(appClosing: false);
                }
            }
        }

        public async Task SendToClient(string data)
        {
            string text = data;
            if (text != null && text.Length > 0)
            {
                if (data[data.Length - 1] != 0)
                {
                    data += "\0";
                }
                await SendToClient(Encoding.UTF8.GetBytes(data));
            }
        }

        public async Task SendToClient(byte[] data)
        {
            NetworkStream stream = _client.GetStream();
            if (stream.CanWrite)
            {
                try
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }
                catch
                {
                    Stop(appClosing: false);
                }
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
            catch
            {

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

        static Proxy()
        {
            Instance = new Proxy();
            AppClosingToken = new CancellationTokenSource();
        }
    }
}