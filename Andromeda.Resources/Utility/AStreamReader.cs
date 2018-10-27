using SFML.System;
using System;
using System.IO;
using System.Text;

namespace Andromeda.Resources.Utility
{
    [Obsolete("Use BinaryReader instead.")]
    public class AStreamReader : IDisposable
    {
        Stream stream;

        public void Dispose()
        {
            stream.Close();
        }

        /// <summary>
        /// Gets a boolean from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool GetBoolean()
        {
            byte[] buffer = new byte[sizeof(bool)];
            stream.Read(buffer, 0, sizeof(bool));
            bool result = BitConverter.ToBoolean(buffer, 0);
            return result;
        }

        /// <summary>
        /// Gets a byte from the Stream
        /// </summary>
        /// <returns></returns>
        public byte GetByte()
        {
            return (byte)stream.ReadByte();
        }

        /// <summary>
        /// Get a string of a specified length from the Stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="length">The length of the string</param>
        /// <returns></returns>
        public string GetString(int length)
        {
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            string result = Encoding.ASCII.GetString(buffer);
            return result;
        }

        /// <summary>
        /// Get an unsigned long (UInt64) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ulong GetUInt64()
        {
            byte[] buffer = new byte[sizeof(ulong)];
            stream.Read(buffer, 0, sizeof(ulong));
            ulong result = BitConverter.ToUInt64(buffer, 0);
            return result;
        }

        /// <summary>
        /// Get a long (Int64) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public long GetInt64()
        {
            byte[] buffer = new byte[sizeof(long)];
            stream.Read(buffer, 0, sizeof(long));
            long result = BitConverter.ToInt64(buffer, 0);
            return result;
        }

        /// <summary>
        /// Get an ushort (UInt16) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ushort GetUInt16()
        {
            byte[] buffer = new byte[sizeof(ushort)];
            stream.Read(buffer, 0, sizeof(ushort));
            ushort result = BitConverter.ToUInt16(buffer, 0);
            return result;
        }

        /// <summary>
        /// Get an uint (UInt32) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public uint GetUInt32()
        {
            byte[] buffer = new byte[sizeof(uint)];
            stream.Read(buffer, 0, sizeof(uint));
            uint result = BitConverter.ToUInt32(buffer, 0);
            return result;
        }

        /// <summary>
        /// Get an int (Int32) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public int GetInt32()
        {
            byte[] buffer = new byte[sizeof(int)];
            stream.Read(buffer, 0, sizeof(int));
            int result = BitConverter.ToInt32(buffer, 0);
            return result;
        }

        public struct StringBlock
        {
            public int Length { get; }
            public string Result { get; }

            public StringBlock(int length, string result)
            {
                Length = length;
                Result = result;
            }
        }

        /// <summary>
        /// Get a string block (Int32 with string length + the string) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public StringBlock GetStringBlock()
        {
            int size = GetInt32();
            string result = GetString(size);
            return new StringBlock(size, result);
        }

        /// <summary>
        /// Get a short (Int16) from the Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public short GetInt16()
        {
            byte[] buffer = new byte[sizeof(short)];
            stream.Read(buffer, 0, sizeof(short));
            short result = BitConverter.ToInt16(buffer, 0);
            return result;
        }

        /// <summary>
        /// Gets a float (Single) from the Stream
        /// </summary>
        /// <returns></returns>
        public float GetFloat()
        {
            byte[] buffer = new byte[sizeof(float)];
            stream.Read(buffer, 0, sizeof(float));
            float result = BitConverter.ToSingle(buffer, 0);
            return result;
        }

        /// <summary>
        /// Gets a Vector2u from the Stream
        /// </summary>
        /// <returns></returns>
        public Vector2u GetVector2u()
        {
            var x = GetUInt32();
            var y = GetUInt32();
            return new Vector2u(x, y);
        }

        /// <summary>
        /// Gets a Vector2i from the Stream
        /// </summary>
        /// <returns></returns>
        public Vector2i GetVector2i()
        {
            var x = GetInt32();
            var y = GetInt32();
            return new Vector2i(x, y);
        }

        /// <summary>
        /// Gets a Vector2f from the Stream
        /// </summary>
        /// <returns></returns>
        public Vector2f GetVector2f()
        {
            var x = GetFloat();
            var y = GetFloat();
            return new Vector2f(x, y);
        }


        public AStreamReader(Stream stream)
        {
            this.stream = stream;
        }
    }
}
