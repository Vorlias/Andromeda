using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Legacy
{
    [Obsolete("Temporary support for Legacy component loading")]
    public interface ILegacyComponent
    {
        Type LegacyType { get; }
    }
}
