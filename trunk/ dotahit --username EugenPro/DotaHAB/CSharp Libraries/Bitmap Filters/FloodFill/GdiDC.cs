using System;

namespace BitmapUtils
{
	/// <summary>
	/// Wrapper that frees a DC when Dispose is called.
	/// </summary>
	/// <remarks>
	/// This allows the C# using statement to be used to
	/// guarantee that a DC is deleted.
	/// </remarks>
	public struct GdiDC : IDisposable
	{
        private IntPtr dc;
        public GdiDC(IntPtr hDC)
		{
            dc = hDC;
        }
        public static implicit operator IntPtr(GdiDC wrapper)
        {
            return wrapper.dc;
        }

        public void Dispose()
        {
            if (dc != IntPtr.Zero)
            {
                Gdi.DeleteDC(dc);
                dc = IntPtr.Zero;
            }
        }
    }
}
