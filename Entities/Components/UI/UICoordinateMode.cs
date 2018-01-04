namespace Andromeda.Entities.Components
{
    public enum UICoordinateMode
    {
        /// <summary>
        /// Will be based on the parent's X and Y values.
        /// </summary>
        ParentXY,

        /// <summary>
        /// Will start from the render origin (top 0, left 0)
        /// </summary>
        Origin,
    }
}
