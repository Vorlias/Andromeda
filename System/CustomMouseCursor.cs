using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System
{
    public class CustomMouseCursor
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
    }
}
