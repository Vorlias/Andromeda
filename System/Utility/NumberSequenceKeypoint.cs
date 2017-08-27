using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System.Utility
{

    /// <summary>
    /// A number sequence keypoint
    /// </summary>
    public struct NumberSequenceKeypoint : ISequenceKeyframe<float>
    {
        public float Time
        {
            get;
        }

        public float Value
        {
            get;
        }

        public float Deviation
        {
            get;
        }

        /// <summary>
        /// Creates a new NumberSequenceKeypoint
        /// </summary>
        /// <param name="time"></param>
        /// <param name="value"></param>
        /// <param name="deviation"></param>
        /// <exception cref="KeypointTimeException">Thrown if keypoint time is out of range of 0.0 - 1.0</exception>
        public NumberSequenceKeypoint(float time, float value, float deviation = 0.0f)
        {
            if (time > 1.0f || time < 0.0f)
            {
                throw new KeypointTimeException(time);
            }

            Time = time;
            Value = value;
            Deviation = deviation;
        }
    }
}
