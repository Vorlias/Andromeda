using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorlias2D.System.SequenceTypes
{
    public abstract class Sequence<KeyframeType, ValueType> 
        where KeyframeType : ISequenceKeyframe<ValueType>
        where ValueType : struct
    {
        protected List<KeyframeType> keypoints = new List<KeyframeType>();

        KeyframeType[] GetCurrentKeypointTransition(float currentTime)
        {
            var previousKeypoints = keypoints.Where(kp => kp.Time <= currentTime).OrderByDescending(kp => kp.Time);
            var nextKeypoints = keypoints.Where(kp => kp.Time > currentTime).OrderBy(kp => kp.Time);

            if (nextKeypoints.Count() > 0)
            {

                var nextKeyFrame = nextKeypoints.ElementAt(0);
                var previousKeyFrame = previousKeypoints.ElementAt(0);

                return new KeyframeType[] { previousKeyFrame, nextKeyFrame };
            }
            else
            {
                var previousKeyFrame = previousKeypoints.ElementAt(0);
                return new KeyframeType[] { previousKeyFrame };
            }

        }

        public ValueType First
        {
            get => keypoints[0].Value;
        }

        protected abstract KeyframeType GetAtTime(float time, KeyframeType first, KeyframeType last);

        /// <summary>
        /// Gets the latest number key sequence
        /// </summary>
        /// <param name="time">The time</param>
        /// <returns></returns>
        public KeyframeType GetAtTime(float time)
        {

            var sequences = GetCurrentKeypointTransition(time);
            if (sequences.Length > 1)
            {
                return GetAtTime(time, sequences[0], sequences[1]);
            }
            else
            {
                return GetCurrentKeypointTransition(time)[0];
            }
        }
    }
}
