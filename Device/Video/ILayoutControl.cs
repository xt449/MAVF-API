using MILAV.API.Layout;

namespace MILAV.API.Device.Video
{
    public interface ILayoutControl
    {
        ILayout GetLayout();

        void SetLayout(ILayout layout);
    }
}
