using SFML.Graphics;

namespace Andromeda.Graphics
{
    public static class TextureExtension
    {
        public static NineSliceTexture CreateSlicedSubTexture(this Texture texture, SliceRect rect)
        {
            return new NineSliceTexture(texture, rect);
        }
    }


}
