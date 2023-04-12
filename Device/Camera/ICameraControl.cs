namespace MILAV.API.Device.Camera
{
    public interface ICameraControl
    {
        void PanUp();

        void PanDown();

        void PanLeft();

        void PanRight();

        void ZoomIn();

        void ZoomOut();

        void OpenLens();

        void CloseLens();
    }
}
