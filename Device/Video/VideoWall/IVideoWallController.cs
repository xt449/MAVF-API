using MILAV.API.Layout;

namespace MILAV.API.Device.Video.VideoWall
{
    public interface IVideoWallController
    {
        ILayout GetWallLayout(int wall);

        void SetWallLayout(int wall, ILayout layout);
    }
}
