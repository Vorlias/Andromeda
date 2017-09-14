namespace Andromeda2D.Entities.Components
{
    public enum UIPositionAnchor
    {
        Center = UIPositionAlign.CenterWidth | UIPositionAlign.CenterHeight | UIPositionAlign.TopCenter | UIPositionAlign.LeftCenter,
        TopLeft = UIPositionAlign.Top | UIPositionAlign.Left,
        TopCenter = UIPositionAlign.CenterWidth | UIPositionAlign.Top | UIPositionAlign.LeftCenter,
        TopRight = UIPositionAlign.InverseWidth | UIPositionAlign.Top | UIPositionAlign.Right,
        BottomLeft = UIPositionAlign.Bottom | UIPositionAlign.Left | UIPositionAlign.InverseHeight,
        BottomCenter = UIPositionAlign.CenterWidth | UIPositionAlign.Bottom | UIPositionAlign.LeftCenter | UIPositionAlign.InverseHeight,
        BottomRight = UIPositionAlign.Bottom | UIPositionAlign.Right | UIPositionAlign.InverseWidth | UIPositionAlign.InverseHeight,
    }
}
