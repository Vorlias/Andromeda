using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    public sealed class Transform : Transformable, IComponent
    {
        private Entity entity;
        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return "Transform";
            }
        }

        /// <summary>
        /// Called when the component is added to an entity
        /// </summary>
        /// <param name="entity">The entity it is added to</param>
        /// <exception cref="SetEntityInvalidException">Called if the user tries to set it</exception>
        public void OnComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();
        }

        public void OnComponentCopy(Entity source, Entity copy)
        {
            var sourceTransform = source.Transform;
            var copyTransform = copy.Transform;

            // Just set the properties the same :-)
            copyTransform.Origin = sourceTransform.Origin;
            copyTransform.Position = sourceTransform.Position;
            copyTransform.Scale = sourceTransform.Scale;
        }
    }
}
