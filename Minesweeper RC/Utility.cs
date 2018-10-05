using System;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
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
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns>The pointer and the number of bytes it points to.</returns>
        public static Tuple<IntPtr, int> GetResourceBytesPointer(ResourceManager manager, string resourceName)
        {
            var fontBytes = GetResourceBytes(manager, resourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            return new Tuple<IntPtr, int>(fontData, fontBytes.Length);
        }

        /// <summary>
        /// Retrieves the bytes of an embedded resource accessible via the specified
        /// ResourceManager instance.
        /// </summary>
        /// <param name="manager">The ResourceManager instance that can access the resource.</param>
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns></returns>
        public static byte[] GetResourceBytes(ResourceManager manager, string resourceName)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if (resourceName == null)
                throw new ArgumentNullException("resourceName");
            if (resourceName.Length == 0)
                throw new ArgumentException("resourceName cannot be blank.");

            var resSet = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var bytes = resSet.GetObject(resourceName);
            if (bytes == null)
                throw new ArgumentException("Resource '" + resourceName + "' not found.");
            return (byte[])bytes;
        }
    }
}
