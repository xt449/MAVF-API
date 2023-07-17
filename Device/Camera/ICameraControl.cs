namespace MILAV.API.Device.Camera
{
	public interface ICameraControl
	{
		public void PanUp();

		public void PanDown();

		public void PanLeft();

		public void PanRight();

		public void ZoomIn();

		public void ZoomOut();

		public void OpenLens();

		public void CloseLens();
	}
}
