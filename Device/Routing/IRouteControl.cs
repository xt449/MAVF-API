namespace MILAV.API.Device.Routing
{
    public interface IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
    {
        public Dictionary<string, I> Inputs { get; }

        public Dictionary<string, O> Outputs { get; }

        protected Dictionary<O, I> routes { get; }

        public bool Route(I input, O output)
        {
            if (input.Type == output.Type)
            {
                var result = ExecuteRoute(input, output);
                if (result)
                {
                    routes[output] = input;
                }
            }

            return false;
        }

        protected bool ExecuteRoute(I input, O output);

        public virtual I? GetRoute(O output)
        {
            return routes[output];
        }
    }
}
