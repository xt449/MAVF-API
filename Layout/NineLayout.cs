using MAVF.Data;

namespace MAVF.API.Layout
{
	public class NineLayout : ILayout
	{
		public static readonly NineLayout Instance = new NineLayout();

		private NineLayout() { }

		public int Sections => 9;

		public Rectangle[] GetSectionDimensions(int width, int height)
		{
			var thirdWidth = width / 3;
			var thirdHeight = height / 3;
			var twoThirdWidth = thirdWidth * 2;
			var twoThirdHeight = thirdHeight * 2;

			return new Rectangle[]
			{
				new Rectangle(0, 0, thirdWidth, thirdHeight),
				new Rectangle(thirdWidth, 0, thirdWidth, thirdHeight),
				new Rectangle(twoThirdWidth, 0, thirdWidth, thirdHeight),

				new Rectangle(0, thirdHeight, thirdWidth, thirdHeight),
				new Rectangle(thirdWidth, thirdHeight, thirdWidth, thirdHeight),
				new Rectangle(twoThirdWidth, thirdHeight, thirdWidth, thirdHeight),

				new Rectangle(0, twoThirdHeight, thirdWidth, thirdHeight),
				new Rectangle(thirdWidth, twoThirdHeight, thirdWidth, thirdHeight),
				new Rectangle(twoThirdWidth, twoThirdHeight, thirdWidth, thirdHeight),
			};
		}
	}
}
