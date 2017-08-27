using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorlias2D.System.Utility;

namespace Vorlias2D.System.SequenceTypes
{ 
    /// <summary>
    /// A sequence of colours
    /// </summary>
    public class ColorSequence : Sequence<ColorSequenceKeypoint, Color>
    {

        protected override ColorSequenceKeypoint GetAtTime(float time, ColorSequenceKeypoint first, ColorSequenceKeypoint last)
        {
            float change = (time - first.Time) / (last.Time - first.Time);
            return new ColorSequenceKeypoint(0.0f, first.Value.Lerp(last.Value, change));
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
