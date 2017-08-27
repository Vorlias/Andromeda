using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.SequenceTypes
{ 
    /// <summary>
    /// A sequence of colours
    /// </summary>
    public class ColorSequence : Sequence<ColorSequenceKeypoint, Color>
    {
        Color ColorLerp(Color first, Color second, float alpha)
        {
            return new Color(
                (byte)(second.R + (second.R - first.R) * (alpha - 1)),
                (byte)(second.G + (second.G - first.G) * (alpha - 1)),
                (byte)(second.B + (second.B - first.B) * (alpha - 1)),
                (byte)(second.A + (second.A - first.A) * (alpha - 1))
            );
        }

        protected override ColorSequenceKeypoint GetAtTime(float time, ColorSequenceKeypoint first, ColorSequenceKeypoint last)
        {
            float change = (time - first.Time) / (last.Time - first.Time);
            return new ColorSequenceKeypoint(0.0f, ColorLerp(first.Value, last.Value, change));
        }


        /// <summary>
        /// Allows assigning a float value as a number sequence
        /// </summary>
        /// <param name="value">The value to assign</param>
        public static implicit operator ColorSequence(Color value)
        {
            return new ColorSequence(value);
        }

        public ColorSequence(Color value)
        {
            keypoints.Add(new ColorSequenceKeypoint(0.0f, value));
        }

        public ColorSequence(params ColorSequenceKeypoint[] numberSequenceKeypoints)
        {
            keypoints.AddRange(numberSequenceKeypoints);

            var lastKeypoint = keypoints.Last();
            if (lastKeypoint.Time < 1.0f)
            {
                Console.WriteLine("Add default");
                ColorSequenceKeypoint lastKF = new ColorSequenceKeypoint(1.0f, lastKeypoint.Value);
            }
        }
    }
}
