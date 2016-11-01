using System.Drawing;
using System.Linq;
using attestation_2_3_iiasp.Garden_.Etc;

namespace attestation_2_3_iiasp.Garden_
{
    public class PlantImage
    {
        private const float CountY = 3;
        private const int CountX = 5;


        /// <summary>
        /// 236 x 291 - x-y
        /// </summary>
        private const int Width = 236;

        private static readonly float[] OffsetX0 = { 30, 70, 120, 170 };

        private static readonly float[] OffsetX1 = { 25, 75, 120, 180 };

        private static readonly float[] OffsetX2 = { 25, 70, 120, 175 };

        private static readonly float[][] OffsetX = { OffsetX0, OffsetX1, OffsetX2 };


        private static readonly float SizeY;

        private static readonly Image PlantsSprite = Garden_Resources.plants;

        public int RipenessCount { get { return CountX; } }

        static PlantImage()
        {
            var size = PlantsSprite.Size;
            SizeY = size.Height / CountY;
        }

        public void Draw(Graphics graphics, RectangleF dstRect, int ripeness, int plantType)
        {
            Images.Ground.Draw(graphics, dstRect);
            if (ripeness == 0)
                return;
            ripeness--;
            dstRect = Helper.IncreaseX0(dstRect, 10);
            graphics.DrawImage(
                PlantsSprite,
                dstRect,
                GetImageSourceRect(plantType, ripeness),
                GraphicsUnit.Pixel);
        }

        private static float GetImageSourceXOffset(int ripeness, int planetType)
        {
            var offsetX = OffsetX[planetType];
            if (ripeness == 0)
                return 0;
            return offsetX[ripeness - 1];
        }
        private static float GetImageSourceXSize(int ripeness, int planetType)
        {
            var offsetX = OffsetX[planetType];
            if (ripeness == 0)
                return offsetX[0];
            if (ripeness == CountX - 1)
                return Width - offsetX.Last();
            return offsetX[ripeness] - offsetX[ripeness - 1];
        }

        private static RectangleF GetImageSourceRect(int plantType, int ripeness)
        {
            return new RectangleF(new PointF(GetImageSourceXOffset(ripeness, plantType), plantType * SizeY), new SizeF(GetImageSourceXSize(ripeness, plantType), SizeY));
        }
    }

    public class SimpleImage
    {
        private readonly Image _image;

        public SimpleImage(Image image)
        {
            _image = image;
        }

        public void Draw(Graphics graphics, RectangleF rect)
        {
            graphics.DrawImage(_image, rect);
        }
    }

    public static class Images
    {
        public static readonly PlantImage Plant = new PlantImage();

        public static readonly SimpleImage Ground = new SimpleImage(Garden_Resources.ground);
    }


}
