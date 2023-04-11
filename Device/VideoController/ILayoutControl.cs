using MILAV.API.Layout;

namespace MILAV.API.Device.VideoController
{
    public interface ILayoutControl
    {
        ILayout GetLayout();

        void SetLayout(ILayout layout);
    }
}
