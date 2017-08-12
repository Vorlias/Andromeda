using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace VorliasEngine2D.Graphics
{
    /// <summary>
    /// A Line shape
    /// With thanks from https://github.com/SFML/SFML/wiki/Source:-Line-segment-with-thickness
    /// </summary>
    public class LineShape : Shape
    {
        Vector2f direction;
        float thickness;

        public float Length
        {
            get => (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
        }

        public float Thickness
        {
            get => thickness;
            set
            {
                thickness = value;
                Update();
            }
        }

        public override Vector2f GetPoint(uint index)
        {
            Vector2f unitDirection = direction / Length;
            Vector2f unitPerpendicular = new Vector2f(-unitDirection.Y, unitDirection.X);

            Vector2f offset = (thickness / 2.0f) * unitPerpendicular;

            switch (index)
            {
                default:
                case 0:
                    return offset;
                case 1:
                    return direction + offset;
                case 2:
                    return direction - offset;
                case 3:
                    return -offset;
            }
        }

        public LineShape(Vector2f point1, Vector2f point2)
        {
            direction = (point2 - point1);
            Position = point1;
            thickness = 2.0f;
        }

        public override uint GetPointCount()
        {
            return 4;
        }
    }
}
