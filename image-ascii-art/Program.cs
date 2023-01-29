using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using Aspose.Imaging.FileFormats.Core.VectorPaths;
using Aspose.Imaging.ImageOptions;
using CommandLine;
using static image_ascii_art.Program;

namespace image_ascii_art;

class Program
{
    public class Options
    {
        [Option('i', "image", Required = true, HelpText = "The image to process")]
        public string ImagePath { get; set; }

        [Option('o', "output", Required = true, HelpText = "The output path of the image")]
        public string OutputPath { get; set; }

        [Option('t', "text", Required = false, Default = "theskz4evr", HelpText = "The text to write on the image")]
        public string ImageText { get; set; }

        [Option("resized-width", Required = false, HelpText = "The width to resize the image before processing")]
        public int? ResizedWidth { get; set; }

        [Option("font-size", Required = false, Default = 10, HelpText = "The text font size")]
        public int FontSize { get; set; }

        [Option("character-spacing", Required = false, Default = 8, HelpText = "How many pixels to space the characters apart")]
        public int CharacterSpacing { get; set; }

        [Option("brightness-levels", Required = false, Default = 8, HelpText = "How many levels of brightness there are")]
        public int BrightnessLevels { get; set; }

        [Option("min-letters", Required = false, Default = 1, HelpText = "The minimum number of letters per pixel")]
        public int MinLetters { get; set; }

        [Option("offset", Required = false, Default = 2, HelpText = "The offset (in pixels) per letter")]
        public int Offset { get; set; }

    }

    public static Dictionary<int, string> Units = new Dictionary<int, string>()
    {
        {0, "" },
        {1, "" },
        {2, "" },
        {3, "K" },
        {4, "K" },
        {5, "K" },
        {6, "M" },
        {7, "M" },
        {8, "M" },
        {9, "B" },
        {10, "B" },
        {11, "B" },
        {12, "T" }
    };

    public static string KiloFormat(int num)
    {
        if (num >= 100_000_000)
            return (num / 1000000).ToString("#,0M");

        if (num >= 10_000_000)
            return (num / 1000000).ToString("0.#") + "M";

        if (num >= 100_000)
            return (num / 1000).ToString("#,0K");

        if (num >= 10_000)
            return (num / 1000).ToString("0.#") + "K";

        return num.ToString("#,0");
    }

    [SupportedOSPlatform("windows")]
    static void Main(string[] args)
    {

        Console.ForegroundColor = ConsoleColor.Blue;

        Console.WriteLine("IIIIIIIIIITTTTTTTTTTTTTTTTTTTTTTT         AAA                              AAA               ");
        Console.WriteLine("I::::::::IT:::::::::::::::::::::T        A:::A                            A:::A              ");
        Console.WriteLine("I::::::::IT:::::::::::::::::::::T       A:::::A                          A:::::A             ");
        Console.WriteLine("II::::::IIT:::::TT:::::::TT:::::T      A:::::::A                        A:::::::A            ");
        Console.WriteLine("  I::::I  TTTTTT  T:::::T  TTTTTT     A:::::::::A                      A:::::::::A           ");
        Console.WriteLine("  I::::I          T:::::T            A:::::A:::::A                    A:::::A:::::A          ");
        Console.WriteLine("  I::::I          T:::::T           A:::::A A:::::A                  A:::::A A:::::A         ");
        Console.WriteLine("  I::::I          T:::::T          A:::::A   A:::::A                A:::::A   A:::::A        ");
        Console.WriteLine("  I::::I          T:::::T         A:::::A     A:::::A              A:::::A     A:::::A       ");
        Console.WriteLine("  I::::I          T:::::T        A:::::AAAAAAAAA:::::A            A:::::AAAAAAAAA:::::A      ");
        Console.WriteLine("  I::::I          T:::::T       A:::::::::::::::::::::A          A:::::::::::::::::::::A     ");
        Console.WriteLine("  I::::I          T:::::T      A:::::AAAAAAAAAAAAA:::::A        A:::::AAAAAAAAAAAAA:::::A    ");
        Console.WriteLine("II::::::II      TT:::::::TT   A:::::A             A:::::A      A:::::A             A:::::A   ");
        Console.WriteLine("I::::::::I      T:::::::::T  A:::::A               A:::::A    A:::::A               A:::::A  ");
        Console.WriteLine("I::::::::I      T:::::::::T A:::::A                 A:::::A  A:::::A                 A:::::A ");
        Console.WriteLine("IIIIIIIIII      TTTTTTTTTTTAAAAAAA                   AAAAAAAAAAAAAA                   AAAAAAA");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("                                  [I]mage [t]o [a]scii [a]rt                                  ");
        Console.WriteLine("                                   Made by Philskillz_#0266                                   ");
        Console.WriteLine("                              github.com/philskillz-coder/itaa                                ");
        Console.WriteLine("                                         theskz.dev                                           ");

        Console.ForegroundColor = ConsoleColor.White;

        Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(options =>
        {

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Selected image: {options.ImagePath}");
            Console.WriteLine($"Using text: {options.ImageText}");
            Console.WriteLine($"Resized image: {options.ResizedWidth.ToString() ?? "<original>"} pixels");
            Console.ForegroundColor = ConsoleColor.White;

            var watch = new System.Diagnostics.Stopwatch();
            Size size;

            watch.Start();
            using (Image generated = Generator.GenerateImage(
                ImagePath: options.ImagePath,
                Width: options.ResizedWidth,
                ImageText: options.ImageText,
                FontSize: options.FontSize,
                CharacterSpacing: options.CharacterSpacing,
                BrightnessLevels: options.BrightnessLevels,
                MinLetters: options.MinLetters,
                Offset: options.Offset
                )
            )
            {
                using (Stream stream = new MemoryStream())
                {
                    size = generated.Size;
                    generated.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);

                    using (Aspose.Imaging.Image compressed = Aspose.Imaging.Image.Load(stream))
                    {
                        PngOptions pngOptions = new PngOptions();
                        pngOptions.CompressionLevel = 9;
                        compressed.Save(options.OutputPath, pngOptions);
                    }
                }
            }
            watch.Stop();

            Console.WriteLine();
            Console.WriteLine("#############################################################################################");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Generated {KiloFormat(size.Height*size.Width)} Pixels in {watch.Elapsed.TotalSeconds.ToString("0.##")} seconds");
            Console.WriteLine($"Image saved as {options.OutputPath}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("#############################################################################################");
        });

    }
}