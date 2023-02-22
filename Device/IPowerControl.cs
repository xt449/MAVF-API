namespace MILAV.API.Device
{
    public interface IPowerControl
    {
        bool GetPower();

        void SetPower(bool state);
    }
}
