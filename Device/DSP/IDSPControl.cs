using MILAV.API.Device.Routing;

namespace MILAV.API.Device.DSP
{
    public interface IDSPControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
    {
    }
}
