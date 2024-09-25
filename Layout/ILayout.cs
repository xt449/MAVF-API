using MAVF.Data;

namespace MAVF.API.Layout
{
	public interface ILayout
	{
		int Sections { get; }

		Rectangle[] GetSectionDimensions(int width, int height);
	}
}
