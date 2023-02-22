using MILAV.API.Device.Layout;

namespace MILAV.API.Device
{
    public interface ILayoutControl
    {
        ILayout GetLayout();

        void SetLayout(ILayout layout);
    }
}
