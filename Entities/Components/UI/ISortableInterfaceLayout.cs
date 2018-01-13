using Andromeda.Entities.Components.Internal;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.UIComponents
{
    public interface IInterfaceLayout
    {
        Vector2f Padding { get; set; }
        bool SizeToContents { get; set; }
    }

    public interface ISortableInterfaceLayout : IInterfaceLayout
    {
        LayoutSortOrder LayoutSortOrder { get; set; }
        Func<UIComponent, bool> CustomSortFunction { get; set; }
    }
}
