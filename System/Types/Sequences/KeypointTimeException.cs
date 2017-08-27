using System;

namespace VorliasEngine2D.System.SequenceTypes
{
    public class KeypointTimeException : Exception
    {
        public KeypointTimeException(float time) : base("Keypoint time out of range of 0.0 - 1.0, got " + time)
        {

        }
    }
}
