using Andromeda.System;
using System.IO;
using System.Linq;

namespace Andromeda.Resources
{ 
    public static class Resources
    {
        public static FolderResources FromFolder(string folder)
        {
            if (Directory.Exists(folder))
                return new FolderResources(folder);
            else
                throw new DirectoryNotFoundException(folder);
        }

        public static PackageResources FromArchive(string archive)
        {
            if (File.Exists(archive))
                return new PackageResources(archive);
            else
                throw new FileNotFoundException(archive);
        }
    }
}
