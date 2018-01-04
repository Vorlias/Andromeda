using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System.Internal
{

    public abstract class ResourceManager<ResourceType>
    {
        Dictionary<string, ResourceType> items;

        protected ResourceManager()
        {
            items = new Dictionary<string, ResourceType>();
        }

        public bool ContainsKey(string id)
        {
            return items.ContainsKey(id);
        }

        /// <summary>
        /// Get the string id of this instance (if applicable)
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns>The id</returns>
        public string FindId(ResourceType instance)
        {
            if (items.ContainsValue(instance))
            {
                return items.Where(kv => kv.Value.Equals(instance)).Select(kv => kv.Key).FirstOrDefault();
            }
            else
                return null;
        }

        /// <summary>
        /// Attempts to fetch the resource with the specified id
        /// </summary>
        /// <param name="id">The id of the resource</param>
        /// <returns>The resource</returns>
        public ResourceType Get(string id)
        {
            if (items.ContainsKey(id))
                return items[id];
            else
                throw new ResourceNotFoundException(typeof(ResourceType).Name, id);
        }

        /// <summary>
        /// Attempts to fetch the resource with the specified id, if it's found it will set the texture variable and return true,
        /// otherwise it will return false.
        /// </summary>
        /// <param name="id">The id of the resource</param>
        /// <param name="resource">The resource variable</param>
        /// <returns>True if the resource was found</returns>
        public bool TryGet(string id, out ResourceType resource)
        {
            try
            {
                resource = Get(id);
                return true;
            }
            catch (ResourceNotFoundException e)
            {
                Console.Error.WriteLine(e.Message);
                resource = default(ResourceType);
                return false;
            }
        }

        /// <summary>
        /// Adds the resource to this resource manager
        /// </summary>
        /// <param name="id">The id of the resource</param>
        /// <param name="resource">The resource</param>
        public virtual void Add(string id, ResourceType resource)
        {
            if (!items.ContainsKey(id))
                items.Add(id, resource);
            else
                Console.Error.WriteLine("Item '" + id + "' already exists!");
        }
    }
}
