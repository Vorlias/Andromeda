using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Vorlias2D.System.Utility;
using Vorlias2D.System.Internal;
using Vorlias2D.Graphics;
using SFML.System;

namespace Vorlias2D.System
{

    public class TextureManager : ResourceManager<Texture>
    {
        Dictionary<string, NineSliceTexture> nineslicedTextures = new Dictionary<string, NineSliceTexture>();

        static TextureManager textureManager = new TextureManager();
        public static TextureManager Instance
        {
            get => textureManager;
        }

        /// <summary>
        /// Loads a texture from a file to the specified id
        /// </summary>
        /// <param name="id">The id of the texture</param>
        /// <param name="file">The file containing the texture</param>
        /// <param name="area">The area of the image to load</param>
        public void LoadToId(string id, string file, IntRect area = new IntRect())
        {
            Texture newTexture = new Texture(new Image(file), area);
            Add(id, newTexture);
        }

        /// <summary>
        /// Loads a NineSliceTexture to the specified texture id
        /// </summary>
        /// <param name="id">The id of the destination texture</param>
        /// <param name="slicedTexture">The NineSliceTexure</param>
        /// <param name="size">The size of the texture</param>
        public void LoadToId(string id, NineSliceTexture slicedTexture, Vector2u size)
        {
            Add(id, slicedTexture.ToTexture(size));
        }

        /// <summary>
        /// Gets a NineSliceTexture from the specified id
        /// </summary>
        /// <param name="slicedId">The id of the nineslice texture</param>
        /// <returns></returns>
        public NineSliceTexture GetSliced(string slicedId)
        {
            if (nineslicedTextures.ContainsKey(slicedId))
                return nineslicedTextures[slicedId];
            else
                throw new ResourceNotFoundException(typeof(NineSliceTexture).Name, slicedId);
        }

        /// <summary>
        /// Loads a NineSliceTexture from a texture source
        /// </summary>
        /// <param name="slicedId">The id of the NineSliceTexture</param>
        /// <param name="source">The source texture</param>
        /// <param name="rect">The slice rect</param>
        /// <returns>The NineSliceTexture</returns>
        public NineSliceTexture LoadSliced(string slicedId, Texture source, SliceRect rect)
        {
            NineSliceTexture sliced = new NineSliceTexture(source, rect);
            nineslicedTextures.Add(slicedId, sliced);
            return sliced;
        }
    }
}
