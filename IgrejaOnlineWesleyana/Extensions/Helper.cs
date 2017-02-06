using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IgrejaOnlineWesleyana.Extensions
{
    public static class Helper
    {
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

    }
}