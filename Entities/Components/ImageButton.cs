using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.System;
using SFML.System;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.Entities.Components
{
    public class ImageButton : UIRenderer, ITextureComponent, IUpdatableComponent
    {
        public override string Name
        {
            get
            {
                return "ImageButton";
            }
        }

        private Texture texture;
        private string textureId;

        public Texture Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        public string TextureId
        {
            get
            {
                return textureId;
            }

            set
            {
                Console.WriteLine("SetTexture {0}", value);
                texture = TextureManager.Instance.GetTexture(value);
                Transform.Size = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                textureId = value;
                RenderOrder = RenderOrder.Interface;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        { 
            UICoordinates size = Transform.Size;
            Vector2f totalSize = size.Absolute(target);
            Sprite sprite = new Sprite(texture);
            sprite.Scale = new Vector2f(totalSize.X / texture.Size.X, totalSize.Y / texture.Size.Y);

            target.Draw(sprite);
        }

        public void Update()
        {

        }

        public void AfterUpdate()
        {
            
        }

        public void BeforeUpdate()
        {
            
        }
    }
}
