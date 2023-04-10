using MILAV.API.Layout;

namespace MILAV.API.Device
{
    public interface ILayoutControl
    {
        ILayout GetLayout();

        void SetLayout(ILayout layout);
    }
}
