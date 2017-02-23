using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgify
{
    class Program
    {
        private static string _source, _dest;
        private static int _width, _quality;
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Sorry, this app requires both a source file and a destination folder. Please enter them after the app file name.");
            } else if( args.Length == 2)
            {
                try {
                    _source = args[0];
                    _dest = args[1];
                }catch (Exception e)
                {
                    Console.WriteLine("Sorry There is an issue with the file path(s) Provided. Try Quoting the path(s) \n " + e.ToString());
                }
                _width = 300;
                _quality = 75;
                /*Console.WriteLine(_source);
                Console.WriteLine(_dest);
                Console.WriteLine(_width);
                Console.ReadLine();*/
                ReadImgFile(_source, _dest, _width, _quality);
            }else if( args.Length == 3)
            {
                _source = args[0];
                _dest = args[1];
                _width = int.Parse(args[2]);
                _quality = 75;
                /*Console.WriteLine(_source);
                Console.WriteLine(_dest);
                Console.WriteLine(_width);
                Console.WriteLine(_quality);
                Console.ReadLine();*/
                ReadImgFile(_source, _dest, _width, _quality);
            }else if (args.Length == 4)
            {
                _source = args[0];
                _dest = args[1];
                _width = int.Parse(args[2]);
                _quality = int.Parse(args[3]);
                /*Console.WriteLine(_source);
                Console.WriteLine(_dest);
                Console.WriteLine(_width);
                Console.WriteLine(_quality);
                Console.ReadLine();*/
            }
        }

        private static void ReadImgFile(string _sourceLocation, string _destLocation, int _w, int _q)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach(ImageCodecInfo codec in codecs)
            {
                if(codec.MimeType == "image/jpeg")
                {
                    ici = codec;
                }
            }
            EncoderParameters ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)_q);

            Image img = Bitmap.FromFile(_sourceLocation);
            Console.WriteLine(img.Size);
            int h = img.Size.Height, w = img.Size.Width;
            Size nSize = new Size(_w, ((h * _w) / w));
            Console.Write("Working on: " + _sourceLocation + "\nCompressing by a factor of: " + _q + "\nResizing to: " + nSize);
            
            /* Console.WriteLine(nSize);
             Console.ReadLine();*/
            img = ResizeImage(img, nSize);
            try {
                img.Save(_destLocation, ici, ep);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static Image ResizeImage(Image img, Size newSize)
        {
            Image newImg = new Bitmap(newSize.Width, newSize.Height);
            using(Graphics Gfx = Graphics.FromImage((Bitmap)newImg))
            {
                Gfx.DrawImage(img, new Rectangle(Point.Empty, newSize));
            }
            return newImg;

        }
    }
}
