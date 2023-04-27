using Renci.SshNet;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace MILAV.API.Connection
{
    internal class SSHConnection : NetworkConnection
    {
        private readonly SshClient client;

        private ConcurrentQueue<string> responses = new ConcurrentQueue<string>();

        public SSHConnection(string ip, int port, string username, string password) : base(ip, port)
        {
            client = new SshClient(ip, port, username, password);
        }

        public override byte[] ReadBytes(int maxLength = 4096)
        {
            if(responses.TryDequeue(out var response))
            {
                return Encoding.ASCII.GetBytes(response);
            }

            throw new Exception("No response gotten back from commands");
        }

        public override string ReadASCII(int maxLength = 4096)
        {
            if (responses.TryDequeue(out var response))
            {
                return response;
            }

            throw new Exception("No response gotten back from commands");
        }

        public override void WriteBytes(byte[] buffer, int offset, int length)
        {
            responses.Enqueue(client.RunCommand(Encoding.ASCII.GetString(buffer, offset, length)).Execute());
        }

        public override void WriteASCII(string text)
        {
            responses.Enqueue(client.RunCommand(text).Execute());
        }

        public override bool Connect()
        {
            client.Connect();

            return client.IsConnected;
        }

        public override void Disconnect()
        {
            client.Disconnect();
        }

        public override void Dispose()
        {
            client.Dispose();
            // Compiler says this is good to have
            GC.SuppressFinalize(this);
        }
    }
}
