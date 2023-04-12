namespace MILAV.API.Device
{
    public class Input
    {
        public readonly AbstractDevice device;

        public readonly IOType type;

        public readonly string id;

        public readonly int input;

        public readonly string group;

        public Input(AbstractDevice device, IOType type, string group, string id, int input)
        {
            this.device = device;
            this.type = type;
            this.group = group;
            this.id = id;
            this.input = input;
        }
    }
}
