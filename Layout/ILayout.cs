using MILAV.Data;

namespace MILAV.API.Layout
{
	public interface ILayout
	{
		int Sections { get; }

		Rectangle[] GetSectionDimensions(int width, int height);
	}
}
