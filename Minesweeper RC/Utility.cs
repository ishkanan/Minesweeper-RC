using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_RC.Utility
{
    /// <summary>
    /// Provides general utility functions.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Adds a font stored as an assembly resource into a PrivateFontCollection instance.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="collection">The private font collection.</param>
        /// <param name="fontResourceName">The name of the font resource.</param>
        public static void AddFontFromResource(Assembly assembly, PrivateFontCollection collection, string fontResourceName)
        {
            var fontBytes = GetFontResourceBytes(assembly, fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            collection.AddMemoryFont(fontData, fontBytes.Length);
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
