using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Graphics
{

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

        //RenderTexture renderTexture;
        SliceRect sliceRect;
        Texture sourceTexture;
        RenderTexture renderTexture;
        Color color = Color.White;

        /// <summary>
        /// Creates a NineSlice texture
        /// </summary>
        /// <param name="size">The size of this texture</param>
        /// <param name="color">The colour of this texture</param>
        /// <returns></returns>
        public Texture ToTexture(Vector2u size)
        {
            renderTexture = new RenderTexture(size.X, size.Y);

            renderTexture.Clear(Color.Transparent);

            // TOP

            Sprite topLeftSprite = new Sprite(topLeft)
            {
                Color = color
            };
            renderTexture.Draw(topLeftSprite);

            Sprite topCentreSprite = new Sprite(topCentre, new IntRect(sliceRect.Left, 0, (int) size.X - ( sliceRect.Width * 2 ), sliceRect.Height))
            {
                Position = new Vector2f(sliceRect.Left, 0),
                Color = color
            };
            renderTexture.Draw(topCentreSprite);

            Sprite topRightSprite = new Sprite(topRight)
            {
                Position = new Vector2f(size.X - sliceRect.Width, 0),
                Color = color
            };
            renderTexture.Draw(topRightSprite);

            // CENTRE

            Sprite centreLeftSprite = new Sprite(centreLeft, new IntRect(0, 0, sliceRect.Width, ( (int) size.Y - sliceRect.Height * 2 )))
            {
                Position = new Vector2f(0, sliceRect.Top),
                Color = color
            };
            renderTexture.Draw(centreLeftSprite);

            Sprite centreSprite = new Sprite(centre, new IntRect(0, 0, (int) size.X - ( sliceRect.Width * 2 ), (int) size.Y - ( sliceRect.Height * 2 )))
            {
                Position = new Vector2f(sliceRect.Left, sliceRect.Top),
                Color = color
            };
            renderTexture.Draw(centreSprite);

            Sprite centreRightSprite = new Sprite(centreRight, new IntRect(0, 0, sliceRect.Width, ( (int) size.Y - sliceRect.Height * 2 )))
            {
                Position = new Vector2f(size.X - sliceRect.Width, sliceRect.Height),
                Color = color
            };
            renderTexture.Draw(centreRightSprite);

            // BOTTOM

            Sprite bottomLeftSprite = new Sprite(bottomLeft, new IntRect(0, 0, sliceRect.Width, sliceRect.Height))
            {
                Position = new Vector2f(0, size.Y - sliceRect.Height),
                Color = color
            };
            renderTexture.Draw(bottomLeftSprite);

            Sprite bottomCentreSprite = new Sprite(bottomCentre, new IntRect(0, 0, (int) size.X - ( sliceRect.Width * 2 ), sliceRect.Height))
            {
                Position = new Vector2f(sliceRect.Left, size.Y - sliceRect.Top),
                Color = color
            };
            renderTexture.Draw(bottomCentreSprite);

            Sprite bottomRightSprite = new Sprite(bottomRight, new IntRect(0, 0, sliceRect.Width, sliceRect.Height))
            {
                Position = new Vector2f(size.X - sliceRect.Left, size.Y - sliceRect.Top),
                Color = color
            };
            renderTexture.Draw(bottomRightSprite);


            renderTexture.Display();
            return renderTexture.Texture;
        }

        /// <summary>
        /// Creates a NineSliceTexture from a texture (expensive)
        /// </summary>
        /// <param name="texture">The texture to load it from</param>
        /// <param name="sliceRect">The slice rect</param>
        public NineSliceTexture(Texture texture, SliceRect sliceRect) : this(texture.CopyToImage(), sliceRect)
        {
            sourceTexture = texture;
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
