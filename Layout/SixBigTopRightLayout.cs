using MILAV.Data;

namespace MILAV.API.Layout
{
	public class SixBigTopRightLayout : ILayout
	{
		public static readonly SixBigTopRightLayout Instance = new SixBigTopRightLayout();

		private SixBigTopRightLayout() { }

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
				new Rectangle(thirdWidth, 0, twoThirdWidth, twoThirdHeight),
				new Rectangle(0, thirdHeight, thirdWidth, thirdHeight),
				new Rectangle(0, twoThirdHeight, thirdWidth, thirdHeight),
				new Rectangle(thirdWidth, twoThirdHeight, thirdWidth, thirdHeight),
				new Rectangle(twoThirdWidth, twoThirdHeight, thirdWidth, thirdHeight),
			};
		}
	}
}
