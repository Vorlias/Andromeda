using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.System.Internal;

namespace Andromeda.System
{
    public class FontManager : ResourceManager<Font>
    {
        static FontManager fontManager = new FontManager();
        public static FontManager Instance
        {
            get => fontManager;
        }

        Font defaultFont;
        /// <summary>
        /// The specified default font
        /// </summary>
        /// <exception cref="ResourceNotFoundException"></exception>
        public Font Default
        {
            get
            {
                if (defaultFont != null)
                    return defaultFont;
                else
                    throw new ResourceNotFoundException(typeof(Font).Name, "Default");
            }
        }

        /// <summary>
        /// Loads a font from file to the specified id
        /// </summary>
        /// <param name="id">The id of the font</param>
        /// <param name="file">The font file path</param>
        public void LoadToId(string id, string file)
        {
            Add(id, new Font(file));
        }

        /// <summary>
        /// Loads a font from file to the specified id, and sets it as the default
        /// </summary>
        /// <param name="id">The id of the font</param>
        /// <param name="file">The font file path</param>
        public void LoadToIdDefault(string id, string file)
        {
            Add(id, new Font(file));
            SetDefaultFont(id);
        }

        /// <summary>
        /// Adds the font as the default font to be used
        /// </summary>
        /// <param name="id">The id of the font</param>
        /// <param name="font">The font</param>
        public void AddDefault(string id, Font font)
        {
            Add(id, font);
            SetDefaultFont(id);
        }

        /// <summary>
        /// Sets the default font
        /// </summary>
        /// <param name="id">The id of the font</param>
        public void SetDefaultFont(string id)
        {
            if (TryGet(id, out Font font))
            {
                defaultFont = font;
            }
        }
    }
}
