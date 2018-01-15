using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Andromeda.Entities.Components.Internal;
using Andromeda.System;
using Andromeda.Serialization;
using SFML.System;
using Andromeda.System.Input;

namespace Andromeda.Entities.Components
{
    public partial class UIText : UIComponent, ITextObject
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
        private bool posFix = true;

        /// <summary>
        /// HACK: Fixes the Y position for most fonts.
        /// THopefully I figure out the actual problem with this.
        /// </summary>
        public bool UseYPositionFix
        {
            get => posFix;
            set => posFix = value;
        }

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
        [Obsolete("No longer functional", true)]
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

        Vector2f textPadding;
        public Vector2f Padding
        {
            get => textPadding;
            set => textPadding = value;
        }

        protected Vector2f GetPositionOfText(Text text)
        {
            var pos = Transform.GlobalPosition.GlobalAbsolute;

            float x = 0;
            float y = 0;
            var textSize = text.GetLocalBounds();
            var uiSize = Transform.GlobalSize.GlobalAbsolute;
            var height = UseYPositionFix ? text.CharacterSize + 5 : textSize.Height;

            var uiCenter = uiSize / 2.0f;

            if (TextXAlignment == TextXAlignment.Center)
                x = uiCenter.X - textSize.Width / 2.0f;
            else if (TextXAlignment == TextXAlignment.Right)
                x = uiSize.X - textSize.Width;

            if (TextYAlignment == TextYAlignment.Center)
                y = uiCenter.Y - height / 2.0f;
            else if (TextYAlignment == TextYAlignment.Bottom)
                y = uiSize.Y - height;

            return pos + new Vector2f(x, y) + Padding;
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

        protected override void OnComponentInit(Entity entity)
        {
            RenderOrder = RenderOrder.Interface + 1; // Bring it to top
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            UIText sourceText = source.GetComponent<UIText>();
            UIText copyText = copy.AddComponent<UIText>();
            copyText.Text = sourceText.Text;
            copyText.FontSize = sourceText.FontSize;
        }

        public override void Update()
        {

        }

        public float Width
        {
            get;
            protected set;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            Text fontText = new Text(Text, Font, FontSize);
            fontText.FillColor = Color;
            Width = fontText.GetLocalBounds().Width;
            fontText.Position = GetPositionOfText(fontText);
            target.Draw(fontText);
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

    }
}
