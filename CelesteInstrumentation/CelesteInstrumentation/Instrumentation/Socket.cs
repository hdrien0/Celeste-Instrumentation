using System.Net.Sockets;
using System.Text;

namespace Instrumentation
{
	public static class Socket
	{
		public static void Initialize()
		{
			while (true)
			{
				try
				{
					Socket.Client = new TcpClient(Socket.ConnectionIP, Socket.ConnectionPort);
					return;
				}
				catch
				{
					System.Threading.Thread.Sleep(50);
				}
			}
		}

		public static byte[] SendAndReceiveData(byte[] data)
		{
			NetworkStream stream = Socket.Client.GetStream();
			stream.Write(data, 0, data.Length);
			byte[] array = new byte[Socket.Client.ReceiveBufferSize];
			stream.Read(array, 0, Socket.Client.ReceiveBufferSize);
			return array;
		}

		public static string SendAndReceiveMessage(string message)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(message);
			return Encoding.ASCII.GetString(Socket.SendAndReceiveData(bytes)).Replace("\0", string.Empty);
		}

		private static string ConnectionIP = "127.0.0.1";

		private static int ConnectionPort = 25001;

		private static TcpClient Client;
	}
}
