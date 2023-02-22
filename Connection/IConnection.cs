using Newtonsoft.Json;

namespace MILAV.API.Connection
{
    [JsonConverter(typeof(IConnectionConverter))]
    public interface IConnection
    {
        bool Connect();

        byte[] ReadBytes(int maxLength);

        void WriteBytes(byte[] buffer, int offset, int length);
    }
}
