using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Andromeda.Resources.Utility
{
    public class AStreamWriter : IDisposable
    {
        List<byte> list = new List<byte>();

        public Stream TargetStream
        {
            get;
            set;
        }

        public AStreamWriter(Stream stream)
        {
            TargetStream = stream;
        }

        /// <summary>
        /// Disposes this StreamWriter (will write to the target stream)
        /// </summary>
        public void Dispose()
        {
            TargetStream.Write(list.ToArray(), 0, list.Count);
        }

        /// <summary>
        /// Writes a string to the Stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteString(string value)
        {
            byte[] strVal = Encoding.ASCII.GetBytes(value);
            list.AddRange(strVal);
        }

        /// <summary>
        /// Writes a string block (Int32 of the size + the string) to the Stream
        /// </summary>
        /// <param name="value">The string to write</param>
        public void WriteStringBlock(string value)
        {
            byte[] strSize = BitConverter.GetBytes(value.Length);
            byte[] strVal = Encoding.ASCII.GetBytes(value);

            list.AddRange(strSize);
            list.AddRange(strVal);
        }

        /// <summary>
        /// Writes a byte to the Stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteByte(byte value)
        {
            list.Add(value);
        }

        /// <summary>
        /// Writes a ushort (UInt16) to the Stream
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteUInt16(ushort value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }


        /// <summary>
        /// Writes a short (Int16) to the Stream
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteInt16(short value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a uint (UInt32) to the Stream
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteUInt32(uint value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a int (Int32) to the Stream
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteInt32(int value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a long (Int64) to the Stream.
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteInt64(long value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a ulong (UInt64) to the Stream.
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteUInt64(ulong value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a float to the Stream.
        /// </summary>
        /// <param name="value">The value</param>
        public void WriteFloat(float value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a Vector2u to the Stream
        /// </summary>
        /// <param name="vector2f">The value</param>
        public void WriteVector2u(Vector2u vector2u)
        {
            WriteInt64(vector2u.X);
            WriteInt64(vector2u.Y);
        }

        /// <summary>
        /// Writes a Vector2i to the Stream
        /// </summary>
        /// <param name="vector2f">The value</param>
        public void WriteVector2i(Vector2i vector2i)
        {
            WriteInt32(vector2i.X);
            WriteInt32(vector2i.Y);
        }

        /// <summary>
        /// Writes a Vector2f to the Stream
        /// </summary>
        /// <param name="vector2f">The value</param>
        public void WriteVector2f(Vector2f vector2f)
        {
            WriteFloat(vector2f.X);
            WriteFloat(vector2f.Y);
        }
    }
}
