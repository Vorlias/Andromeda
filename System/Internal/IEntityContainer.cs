using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorlias2D.Entities;
using Vorlias2D.Entities.Components.Internal;

namespace Vorlias2D.System.Internal
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
