namespace MAVF.API.Layout
{
	public class LayoutRegistry
	{
		private readonly Dictionary<string, ILayout> layouts = new Dictionary<string, ILayout>();

		public LayoutRegistry()
		{
			layouts.Add("full", SingleLayout.Instance);
			layouts.Add("quad", QuadLayout.Instance);
			layouts.Add("six_left", SixBigTopLeftLayout.Instance);
			layouts.Add("six_right", SixBigTopRightLayout.Instance);
			layouts.Add("nine", NineLayout.Instance);
		}

		public bool Add(string name, ILayout layout)
		{
			if (layouts.ContainsKey(name))
			{
				return false;
			}

			layouts.Add(name, layout);
			return true;
		}

		public bool TryGet(string layout, out ILayout o)
		{
			return layouts.TryGetValue(layout, out o);
		}
	}
}
