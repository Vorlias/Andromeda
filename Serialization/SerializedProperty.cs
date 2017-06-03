using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Serialization
{
    public enum SerializedPropertyType
    {
        String,
        Int32,
        Int64,
        Float,
        Double
    }

    /// <summary>
    /// A property that can be serialized
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class PersistentPropertyAttribute : Attribute
    {
        public SerializedPropertyType PropertyType
        {
            get;
            set;
        }

        public PersistentPropertyAttribute(string propertyName)
        {

        }
    }
}
