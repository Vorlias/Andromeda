using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Makes the property auto copy itself when the object is copied
    /// (Used for component copying)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PersistentPropertyAttribute : Attribute
    {
    }
}
