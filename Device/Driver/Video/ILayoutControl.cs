using MAVF.API.Layout;

namespace MAVF.API.Device.Driver.Video
{
	public interface ILayoutControl
	{
		public ILayout GetLayout();

		public void SetLayout(ILayout layout);
	}
}
