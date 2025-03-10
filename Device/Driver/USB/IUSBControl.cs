using MAVF.API.Device.Driver.Routing;

namespace MAVF.API.Device.Driver.USB
{
	public interface IUSBControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{

	}
}
