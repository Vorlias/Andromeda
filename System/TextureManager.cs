using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace VorliasEngine2D.System
{
    public class TextureManager
    {
        Dictionary<string, Texture> textures;

        private TextureManager()
        {

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
        public Texture this[string id]
        {
            get
            {
                if (textures.ContainsKey(id))
                    return textures[id];
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the specified texture
        /// </summary>
        /// <param name="id">The id of the texture</param>
        /// <returns>The texture if it exists, otherwise null</returns>
        public Texture GetTexture(string id)
        {
            if (textures.ContainsKey(id))
                return textures[id];
            else
                return null;
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
    }
}
