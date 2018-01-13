using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components.Internal
{
    public abstract class Component : IComponent
    {
        protected Entity entity;

        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public virtual void OnComponentCopy(Entity source, Entity copy)
        {

        }

        protected virtual void OnComponentInit(Entity entity)
        {

        }

        void IComponent.ComponentInit(Entity entity)
        {
            this.ComponentInit(entity);
        }

        private void ComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();

            OnComponentInit(entity);
        }
    }
}
