namespace MILAV.API.Device
{
    public class Output
    {
        public readonly AbstractDevice device;

        public readonly IOType type;

        public readonly string id;

        public readonly int output;

        public readonly string group;

        public Output(AbstractDevice device, IOType type, string group, string id, int output)
        {
            this.device = device;
            this.type = type;
            this.group = group;
            this.id = id;
            this.output = output;
        }
    }
}
