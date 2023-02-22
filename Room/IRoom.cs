using MILAV.API.Device;

namespace MILAV.API.Room
{
    public interface IRoom
    {
        string Id { get; }
        string Name { get; }

        bool TryGetDevice(string id, out IDevice device);

        IDevice GetDevice(string id);
    }
}
