using Andromeda.Entities;
using Andromeda.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Resources
{
    class PrefabResource : IResource
    {
        public ResourceType Type => ResourceType.Prefab;

        public Prefab Prefab { get; }

        public PrefabResource(Stream stream)
        {
            PrefabSerialization serialization = new PrefabSerialization(new StreamReader(stream));
            serialization.RunLexer();
            Prefab = serialization.ToPrefab();
        }
    }
}
