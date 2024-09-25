using MAVF.API.Layout;

namespace MAVF.API.Device.Video
{
	public interface ILayoutControl
	{
		public ILayout GetLayout();

		public void SetLayout(ILayout layout);
	}
}
