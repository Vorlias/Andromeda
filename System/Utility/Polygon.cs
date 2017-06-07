using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.Utility
{
    public class NumberRange
    {
        public float Min
        {
            get; private set;
        }
        public float Max
        {
            get; private set;
        }

        public bool Overlaps(NumberRange other)
        {
            return !(other.Max < Min || other.Min > Max);
        }

        public NumberRange(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }

    public class Polygon : List<Vector2f>
    {

        public float Left
        {
            get
            {
                float min = this[0].X;
                foreach (var p in this)
                {
                    if (p.X > min) continue;
                    min = p.X;
                }
                return min;
            }
        }

        public float Right
        {
            get
            {
                float max = this[0].X;
                foreach (var p in this)
                {
                    if (p.X < max) continue;
                    max = p.X;
                }
                return max;
            }
        }

        public float Top
        {
            get
            {
                float min = this[0].Y;
                foreach (var p in this)
                {
                    if (p.Y > min) continue;
                    min = p.Y;
                }
                return min;
            }
        }

        public float Bottom
        {
            get
            {
                float max = this[0].Y;
                foreach (var p in this)
                {
                    if (p.Y < max) continue;
                    max = p.Y;
                }
                return max;
            }
        }

        /// <summary>
        /// The Width of the polygon determined by the right most point minus the left most point.
        /// </summary>
        public float Width
        {
            get
            {
                float min = float.MaxValue;
                float max = float.MinValue;
                foreach (var p in this)
                {
                    min = Math.Min(min, p.X);
                    max = Math.Max(max, p.X);
                }
                return Math.Abs(max - min);
            }
        }

        /// <summary>
        /// The Height of the polygon determined by the bottom most point minus the top most point.
        /// </summary>
        public float Height
        {
            get
            {
                float min = float.MaxValue;
                float max = float.MinValue;
                foreach (var p in this)
                {
                    min = Math.Min(min, p.Y);
                    max = Math.Max(max, p.Y);
                }
                return Math.Abs(max - min);
            }
        }

        /// <summary>
        /// Offset all the points by a Vector2 amount.
        /// </summary>
        /// <param name="vector">The offset amount.</param>
        public void OffsetPoints(Vector2f vector)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] += vector;
            }
        }

        public static Polygon CreateRectangle(float width, float height)
        {
            var poly = new Polygon();

            poly.Add(new Vector2f(0, 0));
            poly.Add(new Vector2f(width, 0));
            poly.Add(new Vector2f(width, height));
            poly.Add(new Vector2f(0, height));
            poly.Add(new Vector2f(0, 0));

            return poly;
        }

        public Polygon(List<Vector2f> verts)
        {
            verts.ForEach(vert => Add(vert));
        }

        public Polygon()
        {

        }

        /// <summary>
        /// Checks to see if the polygon contains a point
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>True if the point is inside the polygon</returns>
        public bool ContainsPoint(Vector2f point)
        {
            // I have no idea how this works either! http://stackoverflow.com/questions/11716268/point-in-polygon-algorithm
            int i, j, nvert = Count;
            bool c = false;

            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((this[i].Y) >= point.Y) != (this[j].Y >= point.Y) && (point.X <= (this[j].X - this[i].X) * (point.Y - this[i].Y) / (this[j].Y - this[i].Y) + this[i].X))
                    c = !c;
            }

            return c;
        }

        public List<Vector2f> Axes
        {
            get
            {
                var axes = new List<Vector2f>();
                for (var i = 0; i < Count; i++)
                {
                    Vector2f p1 = this[i];
                    Vector2f p2 = this[i + 1 == Count ? 0 : i + 1]; // Clever!
                    Vector2f edge = p1 - p2;
                    Vector2f normal = new Vector2f(-edge.Y, edge.X);
                    axes.Add(normal);
                }
                return axes;
            }
        }

        public NumberRange Projection(Vector2f axis)
        {
            if (Count == 0) return new NumberRange(0, 0);

            float min = axis.Dot(this[0]);
            float max = min;

            for (var i = 0; i < Count; i++)
            {
                float p = axis.Dot(this[i]);
                if (p < min)
                {
                    min = p;
                }
                else if (p > max)
                {
                    max = p;
                }
            }

            return new NumberRange(min, max);
        }

        public Polygon Transform(Vector2f offset, Vector2f origin,  float angle = 0)
        {
            return new Polygon(this.Select(vec => (vec + origin).Rotate(angle) + offset ).ToList());
        }



        /// <summary>
        /// Turns it into a drawable VertexArray
        /// </summary>
        /// <param name="color">The colour</param>
        /// <returns></returns>
        public VertexArray ToVertexArray(Color color)
        {
            VertexArray vertices = new VertexArray();
            ForEach(vert => vertices.Append(new Vertex(vert, color)));
            return vertices;
        }


        [Obsolete("Use 'ToVertexArray' instead.")]
        public VertexArray VertexArray
        {
            get
            {
                VertexArray vertices = new VertexArray();
                ForEach(vert => vertices.Append(new Vertex(vert, Color.Green)));
                return vertices;
            }
        }

        public bool Overlaps(Polygon other)
        {
            var axes = Axes;
            axes.AddRange(other.Axes);

            int i = 0;
            foreach (var axis in axes)
            {
                var p1 = Projection(axis);
                var p2 = other.Projection(axis);
                if (!p1.Overlaps(p2))
                {
                    return false;
                }
                i++;
            }

            return true;
        }

        public override string ToString()
        {
            string arrayString = "";
            ForEach(axis => arrayString += "<" + axis.X + ", " + axis.Y + ">; ");

            return "Polygon {" + arrayString + "}";
        }
    }
}
