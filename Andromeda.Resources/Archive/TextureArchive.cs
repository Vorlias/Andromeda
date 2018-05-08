using Andromeda.Resources.FileTypes;
using Andromeda.Resources.Types;
using Andromeda.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Andromeda.Resources.Archive
{
    internal class TextureArchive : AssetArchive
    {
        public IEnumerable<ZipArchiveEntry> TextureMapFiles
        {
            get
            {
                return Entries.Where(entry => entry.Name.EndsWith(".json"));
            }
        }

        public IEnumerable<ZipArchiveEntry> AndromedaSpritesheets
        {
            get
            {
                return Entries.Where(entry => entry.Name.EndsWith(".as"));
            }
        }

        public IEnumerable<ZipArchiveEntry> TextureFiles
        {
            get
            {
                return Entries.Where(entry => Regex.IsMatch(entry.Name, @"\.(png|jpg)$"));
            }
        }

        public void LoadImages()
        {
            TextureManager textureManager = TextureManager.Instance;

            Console.WriteLine(TextureFiles.Count());

            foreach (var textureFile in TextureFiles)
            {
                //var textureFile = TextureFiles.First(tf => tf.FullName == name + ".png");
                if (textureFile != null)
                {
                    //foreach (var info in textureMap.Metadata)
                    // {

                    //    Texture texture = ReadTexture(textureFile.FullName, info.TextureRect);
                    //    texture.Smooth = true;
                    //    textureManager.Add(info.Id, texture);
                    //}

                    string textureName = Regex.Replace(textureFile.Name, @"\.(.*)", "");

                    Texture texture = GetTexture(textureFile.FullName, new IntRect());
                    textureManager.Add(textureName, texture);
                    Console.WriteLine("Registered texture: " + textureName);
                }

            }
        }

        public void LoadEncodedSpritesheets()
        {
            TextureManager textureManager = TextureManager.Instance;

            foreach (var entry in AndromedaSpritesheets)
            {

                if (SpriteFile.Decode(ReadAllBytes(entry.FullName), out byte[] spritesheetData, out byte[] imageData))
                {
                    TextureMap textureMap = TextureMap.FromJSON(Encoding.ASCII.GetString(spritesheetData));
                    foreach (var info in textureMap.Metadata)
                    {
                        Texture texture = new Texture(new MemoryStream(imageData), info.TextureRect);
                        texture.Smooth = true;
                        textureManager.Add(info.Id, texture);
                    }

                }
            }
        }

        /// <summary>
        /// Loads all the spritesheets from an archive
        /// </summary>
        public void LoadSpritesheets()
        {
            TextureManager textureManager = TextureManager.Instance;

            foreach (var entry in TextureMapFiles)
            {
                string name = Regex.Replace(entry.Name, @"\.json$", "");
                TextureMap textureMap = TextureMap.FromJSON(ReadAllText(entry.FullName));

                var textureFile = TextureFiles.First(tf => tf.FullName == name + ".png");
                if (textureFile != null)
                {
                    foreach (var info in textureMap.Metadata)
                    {

                        Texture texture = GetTexture(textureFile.FullName, info.TextureRect);
                        texture.Smooth = true;
                        textureManager.Add(info.Id, texture);
                    }
                }
            }
        }

        public TextureArchive(string file) : base(file)
        {

        }
    }
}
