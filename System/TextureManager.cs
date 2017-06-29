using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System
{
    public class InvalidTextureIdException : Exception
    {
        public InvalidTextureIdException(string textureId) : base ("Invalid texture id given: " + textureId)
        {

        }
    }

    public class TextureManager
    {
        Dictionary<string, Texture> textures;

        private TextureManager()
        {
            textures = new Dictionary<string, Texture>();
        }

        private static TextureManager instance;

        /// <summary>
        /// Returns the static instance of the TextureManager
        /// </summary>
        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextureManager();

                return instance;
            }
        }

        /// <summary>
        /// Get the texture with the specified id
        /// </summary>
        /// <param name="id">The id of the texture</param>
        /// <returns>The texture if it exists, otherwise null</returns>
        /// <exception cref="InvalidTextureIdException">Thrown when the texture id is not found</exception>
        public Texture this[string id]
        {
            get
            {
                if (textures.ContainsKey(id))
                    return textures[id];
                else
                    throw new InvalidTextureIdException(id);
            }
        }

        /// <summary>
        /// Gets the specified texture
        /// </summary>
        /// <param name="id">The id of the texture</param>
        /// <returns>The texture if it exists, otherwise null</returns>
        /// <exception cref="InvalidTextureIdException">Thrown when the texture id is not found</exception>
        public Texture GetTexture(string id)
        {
            if (textures.ContainsKey(id))
                return textures[id];
            else
                throw new InvalidTextureIdException(id);
        }

        /// <summary>
        /// Gets the specified texture - Static alias of
        /// <seealso cref="GetTexture(string)"/>
        /// </summary>
        /// <param name="id">The id of the texture</param>
        /// <returns></returns>
        public static Texture Get(string id)
        {
            return Instance.GetTexture(id);
        }

        /// <summary>
        /// Attempts to fetch the texture with the specified id, if it's found it will set the texture variable and return true,
        /// otherwise it will return false.
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="texture">The texture variable</param>
        /// <returns>True if the texture was found</returns>
        public static bool TryGet(string id, out Texture texture)
        {
            try
            {
                texture = Instance.GetTexture(id);
                return true;
            }
            catch (InvalidTextureIdException e)
            {
                Console.Error.WriteLine(e.Message);
                texture = null;
                return false;
            }
        }

        /// <summary>
        /// Loads a texture to id from a file
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="file">The file to load from</param>
        public void LoadToId(string id, string file, IntRect area = new IntRect())
        {
            Texture newTexture = new Texture(new Image(file), area);
            textures.Add(id, newTexture);
        }

        public void Add(string id, Texture texture)
        {
            textures.Add(id, texture);
        }
    }
}
