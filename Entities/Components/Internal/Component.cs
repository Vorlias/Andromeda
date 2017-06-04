using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components.Internal
{
    public abstract class Component : IComponent
    {
        protected Entity entity;

        public abstract bool AllowsMultipleInstances
        {
            get;
        }

        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public abstract string Name
        {
            get;
        }

        public virtual void OnComponentCopy(Entity source, Entity copy)
        {

        }

        public virtual void OnComponentInit(Entity entity)
        {

        }

        public void ComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();

            OnComponentInit(entity);
        }
    }
}
