using Newtonsoft.Json;

namespace MAVF.API.Device.Routing
{
	public interface IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{
		[JsonProperty("inputs", Required = Required.Always)]
		public Dictionary<string, I> Inputs { get; init; }

		[JsonProperty("outputs", Required = Required.Always)]
		public Dictionary<string, O> Outputs { get; init; }

		protected Dictionary<O, I> Routes { get; }

		public bool Route(I input, O output)
		{
			if (input.Type == output.Type)
			{
				var result = ExecuteRoute(input, output);
				if (result)
				{
					Routes[output] = input;
				}
			}

			return false;
		}

		protected bool ExecuteRoute(I input, O output);

		public virtual I? GetRoute(O output)
		{
			return Routes[output];
		}
	}
}
