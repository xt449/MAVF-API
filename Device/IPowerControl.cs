namespace MILAV.API.Device
{
    public interface IPowerControl
    {
        public bool GetPower();

        public void SetPower(bool state);
    }
}
