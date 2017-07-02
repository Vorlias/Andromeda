using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System
{
    public class CustomMouseCursor : Drawable
    {
        public RenderWindow Context
        {
            get;
            internal set;
        }

        Texture mouseTexture;
        public Texture Texture
        {
            get
            {
                return mouseTexture;
            }
            set
            {
                Context.SetMouseCursorVisible(value == null);
                mouseTexture = value;
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (Texture != null && Visible)
            {
                Vector2i mousePosition = Mouse.GetPosition(Context);
                Sprite mouseSprite = new Sprite(Texture)
                {
                    Position = new Vector2f(mousePosition.X, mousePosition.Y)
                };
                Context.Draw(mouseSprite);
            }
        }
    }
}
