namespace MAVF.API.Connection
{
	internal class TelnetConnection : NetworkConnection
	{
		public TelnetConnection(string ip, int port) : base(ip, port)
		{
		}

		public override bool Connect()
		{
			throw new NotImplementedException();
		}

		public override void Disconnect()
		{
			throw new NotImplementedException();
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override byte[] ReadBytes(int maxLength = 4096)
		{
			throw new NotImplementedException();
		}

		public override void WriteBytes(byte[] buffer, int offset, int length)
		{
			throw new NotImplementedException();
		}
	}
}
