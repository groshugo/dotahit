using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BitmapUtils
{
	/// <summary>
	/// Interop imports for GDI32
	/// </summary>
	public class Gdi
	{
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern bool ExtFloodFill(IntPtr hdc, int nXStart, int nYStart,
            uint crColor, uint fuFillType);
        public const uint FLOODFILLBORDER = 0;
        public const uint FLOODFILLSURFACE = 1;
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("user32.dll")]
        public static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);
        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT 
        {
            public int left;
            public int top;
            public int right;
            public int bottom;


            public RECT(int left, int top, int right, int bottom) 
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle Rectangle
            {
                get { return Rectangle.FromLTRB(left, top, right, bottom); }
            }
        }
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        [DllImport("gdi32.dll")]
        public static extern int SetMapMode(IntPtr hdc, int fnMapMode);
        public const int MM_TEXT = 1;
        [DllImport("gdi32.dll")]
        public static extern bool GdiFlush();

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
            int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    }
        public enum TernaryRasterOperations
        {
            SRCCOPY     = 0x00CC0020, /* dest = source*/
            SRCPAINT    = 0x00EE0086, /* dest = source OR dest*/
            SRCAND      = 0x008800C6, /* dest = source AND dest*/
            SRCINVERT   = 0x00660046, /* dest = source XOR dest*/
            SRCERASE    = 0x00440328, /* dest = source AND (NOT dest )*/
            NOTSRCCOPY  = 0x00330008, /* dest = (NOT source)*/
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY   = 0x00C000CA, /* dest = (source AND pattern)*/
            MERGEPAINT  = 0x00BB0226, /* dest = (NOT source) OR dest*/
            PATCOPY     = 0x00F00021, /* dest = pattern*/
            PATPAINT    = 0x00FB0A09, /* dest = DPSnoo*/
            PATINVERT   = 0x005A0049, /* dest = pattern XOR dest*/
            DSTINVERT   = 0x00550009, /* dest = (NOT dest)*/
            BLACKNESS   = 0x00000042, /* dest = BLACK*/
            WHITENESS   = 0x00FF0062, /* dest = WHITE*/
        };
}
