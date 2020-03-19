using System;
using System.IO;
using System.Drawing;

namespace converter
{
    class Program
    {

        static bool CheckInputPath(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(path + " is incorrect image path.");
                return false;
            }
            if (Path.GetExtension(path) != ".bmp")
            {
                Console.WriteLine(Path.GetFileName(path) + " is not a .bmp file.");
                return false;
            }
            return true;
        }

        static bool CheckOutputPath(string path)
        {
            /*
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Console.WriteLine(path + " is incorrect image path.");
                return false;
            }
            */
            if (Path.GetExtension(path) != ".bmp")
            {
                Console.WriteLine(Path.GetFileName(path) + " is not a .bmp file.");
                return false;
            }
            return true;
        }

        static bool SetPaths(string[] args, ref string inputPath, ref string outputPath)
        {
            switch (args.Length)
            {
                case 0:
                    {
                        Console.WriteLine("Enter the path to the image with BMP image format: ");
                        inputPath = Console.ReadLine();
                        if (!CheckInputPath(inputPath)) return false;

                        Console.WriteLine("Enter the output path: ");
                        outputPath = Console.ReadLine();
                        if (!CheckOutputPath(outputPath))
                        {
                            outputPath = Path.Combine(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath) + "_GrayScale" + Path.GetExtension(inputPath));
                            //outputPath = inputPath.Insert(inputPath.Length - 4, "_GrayScale");
                            Console.WriteLine("The generated output path is : " + outputPath);
                        }
                        break;
                    }
                case 1:
                    {
                        if (!CheckInputPath(args[0])) return false;
                        inputPath = args[0];

                        Console.WriteLine("Enter the output path: ");
                        outputPath = Console.ReadLine();
                        if (!CheckOutputPath(outputPath))
                        {
                            outputPath = Path.Combine(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath) + "_GrayScale" + Path.GetExtension(inputPath));
                            //outputPath = inputPath.Insert(inputPath.Length - 4, "_GrayScale");
                            Console.WriteLine("The generated output path is : " + outputPath);
                        }
                        break;
                    }
                case 2:
                    {
                        if (!CheckInputPath(args[0])) return false;
                        if (!CheckOutputPath(args[1])) return false;

                        inputPath = args[0];
                        outputPath = args[1];
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Application requires lesser parameters than received.");
                        return false;
                    }
            }
            return true;
        }

        static void Stopped()
        {
            Console.WriteLine("Program stopped.");
        }

        static void Main(string[] args)
        {
            string inputPath = null;
            string outputPath = null;

            if (!SetPaths(args, ref inputPath, ref outputPath))
            {
                Stopped();
                return;
            }

            try
            {
                using (Bitmap bitmap = (Bitmap)Image.FromFile(inputPath))
                {
                    Console.WriteLine("Image loaded successfully. Choose one of the conversion methods (type the number):"
                        + Environment.NewLine + "1. Value" + Environment.NewLine + "2. Average" + Environment.NewLine + "3. Luma");
                    int number;
                    try
                    {
                        number = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Number is incorrect.");
                        Stopped();
                        return;
                    }

                    Console.WriteLine("Converting image...");
                    switch (number)
                    {
                        case 1: { ColorToGrayscale.DiscolorByValueMethod(bitmap); break; }
                        case 2: { ColorToGrayscale.DiscolorByAverageMethod(bitmap); break; }
                        case 3: { ColorToGrayscale.DiscolorByLumaMethod(bitmap); break; }
                    }

                    Console.WriteLine("Conversion completed. Saving file...");
                    try
                    {
                        bitmap.Save(outputPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        Console.WriteLine("The modified image is saved." + Environment.NewLine + "Program completed.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Can't save the modified image.");
                        Stopped();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Can't load image.");
                Stopped();
            }
        }
    }
}