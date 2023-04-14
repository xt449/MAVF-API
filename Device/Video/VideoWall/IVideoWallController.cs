using MILAV.API.Layout;

namespace MILAV.API.Device.Video.VideoWall
{
    public interface IVideoWallController
    {
        public ILayout GetWallLayout(int wall);

        public void SetWallLayout(int wall, ILayout layout);
    }
}
