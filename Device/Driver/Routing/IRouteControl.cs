﻿namespace MAVF.API.Device.Driver.Routing
{
	public interface IRouteControl<I, O> where I : IInputOutput where O : IInputOutput
	{
		public Dictionary<string, I> Inputs { get; }

		public Dictionary<string, O> Outputs { get; }

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
			return Routes.GetValueOrDefault(output);
		}
	}
}
