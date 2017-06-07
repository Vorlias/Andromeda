using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.System;
using SFML.System;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.System.Debug;

namespace VorliasEngine2D.Entities.Components
{
    public class ImageButton : UIComponent, ITextureComponent
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
                texture = TextureManager.Instance.GetTexture(value);
                Transform.Size = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                textureId = value;
                RenderOrder = RenderOrder.Interface;
                RectCollider.CreateRectCollider(Transform.Size.GlobalAbsolute);
            }
        }

        public PolygonRectCollider RectCollider
        {
            get
            {
                return Entity.GetComponent<PolygonRectCollider>();
            }
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var copyComponent = copy.AddComponent<ImageButton>();

            if (TextureId != null)
                copyComponent.TextureId = TextureId;
            else if (texture != null)
                copyComponent.Texture = texture;
        }

        public override void OnComponentInit(Entity entity)
        {
            var rectCollider = Entity.AddComponent<PolygonRectCollider>();
            rectCollider.CreateRectCollider(new Vector2f(100, 20));
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            

            UICoordinates size = Transform.Size;
            Vector2f totalSize = size.Absolute(target);

            if (texture != null)
            { 
                Sprite sprite = new Sprite(texture);
                sprite.Position = Entity.Transform.Position;
                sprite.Scale = new Vector2f(totalSize.X / texture.Size.X, totalSize.Y / texture.Size.Y);

                target.Draw(sprite);
            }
            else
            {
                RectangleShape rect = new RectangleShape(totalSize);
                rect.Position = Entity.Transform.Position;
                target.Draw(rect);
            }

            RectCollider.DebugRenderPolygonCollider(target, Color.Red);
        }

        public override void Update()
        {
            if (IsMouseOver)
                Console.WriteLine("Ayy");   
        }

        public override void AfterUpdate()
        {
            
        }

        public override void BeforeUpdate()
        {
            
        }

        public override string ToString()
        {
            if (textureId != null)
                return Name + " (Textured) - TextureId: " + textureId;
            else if (texture != null)
                return Name + " (Textured)";
            else
                return Name + " (No Texture)";
        }
    }
}
