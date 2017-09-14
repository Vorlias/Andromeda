using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components;
using Andromeda2D.System;

namespace Andromeda2D.Entities.Components.UI
{
    /// <summary>
    /// Extension class for easy UI addition
    /// </summary>
    public static class UIExtension
    {
        public static UIImageButton AddImageButton(this Entity parent, string textureId, UICoordinates position = default(UICoordinates), UICoordinates size = default(UICoordinates))
        {
            Entity entity = parent.CreateChild();
            UIImageButton uiImage = entity.AddComponent<UIImageButton>();
            uiImage.TextureId = textureId;
            UITransform transform = entity.GetComponent<UITransform>();
            transform.LocalPosition = position;
            transform.Size = size;

            return uiImage;
        }

        public static UIImage AddImage(this Entity parent, string textureId, UICoordinates position = default(UICoordinates), UICoordinates size = default(UICoordinates))
        {
            Entity entity = parent.CreateChild();
            UIImage uiImage = entity.AddComponent<UIImage>();
            uiImage.TextureId = textureId;
            UITransform transform = entity.GetComponent<UITransform>();
            transform.LocalPosition = position;
            transform.Size = size;

            return uiImage;
        }

        public static UIText AddText(this Entity parent, string text, uint fontSize, UICoordinates position = default(UICoordinates))
        {
            Entity entity = parent.CreateChild();
            UIText uiText = entity.AddComponent<UIText>();
            uiText.Text = text;
            uiText.FontSize = fontSize;
            entity.GetComponent<UITransform>().LocalPosition = position;
            return uiText;
        }
    }
}
