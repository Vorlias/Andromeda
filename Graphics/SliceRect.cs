namespace Andromeda2D.Graphics
{
    public struct SliceRect
    {
        public int Right
        {
            get;
            set;
        }

        public int Bottom
        {
            get;
            set;
        }

        public int Width
        {
            get
            {
                return Right;
            }
        }

        public int Height
        {
            get
            {
                return Bottom;
            }
        }

        public int Top
        {
            get;
            set;
        }

        public int Left
        {
            get;
            set;
        }

        public SliceRect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public SliceRect(int inset)
        {
            Left = inset;
            Top = inset;
            Right = inset;
            Bottom = inset;
        }
    }


}
