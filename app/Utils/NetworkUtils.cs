using System.Net;
using System.Net.Sockets;

namespace Grimoire.Utils
{
    public class NetworkUtils
    {
        #region Methods

        public static int GetAvailablePort()
        {
            int port;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                port = ((IPEndPoint)socket.LocalEndPoint).Port;
            }
            return port;
        }

        #endregion
    }
}