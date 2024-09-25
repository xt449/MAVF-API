using MAVF.Data;

namespace MAVF.API.Layout
{
	public class SixBigBottomLeftLayout : ILayout
	{
		public static readonly SixBigBottomLeftLayout Instance = new SixBigBottomLeftLayout();

		private SixBigBottomLeftLayout() { }

		public int Sections => 6;

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
				new Rectangle(0, thirdHeight, twoThirdWidth, twoThirdHeight),
				new Rectangle(twoThirdWidth, thirdHeight, thirdWidth, thirdHeight),
				new Rectangle(twoThirdWidth, twoThirdHeight, thirdWidth, thirdHeight),
			};
		}
	}
}
