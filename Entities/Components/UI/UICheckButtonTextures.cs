namespace Vorlias2D.Entities.Components.UI
{
    public class UICheckButtonTextures
    {
        private UITexture texture, checkedTexture;

        public UITexture Unchecked
        {
            get => texture;
            set => texture = value;
        }

        public UITexture Checked
        {
            get => checkedTexture.Texture != null ? checkedTexture : texture;
            set => checkedTexture = new UITexture(value);
        }
    }
}
