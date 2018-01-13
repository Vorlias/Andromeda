using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    /// <summary>
    /// Disallow multiple of these components on an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DisallowMultipleAttribute : Attribute
    {
        public DisallowMultipleAttribute()
        {

        }
    }
}
