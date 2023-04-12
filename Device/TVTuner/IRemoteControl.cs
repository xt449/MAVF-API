namespace MILAV.API.Device.TVTuner
{
    public interface IRemoteControl : IPowerControl
    {
        public virtual void PowerOn()
        {
            SetPower(true);
        }

        public virtual void PowerOff()
        {
            SetPower(false);
        }

        void ArrowUp();

        void ArrowDown();

        void ArrowLeft();

        void ArrowRight();

        void Enter();

        void Exit();

        void Back();

        void Guide();

        void Menu();

        void ChannelUp();

        void ChannelDown();

        void VolumeUp();

        void VolumeDown();
    }
}
