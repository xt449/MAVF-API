using MAVF.API.Device.Routing;

namespace MAVF.API.Device.DSP
{
	public interface IDSPControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{
	}
}
