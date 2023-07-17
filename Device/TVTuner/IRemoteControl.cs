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

		public void ArrowUp();

		public void ArrowDown();

		public void ArrowLeft();

		public void ArrowRight();

		public void Enter();

		public void Exit();

		public void Back();

		public void Guide();

		public void Menu();

		public void ChannelUp();

		public void ChannelDown();

		public void VolumeUp();

		public void VolumeDown();
	}
}
