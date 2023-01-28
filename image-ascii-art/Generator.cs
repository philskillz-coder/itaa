using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace image_ascii_art;

[SupportedOSPlatform("windows")]
internal class Generator
{
    public static Bitmap resizeImage(Bitmap imgToResize, Size size)
    {
        return new Bitmap(imgToResize, size);
    }

    public static Bitmap MakeGrayscale3(Bitmap original)
    {
        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        using (Graphics g = Graphics.FromImage(newBitmap))
        {
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
               });

            using (ImageAttributes attributes = new ImageAttributes())
            {
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(
                    original, new Rectangle(0, 0, original.Width, original.Height),
                            0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
        }
        return newBitmap;
    }

    public static int mirror(int max, int n)
    {
        return -n + max - 1;
    }

    public static int chunk(int chunks, int n)
    {
        return (int)(n / (256.0 / chunks));
    }

    public static int zero_if_negative(int n)
    {
        if (n < 0) return 0;
        return n;
    }

    public static Image GenerateImage(
        string ImagePath,
        int? Width,
        string ImageText,
        int FontSize,
        int CharacterSpacing,
        int BrightnessLevels,
        int MinLetters,
        int Offset
    )
    {
        Bitmap original = (Bitmap) Image.FromFile(ImagePath);
        int ResizedWidth = Width ?? original.Width;

        float w_pc = (float) ResizedWidth / original.Width;
        int ResizedHeight = (int) Math.Floor(original.Height * w_pc);

        int NewWidth = ResizedWidth * CharacterSpacing;
        int NewHeight = ResizedHeight * CharacterSpacing;

        var b = new Bitmap(1, 1);
        b.SetPixel(0, 0, Color.White);

        Bitmap @new = new Bitmap(NewWidth, NewHeight);
        for (int y = 0; y < NewHeight; y++)
        {
            for (int x = 0; x < NewWidth; x++)
            {
                @new.SetPixel(x, y, Color.White);
            }
        }

        var size = new Size(ResizedWidth, ResizedHeight);
        original = resizeImage(original, size);

        original = MakeGrayscale3(original);

        using Graphics graphics = Graphics.FromImage(@new);
        using Font font = new Font("arial.ttf", FontSize);

        int current_character = 0;
        for (int y = 0; y < ResizedHeight; y++)
        {
            for (int x = 0; x < ResizedWidth; x++)
            { 
                Color pixel = original.GetPixel(x, y);
                int letter_count = mirror(BrightnessLevels, chunk(BrightnessLevels, (int) (pixel.GetBrightness()*255))) + MinLetters;
                int offset_bounds = Offset * letter_count;
                string current_char = ImageText[current_character].ToString();

                if (letter_count == 1)
                {
                    graphics.DrawString(
                        current_char,
                        font,
                        Brushes.Black,
                        new Point(
                            x * CharacterSpacing,
                            y * CharacterSpacing
                        )
                    );
                } else
                {
                    for (int i = 0; i < letter_count; i++)
                    {
                        Random rand0 = new Random();
                        Random rand1 = new Random();
                        int offset_x = (rand0.Next(2 * offset_bounds) - offset_bounds) / 2;
                        int offset_y = (rand1.Next(2 * offset_bounds) - offset_bounds) / 2;

                        graphics.DrawString(
                            current_char,
                            font,
                            new SolidBrush(Color.Black),
                            new Point(
                                zero_if_negative(x * CharacterSpacing + offset_x),
                                zero_if_negative(y * CharacterSpacing + offset_y)
                            )
                        );
                    }
                }
                current_character++;
                current_character %= ImageText.Length;

            }
        }
        

        return @new;
    }

}
