using MAVF.API.Device.Driver.Routing;

namespace MAVF.API.Device.Driver.DSP
{
	public interface IDSPControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{
	}
}
