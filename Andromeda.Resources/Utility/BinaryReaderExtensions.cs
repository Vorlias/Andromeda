using Andromeda.System;
using Andromeda.System.Types;
using SFML.Graphics;
using SFML.System;
using System.IO;

namespace Andromeda.Resources.Utility
{
    /// <summary>
    /// Reads primitive data types and Andromeda Engine data types from a Stream
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads two four-byte signed integer values (representing a Vector2i) from the stream and advances it by 8 bytes
        /// </summary>
        public static Vector2i ReadVector2i(this BinaryReader reader)
        {
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();

            return new Vector2i(x, y);
        }

        /// <summary>
        /// Reads two four-byte unsigned integer values (representing a Vector2u) from the stream and advances it by 8 bytes
        /// </summary>
        public static Vector2u ReadVector2u(this BinaryReader reader)
        {
            uint x = reader.ReadUInt32();
            uint y = reader.ReadUInt32();

            return new Vector2u(x, y);
        }

        /// <summary>
        /// Reads two four-byte floating-point values (representing a UIAxis) from the stream and advances it by 8 bytes
        /// </summary>
        public static UIAxis ReadUIAxis(this BinaryReader reader)
        {
            float scale = reader.ReadSingle();
            float offset = reader.ReadSingle();

            return new UIAxis(scale, offset);
        }

        /// <summary>
        /// Reads eight four-byte floating point values (representing UICoordinates) from the stream and advances it by 16 bytes
        /// </summary>
        public static UICoordinates ReadUICoordinates(this BinaryReader reader)
        {
            UIAxis x = reader.ReadUIAxis();
            UIAxis y = reader.ReadUIAxis();
            return new UICoordinates(x, y);
        }

        /// <summary>
        /// Reads two four-byte floating-point values (representing a Vector2f) from the stream and advances it by 8 bytes
        /// </summary>
        public static Vector2f ReadVector2f(this BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();

            return new Vector2f(x, y);
        }

        public static Color ReadColor(this BinaryReader reader)
        {
            return new Color(reader.ReadUInt32());
        }

        public static ColorRange ReadColorRange(this BinaryReader reader)
        {
            Color start = reader.ReadColor();
            Color end = reader.ReadColor();
            return new ColorRange() { Start = start, End = end };
        }

        public static NumberRange ReadNumberRange(this BinaryReader reader)
        {
            var min = reader.ReadSingle();
            var max = reader.ReadSingle();
            return new NumberRange(min, max);
        }

        public static IntNumberRange ReadIntNumberRange(this BinaryReader reader)
        {
            var min = reader.ReadInt32();
            var max = reader.ReadInt32();
            return new IntNumberRange(min, max);
        }
    }
}
