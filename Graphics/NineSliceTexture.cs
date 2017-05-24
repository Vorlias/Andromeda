using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Graphics
{
    public class SliceRect
    {
        public int Right
        {
            get;
            set;
        }

        public int Bottom
        {
            get;
            set;
        }

        public int Width
        {
            get
            {
                return Right;
            }
        }

        public int Height
        {
            get
            {
                return Bottom;
            }
        }

        public int Top
        {
            get;
            set;
        }

        public int Left
        {
            get;
            set;
        }

        public SliceRect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    public static class TextureExtension
    {
        public static Texture CreateSlicedSubTexture(this Texture texture, SliceRect rect)
        {
            Image image = texture.CopyToImage();
            NineSliceTexture resultTexture = new NineSliceTexture(image, rect);


            return null;
        }
    }

    public class NineSliceTexture
    {
        Texture topLeft,
            topCentre,
            topRight;

        Texture centreLeft,
            centre,
            centreRight;

        Texture bottomLeft,
            bottomCentre,
            bottomRight;

        RenderTexture renderTexture;
        SliceRect sliceRect;

        public Texture ToTexture(Vector2u size, Color color = new Color())
        {
            RenderTexture rTexture = new RenderTexture(size.X, size.Y);

            // TOP

            Sprite topLeftSprite = new Sprite(topLeft);
            topLeftSprite.Color = color;
            rTexture.Draw(topLeftSprite);

            Sprite topCentreSprite = new Sprite(topCentre);
            topCentreSprite.Position = new Vector2f(sliceRect.Left, 0);
            topCentreSprite.Color = color;
            rTexture.Draw(topCentreSprite);

            Sprite topRightSprite = new Sprite(topRight);
            topRightSprite.Position = new Vector2f(size.X - sliceRect.Width, 0);
            topRightSprite.Color = color;
            rTexture.Draw(topRightSprite);

            // CENTRE

            Sprite centreLeftSprite = new Sprite(centreLeft);
            centreLeftSprite.Position = new Vector2f(0, sliceRect.Top);
            centreLeftSprite.Color = color;
            rTexture.Draw(centreLeftSprite);

            Sprite centreSprite = new Sprite(centre);
            centreSprite.Position = new Vector2f(sliceRect.Left, sliceRect.Top);
            centreSprite.Color = color;
            rTexture.Draw(centreSprite);

            Sprite centreRightSprite = new Sprite(centreRight);
            centreRightSprite.Position = new Vector2f(size.X - sliceRect.Width, sliceRect.Height);

            // BOTTOM

            return null;
        }

        public NineSliceTexture(Image image, SliceRect sliceRect)
        {
            this.sliceRect = sliceRect;

            Vector2u textureSize = image.Size;

            // TOP

            topLeft = new Texture(image, new IntRect(0, 0, sliceRect.Width, sliceRect.Height));
            topLeft.Repeated = false;

            topCentre = new Texture(image, new IntRect(sliceRect.Left, 0, (int)textureSize.X - (sliceRect.Width * 2), sliceRect.Height));
            topCentre.Repeated = false;

            topRight = new Texture(image, new IntRect((int)textureSize.X - sliceRect.Left, 0, sliceRect.Width, sliceRect.Height));
            topRight.Repeated = false;

            // CENTRE

            centreLeft = new Texture(image, new IntRect(0, sliceRect.Top, sliceRect.Width, (int)textureSize.Y - (sliceRect.Height * 2)));
            centreLeft.Repeated = false;

            centre = new Texture(image, new IntRect(sliceRect.Left, sliceRect.Top, (int)textureSize.X - (sliceRect.Width * 2), (int)textureSize.Y - (sliceRect.Height * 2)));
            centre.Repeated = false;

            centreRight = new Texture(image, new IntRect((int)textureSize.X - sliceRect.Left, sliceRect.Top, sliceRect.Width, (int)textureSize.Y - (sliceRect.Height * 2)));
            centreRight.Repeated = false;

            // BOTTOM

            bottomLeft = new Texture(image, new IntRect(0, (int) textureSize.Y - sliceRect.Top, sliceRect.Width, sliceRect.Height));
            bottomLeft.Repeated = false;

            bottomCentre = new Texture(image, new IntRect(sliceRect.Left, (int) textureSize.Y - sliceRect.Top, (int) textureSize.Y - (sliceRect.Width * 2), sliceRect.Height));
            bottomCentre.Repeated = false;
            
            bottomRight = new Texture(image, new IntRect((int) textureSize.X - sliceRect.Left, (int) textureSize.Y - sliceRect.Top, sliceRect.Width, sliceRect.Height));
            bottomRight.Repeated = false;
        }


    }


}
