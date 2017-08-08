using System;
using System.Collections.Generic;
using System.Linq;

namespace VorliasEngine2D.System.Utility
{
    /// <summary>
    /// A sequence of numbers
    /// </summary>
    public class NumberSequence
    {
        List<NumberSequenceKeypoint> keypoints = new List<NumberSequenceKeypoint>();

        NumberSequenceKeypoint[] GetCurrentKeypointTransition(float currentTime)
        {
            var previousKeypoints = keypoints.Where(kp => kp.Time <= currentTime).OrderByDescending(kp => kp.Time);
            var nextKeypoints = keypoints.Where(kp => kp.Time > currentTime).OrderBy(kp => kp.Time);

            if (nextKeypoints.Count() > 0)
            {

                var nextKeyFrame = nextKeypoints.ElementAt(0);
                var previousKeyFrame = previousKeypoints.ElementAt(0);

                return new NumberSequenceKeypoint[] { previousKeyFrame, nextKeyFrame };
            }
            else
            {
                var previousKeyFrame = previousKeypoints.ElementAt(0);
                return new NumberSequenceKeypoint[] { previousKeyFrame };
            }

        }

        public float First
        {
            get => keypoints[0].Value;
        }

        /// <summary>
        /// Gets the latest number key sequence
        /// </summary>
        /// <param name="time">The time</param>
        /// <returns></returns>
        public NumberSequenceKeypoint GetAtTime(float time)
        {
            
            var sequences = GetCurrentKeypointTransition(time);
            if (sequences.Length > 1)
            {
                var first = sequences[0];
                var last = sequences[1];

                var valueDifference = last.Value - first.Value;

                float change = (time - first.Time) / (last.Time - first.Time);
                return new NumberSequenceKeypoint(0.0f, first.Value + (change * (last.Value - first.Value)));
            }
            else
            {
                return GetCurrentKeypointTransition(time)[0];
            }

            //return GetAllAtTime(time)[0];
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

        public NumberSequence(IEnumerable<NumberSequenceKeypoint> numbers)
        {
            keypoints.AddRange(numbers);

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
