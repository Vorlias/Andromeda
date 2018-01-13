namespace Andromeda.System
{
    public struct UIAxis
    {
        public float Scale
        {
            get;
            set;
        }

        public float Offset
        {
            get;
            set;
        }

        public UIAxis(float scale, float offset)
        {
            Scale = scale;
            Offset = offset;
        }
        public static UIAxis operator -(UIAxis left, UIAxis right)
        {
            return new UIAxis(left.Scale - right.Scale, left.Offset - right.Offset);
        }

        public static UIAxis operator +(UIAxis left, UIAxis right)
        {
            return new UIAxis(left.Scale + right.Scale, left.Offset + right.Offset);
        }

        public static UIAxis operator +(UIAxis left, float right)
        {
            return new UIAxis(left.Scale, left.Offset + right);
        }

        public static UIAxis operator /(UIAxis left, float right)
        {
            return new UIAxis(left.Scale / right, left.Offset / right);
        }


        public override string ToString()
        {
            return Scale + ", " + Offset;
        }
    }
}
