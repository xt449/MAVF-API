namespace AV_Device_API
{
    public interface IPowerControl
    {
        bool GetPower();

        void SetPower(bool state);
    }
}
