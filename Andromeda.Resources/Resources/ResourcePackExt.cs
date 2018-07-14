using Andromeda.Debugging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Andromeda.Resources
{
    public static class ResourcePackExt
    {
        public static void Load(this IEnumerable<ILoadableResource> resources)
        {
            foreach (var resource in resources)
            {
                resource.Load();
            }
        }

        public static void LoadAll(this IResourcePackage pkg)
        {
            pkg.LoadableResources.Load();
        }

        internal static void Log(this IResourcePackage pkg, string message, params string[] arg)
        {
            DebugConsole.Log(new DebugOptions() { Prefix = "Resources", TagColor = ConsoleColor.Blue, TextColor = ConsoleColor.DarkBlue }, message, arg);
        }

        internal static string ToIdFormat(this string name)
        {
            return Regex.Replace(name, "([a-zA-Z0-9]+)\\.\\w+$", "$1");
        }
    }
}
