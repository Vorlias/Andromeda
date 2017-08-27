using System;
using System.Collections.Generic;
using System.Linq;

namespace Vorlias2D.System.SequenceTypes
{
    /// <summary>
    /// A sequence of numbers
    /// </summary>
    public class NumberSequence : Sequence<NumberSequenceKeypoint, float>
    {
        protected override NumberSequenceKeypoint GetAtTime(float time, NumberSequenceKeypoint first, NumberSequenceKeypoint last)
        {
            var valueDifference = last.Value - first.Value;

            float change = (time - first.Time) / (last.Time - first.Time);
            return new NumberSequenceKeypoint(0.0f, first.Value + (change * (last.Value - first.Value)));
        }

        /// <summary>
        /// Allows assigning a float value as a number sequence
        /// </summary>
        /// <param name="value">The value to assign</param>
        public static implicit operator NumberSequence(float value)
        {
            return new NumberSequence(value);
        }

        public NumberSequence(float value)
        {
            keypoints.Add(new NumberSequenceKeypoint(0.0f, value));
        }

        public NumberSequence(params NumberSequenceKeypoint[] numberSequenceKeypoints)
        {
            keypoints.AddRange(numberSequenceKeypoints);

            var lastKeypoint = keypoints.Last();
            if (lastKeypoint.Time < 1.0f)
            {
                Console.WriteLine("Add default");
                NumberSequenceKeypoint lastKF = new NumberSequenceKeypoint(1.0f, lastKeypoint.Value);
            }
        }

        /// <summary>
        /// Adds a new keypoint at specified time with the specified value
        /// </summary>
        /// <param name="time">The time</param>
        /// <param name="value">The value</param>
        public void Add(float time, float value)
        {
            keypoints.Add(new NumberSequenceKeypoint(time, value));
        }

    }
}
