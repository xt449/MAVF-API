using MILAV.Data;

namespace MILAV.API.Layout
{
    public class SingleLayout : ILayout
    {
        public static readonly SingleLayout Instance = new SingleLayout();

        private SingleLayout() { }

        public int Sections => 1;

        public Rectangle[] GetSectionDimensions(int width, int height)
        {
            return new Rectangle[]
            {
                new Rectangle(0, 0, width, height),
            };
        }
    }
}
