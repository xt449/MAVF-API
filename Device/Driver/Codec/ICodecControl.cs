namespace MAVF.API.Device.Driver.Codec
{
	public interface ICodecControl
	{
		public void AddParticipant(string phonebookId);

		public void RemoveParticipant(string phonebookId);

		public void BeginConferenceCall();

		public void EndConferenceCall();

		public void VolumeUp();

		public void VolumeDown();
	}
}
