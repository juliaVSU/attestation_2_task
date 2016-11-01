using System;
using System.Drawing;

namespace attestation_2_3_iiasp.Garden_.Etc
{
    public class Helper
    {
        public static RectangleF ToRectangleF(Size size)
        {
            return new RectangleF(0, 0, size.Width, size.Height);
        }

        public static RectangleF DecreaseBorder(RectangleF rect, float df)
        {
            return new RectangleF(
                rect.X + df,
                rect.Y + df,
                rect.Width - 2 * df,
                rect.Height - 2 * df);
        }


        /// <summary>
        /// inclusive
        /// </summary>
        public static bool Belong(int p, int min, int max)
        {
            return min <= p && p <= max;
        }

        public static Size AddHeight(Size size, int height)
        {
            return new Size(size.Width, size.Height + height);
        }

        public static RectangleF IncreaseX0(RectangleF dstRect, float dx)
        {
            return new RectangleF(
                dstRect.X + dx, dstRect.Y,
                dstRect.Width - dx,
                dstRect.Height
                );
        }

        public static void Invoke(Action action)
        {
            if (action != null)
                action();
        }

        public static void Invoke<TArg1>(Action<TArg1> action, TArg1 arg1)
        {
            if (action != null)
                action(arg1);
        }
        public static void Invoke<TArg1, TArg2>(Action<TArg1, TArg2> action, TArg1 arg1, TArg2 arg2)
        {
            if (action != null)
                action(arg1, arg2);
        }

        public static void ValidateBelong(int p, int p0, int p1)
        {
            if (!Belong(p, p0, p1))
                throw new Exception();
        }
    }
}
