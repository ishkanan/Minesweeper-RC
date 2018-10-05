using System;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Minesweeper_RC.Utility
{
    /// <summary>
    /// Provides general utility functions.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        ///  Returns a pointer to the bytes of a font stored as a resource in the specified assembly.
        ///  The result can be used to directly add the font to a PrivateFontCollection instance.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="fontResourceName">The name of the font resource.</param>
        /// <returns>The pointer and the number of bytes it points to.</returns>
        public static Tuple<IntPtr, int> GetFontResourcePointer(_Assembly assembly, string fontResourceName)
        {
            var fontBytes = GetFontResourceBytes(assembly, fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            return new Tuple<IntPtr, int>(fontData, fontBytes.Length);
        }

        /// <summary>
        /// Retrieves the bytes of a font stored as a resource in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <param name="fontResourceName">The name of the font resource.</param>
        /// <returns></returns>
        public static byte[] GetFontResourceBytes(_Assembly assembly, string fontResourceName)
        {
            var resourceStream = assembly.GetManifestResourceStream(fontResourceName);
            if (resourceStream == null)
                throw new ApplicationException(string.Format("Unable to find font '{0}' in embedded resources.", fontResourceName));
            var fontBytes = new byte[resourceStream.Length];
            resourceStream.Read(fontBytes, 0, (int)resourceStream.Length);
            resourceStream.Close();
            return fontBytes;
        }
    }
}
