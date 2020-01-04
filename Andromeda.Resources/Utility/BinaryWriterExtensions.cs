using Andromeda.System;
using Andromeda.System.SequenceTypes;
using Andromeda.System.Types;
using SFML.Graphics;
using SFML.System;
using System.IO;

namespace Andromeda.Resources.Utility
{
    /// <summary>
    /// Writes primitive data types and Andromeda Engine data types to a Stream
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes two four-byte unsigned integer values (representing a Vector2u) to the stream and advances it by 8 bytes
        /// </summary>
        /// <param name="coordinateAxis">The Vector2u</param>
        public static void Write(this BinaryWriter writer, Vector2u vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        /// <summary>
        /// Writes two four-byte signed integer values (representing a Vector2i) to the stream and advances it by 8 bytes
        /// </summary>
        /// <param name="coordinateAxis">The Vector2i</param>
        public static void Write(this BinaryWriter writer, Vector2i vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        /// <summary>
        /// Writes two four-byte floating-point values (representing a Vector2f) to the stream and advances it by 8 bytes
        /// </summary>
        /// <param name="coordinateAxis">The Vector2f</param>
        public static void Write(this BinaryWriter writer, Vector2f vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        /// <summary>
        /// Writes two four-byte floating-point values (representing a UIAxis) to the stream and advances it by 8 bytes
        /// </summary>
        /// <param name="coordinateAxis">The UIAxis</param>
        public static void Write(this BinaryWriter writer, UIAxis coordinateAxis)
        {
            writer.Write(coordinateAxis.Offset);
            writer.Write(coordinateAxis.Scale);
        }
        
        /// <summary>
        /// Writes eight four-byte floating point values (representing UICoordinates) to the stream and advances it by 16 bytes
        /// </summary>
        /// <param name="coordinates"></param>
        public static void Write(this BinaryWriter writer, UICoordinates coordinates)
        {
            writer.Write(coordinates.X);
            writer.Write(coordinates.Y);
        }

        public static void Write(this BinaryWriter writer, Color color)
        {
            writer.Write(color.ToInteger());
        }

        public static void Write(this BinaryWriter writer, ColorRange range)
        {
            writer.Write(range.Start);
            writer.Write(range.End);
        }

        public static void Write(this BinaryWriter writer, NumberRange range)
        {
            writer.Write(range.Min);
            writer.Write(range.Max);
        }

        public static void Write(this BinaryWriter writer, IntNumberRange range)
        {
            writer.Write(range.Min);
            writer.Write(range.Max);
        }
    }
}
