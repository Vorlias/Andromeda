using Andromeda.Colliders;
using Andromeda.System.Types;
using Andromeda.System.Utility;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
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
    public class AssetArchive
    {
        string file;
        ZipArchive archive; //

        public bool Active
        {
            get => archive != null;
        }

        public IReadOnlyCollection<ZipArchiveEntry> Entries
        {
            get
            {
                return archive.Entries;
            }
        }

        protected AssetArchive(string file)
        {
            this.file = file;
            archive = ZipFile.Open(file, ZipArchiveMode.Update);

        }

        public const string EXTENSION_TTF = "ttf";
        public string[] FindEntriesWithExtension(string extension)
        {
            List<string> entries = new List<string>();
            var matchingEntries = archive.Entries.Where(e => e.Name.EndsWith("." + extension));
            foreach (var matchingEntry in matchingEntries)
            {
                entries.Add(matchingEntry.FullName);
            }
            return entries.ToArray();
        }

        public static string GetFriendlyName(string fileName)
        {
            return Regex.Replace(fileName, "([a-zA-Z0-9]+)\\.\\w+$", "$1");
        }

        /// <summary>
        /// Reads a Texture in the archive
        /// </summary>
        /// <param name="relativeFile"></param>
        /// <returns></returns>
        public Texture GetTexture(string relativeFile, IntRect area)
        {
            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();
            var stream = entry.Open();

            Texture image = new Texture(stream, area);

            stream.Close();
            return image;
        }

        /// <summary>
        /// Reads a font from the archive
        /// </summary>
        /// <param name="relativeFile"></param>
        /// <returns></returns>
        public Font GetFont(string relativeFile)
        {
            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();
            var stream = entry.Open();

            Font image = new Font(stream);

            stream.Close();
            return image;
        }

        public SoundBuffer GetSoundBuffer(string relativeFile)
        {
            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();
            var stream = entry.Open();

            SoundBuffer music = new SoundBuffer(stream);

            stream.Close();
            return music;
        }

        /// <summary>
        /// Reads an image in the archive
        /// </summary>
        /// <param name="relativeFile"></param>
        /// <returns></returns>
        public Image GetImage(string relativeFile)
        {
            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();
            var stream = entry.Open();

            Image image = new Image(stream);

            stream.Close();
            return image;
        }

        public StarMapColliderInfo ReadSMCColliderBinary(string relativeFile)
        {
            if (!relativeFile.EndsWith(".smc"))
                relativeFile += ".smc";

            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();

            return new StarMapColliderInfo(entry.Open());
        }

        public Polygon ReadACColliderBinary(string relativeFile)
        {
            if (!relativeFile.EndsWith(".ac"))
                relativeFile += ".ac";

            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();

            Polygon polygon = new Polygon();

            using (Stream fs = entry.Open())
            {
                while (fs.Position != fs.Length)
                {
                    byte[] xCoord = new byte[sizeof(ushort)];
                    byte[] yCoord = new byte[sizeof(ushort)];
                    fs.Read(xCoord, 0, sizeof(ushort));
                    fs.Read(yCoord, 0, sizeof(ushort));

                    ushort x = BitConverter.ToUInt16(xCoord, 0);
                    ushort y = BitConverter.ToUInt16(yCoord, 0);

                    polygon.Add(new Vector2f(x, y));
                }

                fs.Close();
            }

            return polygon;
        }

        public byte[] ReadAllBytes(string relativeFile)
        {
            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();
            var stream = entry.Open();

            byte[] buffer = new byte[1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while (stream.Position != stream.Length)
                {
                    read = stream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Reads all the text of a file in the archive
        /// </summary>
        /// <param name="relativeFile"></param>
        /// <returns></returns>
        public string ReadAllText(string relativeFile)
        {
            var entry = archive.Entries.Where(e => e.FullName == relativeFile).First();
            var stream = entry.Open();

            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();

            reader.Close();
            stream.Close();
            return result;
        }

        static Dictionary<string, AssetArchive> openArchives = new Dictionary<string, AssetArchive>();

        /// <summary>
        /// Attempts to release this archive from use
        /// </summary>
        public void Release()
        {
            if (openArchives.ContainsKey(file))
            {
                AssetArchive arch = openArchives[file];
                openArchives.Remove(file);
                arch.archive.Dispose();
                arch.archive = null;
            }
        }

        public static AssetArchive ReadArchive(string file)
        {
            if (openArchives.ContainsKey(file))
            {
                return openArchives[file];
            }
            else
            {
                AssetArchive archive = new AssetArchive(file);
                openArchives[file] = archive;
                return archive;
            }
        }
    }
}
