using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.System.Internal
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
