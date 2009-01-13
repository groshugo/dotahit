using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BitmapUtils
{
	/// <summary>
	/// Provides flood fill utilities.
	/// </summary>
	public class FloodFillUtils
	{
        /// <summary>
        /// Returns a region that will flood fill the area indicated by the
        /// specified starting point.
        /// </summary>
        /// <param name="bmp">Bitmap for which to calculate the flood fill.</param>
        /// <param name="fillX">X seed position for the flood fill.</param>
        /// <param name="fillY">Y seed position for the flood fill.</param>
        /// <returns>Region representing the flood fill.</returns>
        public static Region GetRegionForFloodFill(Bitmap bmp, int fillX, int fillY)
        {
            Bitmap fillOne = FloodFill(bmp, fillX, fillY, Color.White);
            Bitmap fillTwo = FloodFill(bmp, fillX, fillY, Color.Black);

            Region outRegion = null;

            for (int line = 0; line < bmp.Height; ++line)
            {
                bool inRegion = false;
                int regionStart = 0;
                for (int xpos = 0; xpos < bmp.Width; ++xpos)
                {
                    // This is rather slow, even for fairly small bitmaps.
                    // If that's a problem, it'd be better to rewrite this
                    // to use unsafe code to read the pixel data directly.

                    Color c1 = fillOne.GetPixel(xpos, line);
                    Color c2 = fillTwo.GetPixel(xpos, line);
                    bool sameColour = c1 == c2;
                    if (inRegion)
                    {
                        if (sameColour || (xpos == (bmp.Width - 1)))
                        {
                            // We've hit the end of a sequence of one or
                            // more different pixel values, so it's time to
                            // add the corresponding rectangle to the region.

                            Rectangle rect = new Rectangle(regionStart, line, xpos - regionStart, 1);
                            if (outRegion == null)
                            {
                                outRegion = new Region(rect);
                            }
                            else
                            {
                                outRegion.Union(rect);
                            }
                            inRegion = false;
                        }
                    } // if (inRegion)
                    else
                    {
                        if (!sameColour)
                        {
                            regionStart = xpos;
                            inRegion = true;
                        }
                    }
                }
            }
            return outRegion;
        }        


        private static uint ColorRef(Color c)
        {
            return (uint) ((c.B << 16) + (c.G << 8) + c.R);
        }

        /// <summary>
        /// Returns a copy of the given bitmap with a flood fill
        /// performed from the specified starting point.
        /// </summary>
        /// <param name="bmp">Bitmap to copy and fill.</param>
        /// <param name="x">Fill seed start point X coordinate.</param>
        /// <param name="y">Fill seed start point Y coordinate.</param>
        /// <param name="fillColour">Colour in which to do fill.</param>
        /// <returns>Copy of bitmap with specified flood fill perrformed.</returns>
        public static Bitmap FloodFill(Bitmap bmp, int x, int y, Color fillColour)
        {
            Color startColour = bmp.GetPixel(x, y);
            uint startColorRef = ColorRef(startColour);
            uint fillColorRef = ColorRef(fillColour);

            // Curiously, we can't use the normal technique of
            // getting a DC onto the Bitmap via a Graphics
            // object. It turns out that the existing contents
            // of the Bitmap are invisible to the resulting DC.
            // As far as the DC is concerned the entire surface
            // is a uniform colour, so flood fills just fill the whole
            // bitmap...
            //
            // So instead we have to build a new Bitmap, and
            // then copy the old one into it via the DC using BitBlt.
            // That makes the bitmap's contents visible to the FloodFill
            // API.

            Bitmap outBmp = new Bitmap(bmp.Width, bmp.Height);

            using (Graphics g = Graphics.FromImage(outBmp))
            {                
                IntPtr hDC = g.GetHdc();
                try
                {
                    // Copy input bmp into new bmp, but using the GDI32 API,
                    // so that other GDI32 APIs can see the results.

                    using (GdiDC sourceDC = new GdiDC(Gdi.CreateCompatibleDC(hDC)))
                    {
                        using (GdiObject hBmp = GdiObject.Select(bmp.GetHbitmap(), sourceDC))
                        {
                            Gdi.BitBlt(hDC, 0, 0, bmp.Width, bmp.Height, sourceDC, 0, 0, TernaryRasterOperations.SRCCOPY);
                        }
                    }


                    // Do the flood fill...

                    using (GdiObject hFillBrush = GdiObject.Select(Gdi.CreateSolidBrush(fillColorRef), hDC))
                    {
                        bool rc = Gdi.ExtFloodFill(hDC, x, y, startColorRef, Gdi.FLOODFILLSURFACE);
                        if (!rc)
                        {
                            Debug.WriteLine("Error in flood fill: " + Marshal.GetLastWin32Error());
                        }
                    }
                }
                finally
                {
                    g.ReleaseHdc(hDC);
                }
            }

            return outBmp;
        }        
    }
}
