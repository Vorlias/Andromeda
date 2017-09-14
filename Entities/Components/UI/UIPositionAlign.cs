using System;

namespace Andromeda2D.Entities.Components
{
    [Flags]
    public enum UIPositionAlign
    {
        /// <summary>
        /// Centers to the left of the screen
        /// </summary>
        Left = 1,

        /// <summary>
        /// Centers to the top of the screen
        /// </summary>
        Top = 2,

        /// <summary>
        /// Centers to the center of the screen on X
        /// </summary>
        LeftCenter = 4,

        /// <summary>
        /// Centers to the center of the screen on Y
        /// </summary>
        TopCenter = 8,

        /// <summary>
        /// Centers to the right of the screen
        /// </summary>
        Right = 16,

        /// <summary>
        /// Centers to the bottom of the screen
        /// </summary>
        Bottom = 32,

        /// <summary>
        /// Centers to the size X
        /// </summary>
        CenterWidth = 64,

        /// <summary>
        /// Centers to the size Y
        /// </summary>
        CenterHeight = 128,

        /// <summary>
        /// Changes the Y point to the end Y
        /// </summary>
        InverseHeight = 256,

        /// <summary>
        /// Changes the X point to the end X
        /// </summary>
        InverseWidth = 512,


    }
}
