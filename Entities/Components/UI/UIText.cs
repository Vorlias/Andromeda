using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System;
using Andromeda2D.Serialization;
using SFML.System;

namespace Andromeda2D.Entities.Components
{
    public partial class UIText : UIComponent
    {
        public enum SizeMode
        {
            /// <summary>
            /// The size of this UI component is scaled to the text (Legacy behaviour)
            /// </summary>
            [Obsolete]
            SizeToText,

            /// <summary>
            /// The text is centered to the size of the component
            /// </summary>
            CenterTextToSize,
        }

        private Font font;
        private string fontId;
        private string text = "";
        private uint fontSize = 10U;
        private Vector2f size = new Vector2f(0, 0);

        /// <summary>
        /// The text displayed by this UIText component
        /// </summary>
        [SerializableProperty("Text", PropertyType = SerializedPropertyType.String)]
        public string Text
        {
            get => text;
            set => text = value;
        }

        SizeMode sizeMode = SizeMode.CenterTextToSize;
        [Obsolete]
        public SizeMode TextSizeMode
        {
            get => sizeMode;
            set => sizeMode = value;
        }

        TextXAlignment xAlignment = TextXAlignment.Center;
        public TextXAlignment TextXAlignment
        {
            get => xAlignment;
            set => xAlignment = value;
        }

        TextYAlignment yAlignment = TextYAlignment.Center;
        public TextYAlignment TextYAlignment
        {
            get => yAlignment;
            set => yAlignment = value;
        }

        protected Vector2f GetPositionOfText(Text text)
        {
            float x = 0;
            float y = 0;
            var textSize = text.GetLocalBounds();
            var uiSize = Transform.Size.GlobalAbsolute;
            var uiCenter = uiSize / 2;

            if (TextXAlignment == TextXAlignment.Center)
                x = uiCenter.X - textSize.Width / 2;
            else if (TextXAlignment == TextXAlignment.Right)
                x = uiSize.X - textSize.Width;

            if (TextYAlignment == TextYAlignment.Center)
                y = uiCenter.Y - textSize.Height / 2;
            else if (TextYAlignment == TextYAlignment.Bottom)
                y = uiSize.Y - textSize.Height;

            return new Vector2f(x, y);
        }

        [SerializableProperty("FontSize", PropertyType = SerializedPropertyType.UInt32)]
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

        [SerializableProperty("FontId", PropertyType = SerializedPropertyType.String)]
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
            Entity.AddComponent<UITransform>();
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            UIText sourceText = source.GetComponent<UIText>();
            UIText copyText = copy.AddComponent<UIText>();
            copyText.Text = sourceText.Text;
            copyText.FontSize = sourceText.FontSize;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (TextSizeMode == SizeMode.SizeToText)
            {
                Text fontText = new Text(Text, Font, FontSize)
                {
                    Position = Transform.PositionRelative.GlobalAbsolute
                };
                target.Draw(fontText);
            }
            else
            {
                Text fontText = new Text(Text, Font, FontSize);
                var bounds = fontText.GetLocalBounds();
                fontText.Position = Transform.PositionRelative.GlobalAbsolute + GetPositionOfText(fontText);
                target.Draw(fontText);
            }
        }

        public override void AfterUpdate()
        {
            
        }

        public override void BeforeUpdate()
        {
            
        }

        public override string ToString()
        {
            return "UIText: " + Text;
        }

        public override void Update()
        {
            if (TextSizeMode == SizeMode.SizeToText)
            {
                Text fontText = new Text(Text, Font, FontSize);
                var bounds = fontText.GetLocalBounds();
                size = new Vector2f(bounds.Width, bounds.Height);

                Transform.Size = size;
            }
        }
    }
}
