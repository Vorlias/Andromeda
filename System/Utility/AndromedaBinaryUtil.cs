using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda2D.System.Utility
{
    public static class AndromedaBinaryUtil
    {
        public static bool ReadBool(this Stream stream)
        {
            int size = sizeof(bool);
            byte[] value = new byte[size];
            stream.Read(value, 0, size);
            return BitConverter.ToBoolean(value, 0);
        }

        public static Vector2i ReadVector2i(this Stream stream)
        {
            int x = stream.ReadInt32();
            int y = stream.ReadInt32();
            return new Vector2i(x, y);
        }

        public static Vector2u ReadVector2u(this Stream stream)
        {
            uint x = stream.ReadUInt32();
            uint y = stream.ReadUInt32();
            return new Vector2u(x, y);
        }

        public static string ReadText(this Stream stream)
        {
            int length = stream.ReadInt32();
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            return Encoding.ASCII.GetString(buffer);
        }

        public static UInt32 ReadUInt32(this Stream stream)
        {
            int size = sizeof(UInt32);
            byte[] value = new byte[size];
            stream.Read(value, 0, size);
            return BitConverter.ToUInt32(value, 0);
        }

        public static Int32 ReadInt32(this Stream stream)
        {
            int size = sizeof(Int32);
            byte[] value = new byte[size];
            stream.Read(value, 0, size);
            return BitConverter.ToInt32(value, 0);
        }

        public static Int64 ReadInt64(this Stream stream)
        {
            int size = sizeof(Int64);
            byte[] value = new byte[size];
            stream.Read(value, 0, size);
            return BitConverter.ToInt64(value, 0);
        }

        public static Int16 ReadInt16(this Stream stream)
        {
            int size = sizeof(Int32);
            byte[] value = new byte[size];
            stream.Read(value, 0, size);
            return BitConverter.ToInt16(value, 0);
        }
    }
}
