using MILAV.API.Device.Routing;

namespace MILAV.API.Device.USB
{
    public interface IUSBControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
    {

    }
}
