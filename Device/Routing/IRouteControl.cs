namespace MILAV.API.Device.Routing
{
    public interface IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
    {
        public Dictionary<string, I> Inputs { get; }

        public Dictionary<string, O> Outputs { get; }

        public bool Route(I input, O output)
        {
            if (input.Type == output.Type)
            {
                return ExecuteRoute(input, output);
            }

            return false;
        }

        protected bool ExecuteRoute(I input, O output);
    }
}
