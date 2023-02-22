using MILAV.API.Device.Layout;

namespace AV_Device_API
{
    public interface ILayoutControl
    {
        ILayout GetLayout();

        void SetLayout(ILayout layout);
    }
}
