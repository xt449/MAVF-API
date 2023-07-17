using MILAV.API.Layout;

namespace MILAV.API.Device.Video
{
	public interface ILayoutControl
	{
		public ILayout GetLayout();

		public void SetLayout(ILayout layout);
	}
}
