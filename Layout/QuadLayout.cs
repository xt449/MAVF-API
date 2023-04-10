using MILAV.Data;

namespace MILAV.API.Layout
{
    public class QuadLayout : ILayout
    {
        public static readonly QuadLayout Instance = new QuadLayout();

        private QuadLayout() { }

        public int Sections => 4;

        public Rectangle[] GetSectionDimensions(int width, int height)
        {
            var halfWidth = width / 2;
            var halfHeight = height / 2;

            return new Rectangle[]
            {
                new Rectangle(0, 0, halfWidth, halfHeight),
                new Rectangle(halfWidth, 0, halfWidth, halfHeight),
                new Rectangle(0, halfHeight, halfWidth, halfHeight),
                new Rectangle(halfWidth, halfHeight, halfWidth, halfHeight),
            };
        }
    }
}
