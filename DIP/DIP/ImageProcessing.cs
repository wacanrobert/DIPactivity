using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP
{
    internal class ImageProcessing
    {
        Bitmap processed;
        public ImageProcessing()
        {

        }

        public async Task<Bitmap> GreyScale(Bitmap b)
        {
            return await Task.Run(() =>
            {
                processed = new Bitmap(b.Width, b.Height);
                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color pixel = b.GetPixel(x, y);
                        int grey = (pixel.R + pixel.G + pixel.B) / 3;
                        var color = Color.FromArgb(grey, grey, grey);
                        processed.SetPixel(x, y, color);
                    }
                }
                return processed;
            });
        }
        public async Task<Bitmap> BasicCopy(Bitmap b)
        {
            return await Task.Run(() =>
            {
                processed = new Bitmap(b.Width, b.Height);
                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color pixel = b.GetPixel(x, y);
                        processed.SetPixel(x, y, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                    }
                }
                return processed;
            });
        }

        public async Task<Bitmap> ColorInversion(Bitmap b)
        {
            return await Task.Run(() =>
            {
                processed = new Bitmap(b.Width, b.Height);
                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color pixel = b.GetPixel(x, y);
                        processed.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                    }
                }
                return processed;
            });
        }

        public async Task<Bitmap> Sepia(Bitmap bmp)
        {
            return await Task.Run(() =>
            {
                processed = new Bitmap(bmp.Width, bmp.Height);
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color pixel = bmp.GetPixel(x, y);

                        int a = pixel.A;
                        int r = pixel.R;
                        int g = pixel.G;
                        int b = pixel.B;

                        int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                        int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                        int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                        if (tr > 255)
                            r = 255;
                        else
                            r = tr;

                        if (tg > 255)
                            g = 255;
                        else
                            g = tg;

                        if (tb > 255)
                            b = 255;
                        else
                            b = tb;

                        processed.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                    }
                }
                return processed;
            });
        }
        public async Task<Bitmap> Histogram(Bitmap b)
        {
            return await Task.Run(() =>
            {
                Bitmap bmp;
                processed = new Bitmap(b.Width, b.Height);
                Color sample;
                Color grey;
                Byte greydata;

                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        sample = b.GetPixel(x, y);
                        greydata = (byte)((sample.R + sample.G + sample.B) / 3);
                        grey = Color.FromArgb(greydata, greydata, greydata);
                        processed.SetPixel(x, y, grey);
                    }
                }

                int[] histdata = new int[256]; 
                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        sample = b.GetPixel(x, y);
                        histdata[sample.R]++; /
                    }
                }

                bmp = new Bitmap(256, 800);
                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 800; y++)
                    {
                        bmp.SetPixel(x, y, Color.White);
                    }
                }

                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < Math.Min(histdata[x] / 5, bmp.Height - 1); y++)
                    {
                        bmp.SetPixel(x, (bmp.Height - 1) - y, Color.Black);
                    }
                }
                return bmp;
            });
        }
    }
}
