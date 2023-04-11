using MILAV.API.Device;

namespace MILAV.API.Room
{
    [Obsolete("Rooms are just labels")]
    public interface IRoom
    {
        string Id { get; }
        string Name { get; }

        bool TryGetDevice(string id, out AbstractDevice device);

        AbstractDevice GetDevice(string id);
    }
}
