using MILAV.Data;

namespace MILAV.API.Device.Layout
{
    public interface ILayout
    {
        int Sections { get; }

        Rectangle[] GetSectionDimensions(int width, int height);
    }
}
