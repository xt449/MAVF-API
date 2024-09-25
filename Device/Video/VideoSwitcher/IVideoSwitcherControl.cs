using MAVF.API.Device.Routing;

namespace MAVF.API.Device.Video.VideoSwitcher
{
	[Obsolete("Unimplemented")]
	public interface IVideoSwitcherControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{
	}
}
