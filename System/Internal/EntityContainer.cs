using System.Collections.Generic;
using System.Linq;
using VorliasEngine2D.Entities;

namespace VorliasEngine2D.System.Internal
{
    public abstract class EntityContainer : IEntityContainer
    {
        HashSet<Entity> children;

        public EntityContainer()
        {
            children = new HashSet<Entity>();
        }

        /// <summary>
        /// The child entities of this entity
        /// </summary>
        public Entity[] Children => children.ToArray();

        public List<Entity> Descendants
        {
            get
            {
                List<Entity> entities = new List<Entity>();
                entities.AddRange(children);

                foreach (var child in children)
                {
                    entities.AddRange(child.Descendants);
                }

                return entities;
            }
        }

        public void RemoveEntity(Entity child)
        {
            children.Remove(child);
        }

        protected void AddEntityAsChild(Entity child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Add a child entity to this entity
        /// </summary>
        /// <param name="child">The entity</param>
        public void AddEntity(Entity child)
        {
            child.Init();
            AddEntityAsChild(child);
            child.StartBehaviours();
        }

        public abstract Entity CreateChild();

        /// <summary>
        /// Gets the first child entity with the specified name
        /// </summary>
        /// <param name="name">The name of the entity to find</param>
        /// <returns>An entity if it's found, otherwise null</returns>
        public Entity FindFirstChild(string name)
        {
            return children.First(entity => entity.Name == name);
        }
    }
}
