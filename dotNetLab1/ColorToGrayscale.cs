using System.Drawing;

namespace converter
{
    public class ColorToGrayscale
    {

        public static void DiscolorByValueMethod(Bitmap input)
        {
            int value;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixelColor = input.GetPixel(i, j);
                    if (pixelColor.R > pixelColor.G)
                        if (pixelColor.R > pixelColor.B) value = pixelColor.R;
                        else value = pixelColor.B;
                    else
                        if (pixelColor.G > pixelColor.B) value = pixelColor.G;
                    else value = pixelColor.B;
                    Color newColor = Color.FromArgb(pixelColor.A, value, value, value);
                    input.SetPixel(i, j, newColor);
                }
            }
        }

        public static void DiscolorByAverageMethod(Bitmap input)
        {
            int value;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixelColor = input.GetPixel(i, j);
                    value = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color newColor = Color.FromArgb(pixelColor.A, value, value, value);
                    input.SetPixel(i, j, newColor);
                }
            }
        }

        public static void DiscolorByLumaMethod(Bitmap input)
        {
            int value;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixelColor = input.GetPixel(i, j);
                    value = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
                    if (value > 255) value = 255;
                    Color newColor = Color.FromArgb(pixelColor.A, value, value, value);
                    input.SetPixel(i, j, newColor);
                }
            }
        }

    }
}