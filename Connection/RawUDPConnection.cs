﻿using System.Net;
using System.Net.Sockets;

namespace MAVF.API.Connection
{
	internal class RawUDPConnection : NetworkConnection
	{
		private readonly UdpClient client;

		private IPEndPoint remoteEndPoint;

		public RawUDPConnection(string ip, int port) : base(ip, port)
		{
			client = new UdpClient();
			remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
		}

		public override bool Connect()
		{
			client.Connect(remoteEndPoint);

			// UDP is best case
			return true;
		}

		public override void Disconnect()
		{
			client.Close();
		}

		public override void Dispose()
		{
			client.Dispose();
		}

		public override byte[] ReadBytes(int maxLength = 4096)
		{
			return client.Receive(ref remoteEndPoint);
		}

		public override void WriteBytes(byte[] buffer, int offset, int length)
		{
			client.Send(buffer, length, remoteEndPoint);
		}
	}
}
