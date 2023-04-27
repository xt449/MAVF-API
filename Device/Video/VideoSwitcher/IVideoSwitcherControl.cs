using MILAV.API.Device.Routing;

namespace MILAV.API.Device.Video.VideoSwitcher
{
    [Obsolete("Unimplemented")]
    public interface IVideoSwitcherControl<I, O> : IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
    {
    }
}
