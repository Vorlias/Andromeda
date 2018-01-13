using SFML.System;
using System;
using Andromeda.System.Utility;

namespace Andromeda.Entities.Components
{
    public sealed class AnchorPoint
    {
        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public AnchorPoint(float x, float y)
        {
            X = Math.Min(Math.Max(0.0f, x), 1.0f);
            Y = Math.Min(Math.Max(0.0f, y), 1.0f);
        }
        
        internal Vector2f AppliedTo(SpriteRenderer renderer)
        {
            Vector2f textureSize = renderer.Texture.Size.ToFloat();
            textureSize.X *= X;
            textureSize.Y *= Y;

            return textureSize;
        }
    }
}
