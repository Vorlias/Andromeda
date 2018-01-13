using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda2D.Serialization
{

    /// <summary>
    /// A property that can be serialized
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SerializablePropertyAttribute : Attribute
    {
        public SerializedPropertyType PropertyType
        {
            get;
            set;
        }

        public SerializablePropertyAttribute(string propertyName)
        {

        }
    }
}
