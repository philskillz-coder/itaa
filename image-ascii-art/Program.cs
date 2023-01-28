using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
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
        Console.WriteLine("                                          theskz.dev                                          ");

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
            watch.Start();
            using (Image image = Generator.GenerateImage(
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
                using (Stream s = new MemoryStream())
                {
                    image.Save(s, ImageFormat.Png);
                    s.Seek(0, SeekOrigin.Begin);

                    using (Aspose.Imaging.Image im = Aspose.Imaging.Image.Load(s))
                    {
                        PngOptions o = new PngOptions();
                        o.CompressionLevel = 9;
                        im.Save(options.OutputPath, o);
                    }
                }
            }
            watch.Stop();

            Console.WriteLine();
            Console.WriteLine("#############################################################################################");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Image generated in {watch.Elapsed.TotalSeconds.ToString("0.##")} seconds");
            Console.WriteLine($"Image saved as {options.OutputPath}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("#############################################################################################");
        });

        //using (Image image = Generator.GenerateImage(
        //    ImagePath: "C:\\Users\\Philskillz\\PycharmProjects\\image-text-art\\img.png",
        //    Width: 500,
        //    ImageText: "theskz",
        //    FontSize: 10,
        //    CharacterSpacing: 8,
        //    BrightnessLevels: 8,
        //    MinLetters: 1,
        //    Offset: 4
        //    )
        //)
        //{
        //    image.Save(
        //        "C:\\Users\\Philskillz\\source\\repos\\image-ascii-art\\image-ascii-art\\save.png"
        //    );
        //}


    }
}