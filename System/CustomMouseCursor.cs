using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    public class CustomMouseCursor : Drawable
    {
        public RenderWindow Context
        {
            get;
            set;
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
                mouseTexture = value;
                UpdateVisibility();
            }
        }

        internal void UpdateVisibility()
        {
            Context.SetMouseCursorVisible(Texture == null);
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
