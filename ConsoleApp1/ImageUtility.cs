using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ImageConversions
{
  public  class ImageUtility
    {
        public byte[] ToGif(byte[] buffer)
        {
            using (var memBuffer = new MemoryStream(buffer))
            {
                MemoryStream output = ConvertToGif(memBuffer);
                return output.GetBuffer();
            }
        }

        public static MemoryStream ConvertToGif(Stream imgStream)
        {
            MemoryStream retStream = new MemoryStream();

            using (Image img = Image.FromStream(imgStream, true, true))
            {
                ImageCodecInfo gifEncoder = GetEncoder(ImageFormat.Gif);

                img.Save(retStream, gifEncoder, null);
                retStream.Flush();
            }

            return retStream;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
