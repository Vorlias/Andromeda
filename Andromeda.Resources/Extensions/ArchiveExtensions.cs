using Andromeda.Resources.Archive;
using Andromeda.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Resources.Extensions
{
    public static class ArchiveExtensions
    {
        /// <summary>
        /// Loads all spritesheet textures from an archive
        /// </summary>
        /// <param name="manager">The TextureManager</param>
        /// <param name="archivePath">The archive's path</param>
        public static void LoadArchiveSpritesheets(this TextureManager manager, string archivePath)
        {
            TextureArchive texArchive = new TextureArchive(archivePath);
            texArchive.LoadEncodedSpritesheets();
            texArchive.LoadSpritesheets();

        }

        public static void LoadArchiveImages(this TextureManager manager, string archivePath)
        {
            TextureArchive texArchive = new TextureArchive(archivePath);
            texArchive.LoadImages();
        }

        public static void LoadSpritesheets(this PackageResources resourcePackage, string archivePath)
        {
            var x = resourcePackage.Archive as TextureArchive;
            x.LoadEncodedSpritesheets();
            x.LoadSpritesheets();
        }

        public static void LoadSpritesheets(this FolderResources folderResources, string folderPath)
        {
            //var x = folderResources.
        }

        public static void LoadArchiveFonts(this FontManager manager, string archivePath)
        {
            AssetArchive arch = AssetArchive.ReadArchive(archivePath);
            var fontNames = arch.FindEntriesWithExtension(AssetArchive.EXTENSION_TTF);
            foreach (var fontName in fontNames)
            {
                Font font = arch.GetFont(fontName);
                string friendlyName = AssetArchive.GetFriendlyName(fontName);
                Console.WriteLine("AddFont: " + friendlyName + " from " + fontName);
                FontManager.Instance.Add(friendlyName, font);
            }
        }

    }
}
