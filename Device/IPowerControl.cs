namespace AV_Device_API
{
    public interface IPowerControl
    {
        void SetPower(bool state);

        void SetPowerOff();

        void SetPowerOn();
    }
}
