using Andromeda.System.Types;
using Andromeda.System.Utility;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Colliders
{
    /// <summary>
    /// A collider that works with the Star Map editor for Andromeda
    /// </summary>
    public class StarMapColliderInfo
    {
        public Vector2u Size
        {
            get;
        }

        public Polygon Polygon
        {
            get;
        }

        /// <summary>
        /// Read StarMap collider data from a stream
        /// </summary>
        /// <param name="stream"></param>
        public StarMapColliderInfo(Stream stream)
        {
            Polygon polygon = new Polygon();

            int version, vertexCount;
            Vector2u size;
            bool autoSize;

            using (stream)
            {
                version = stream.ReadByte();
                autoSize = stream.ReadBool();
                size = stream.ReadVector2u();

                vertexCount = stream.ReadInt32();

                for (int i = 0; i < vertexCount; i++)
                {
                    Vector2i vertex = stream.ReadVector2i();
                    polygon.Add(vertex.ToFloat());
                }
            }

            stream.Close();

            Size = size;
            Polygon = polygon;
        }
    }
}
