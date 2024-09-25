using MAVF.API.Device.Routing;

namespace MAVF.API.Device.USB
{
	public interface IUSBControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{

	}
}
