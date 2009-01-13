using System;
using System.Runtime.InteropServices;

namespace BitmapUtils
{
    /// <summary>
    /// Wrapper that frees a GDI object when Dispose is called.
    /// </summary>
    /// <remarks>
    /// This allows the C# using statement to be used to
    /// guarantee that a GDI object is deleted. It can also
    /// optionally select the object into a DC and then select
    /// the old object back in at the end of the using block.
    /// </remarks>
    public struct GdiObject : IDisposable
	{
        private IntPtr obj;
        private IntPtr hDC;
        private IntPtr origObj;

		public GdiObject(IntPtr hObj)
		{
            obj = hObj;
            hDC = origObj = IntPtr.Zero;
        }

        /// <summary>
        /// Temporarily selects the supplied object into the specified DC, freeing
        /// the object when the returned GdiObject is disposed.
        /// </summary>
        /// <param name="hObj">Object to select into DC, and to delete when
        /// the GdiObject is Disposed.</param>
        /// <param name="hDC">DC into which to select the object.</param>
        /// <returns>A GdiObject which, when Disposed, selects back the object
        /// that was previously selected into the DC, and then deletes the object
        /// passed into this method.</returns>
        public static GdiObject Select(IntPtr hObj, IntPtr hDC)
        {
            GdiObject g = new GdiObject(hObj);
            g.hDC = hDC;
            g.origObj = Gdi.SelectObject(hDC, hObj);
            return g;
        }

        public static implicit operator IntPtr(GdiObject wrapper)
        {
            return wrapper.obj;
        }

        public void Dispose()
        {
            if (hDC != IntPtr.Zero)
            {
                Gdi.SelectObject(hDC, origObj);
                hDC = IntPtr.Zero;
            }
            if (obj != IntPtr.Zero)
            {
                Gdi.DeleteObject(obj);
                obj = IntPtr.Zero;
            }
        }
    }
}
