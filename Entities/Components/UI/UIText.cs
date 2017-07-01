using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.System;
using VorliasEngine2D.Serialization;
using SFML.System;

namespace VorliasEngine2D.Entities.Components
{
    public class UIText : UIComponent
    {
        private Font font;
        private string fontId;
        private string text = "";
        private uint fontSize = 10U;
        private Vector2f size = new Vector2f(0, 0);

        /// <summary>
        /// The text displayed by this UIText component
        /// </summary>
        public string Text
        {
            get => text;
            set => text = value;
        }

        public uint FontSize
        {
            get => fontSize;
            set => fontSize = value;
        }

        public Vector2f RenderSize
        {
            get => size;
        }

        /// <summary>
        /// The font being used by this UIText
        /// </summary>
        public Font Font
        {
            get
            {
                if (fontId != null)
                    return FontManager.Instance.Get(fontId);
                else
                    return FontManager.Instance.Default;
            }
        }

        [PersistentProperty("FontId", PropertyType = SerializedPropertyType.String)]
        public string FontId
        {
            get => fontId;
            set
            {
                FontManager fontManager = FontManager.Instance;
                if (fontManager.ContainsKey(value))
                {
                    font = fontManager.Get(value);
                    fontId = value;
                }
            }
        }

        public override void OnComponentInit(Entity entity)
        {
            RenderOrder = RenderOrder.Interface + 1; // Bring it to top
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            Text fontText = new Text(Text, Font, FontSize)
            {
                Position = Transform.PositionRelative.GlobalAbsolute
            };
            target.Draw(fontText);
        }

        public override void AfterUpdate()
        {
            
        }

        public override void BeforeUpdate()
        {
            
        }

        public override void Update()
        {
            Text fontText = new Text(Text, Font, FontSize);
            var bounds = fontText.GetLocalBounds();
            size = new Vector2f(bounds.Width, bounds.Height);
        }
    }
}
