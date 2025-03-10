using MAVF.API.Device.Driver.Routing;

namespace MAVF.API.Device.Driver.Video.VideoSwitcher
{
	[Obsolete("Unimplemented")]
	public interface IVideoSwitcherControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{
	}
}
