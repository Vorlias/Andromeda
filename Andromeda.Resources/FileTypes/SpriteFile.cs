using System;
using System.IO;
using System.Text;

namespace Andromeda.Resources.FileTypes
{
    class SpriteFile
    {
        public static bool Decode(byte[] data, out byte[] spritesheetData, out byte[] imageData)
        {
            using (MemoryStream fs = new MemoryStream(data))
            {
                byte[] headerBytes = new byte[4];
                fs.Read(headerBytes, 0, 4);




                string header = Encoding.ASCII.GetString(headerBytes);
                if (header == ((char)2) + "ASF")
                {
                    byte[] imageSizeBytes = new byte[sizeof(Int32)];
                    fs.Read(imageSizeBytes, 0, sizeof(Int32));
                    int imageSize = BitConverter.ToInt32(imageSizeBytes, 0);

                    if (imageSize > 0)
                    {
                        imageData = new byte[imageSize];
                        fs.Read(imageData, 0, imageSize);
                    }
                    else
                    {
                        imageData = new byte[0];
                    }


                    byte[] sizeBytes = new byte[sizeof(Int32)];
                    fs.Read(sizeBytes, 0, sizeof(Int32));
                    int size = BitConverter.ToInt32(sizeBytes, 0);

                    if (size > 0)
                    {
                        spritesheetData = new byte[size];
                        fs.Read(spritesheetData, 0, size);

                    }
                    else
                    {
                        spritesheetData = new byte[0];
                    }
                }
                else
                {
                    throw new InvalidDataException("Invalid header for spritesheet data");
                }
            }

            return spritesheetData.Length > 0 && imageData.Length > 0;
        }
    }
}
