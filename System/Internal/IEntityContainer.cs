using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.Entities;
using Andromeda.Entities.Components.Internal;

namespace Andromeda.System.Internal
{
    /// <summary>
    /// Interface for instance containers
    /// </summary>
    public interface IEntityContainer
    {
        Entity FindFirstChild(string name);

        Entity[] Children
        {
            get;
        }

        void AddEntity(Entity child);
        Entity CreateChild();
    }
}
