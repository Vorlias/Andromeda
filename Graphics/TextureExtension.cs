using SFML.Graphics;

namespace Andromeda2D.Graphics
{
    public static class TextureExtension
    {
        public static NineSliceTexture CreateSlicedSubTexture(this Texture texture, SliceRect rect)
        {
            return new NineSliceTexture(texture, rect);
        }
    }


}
