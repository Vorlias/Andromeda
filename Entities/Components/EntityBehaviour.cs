using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Base class for custom scripted components
    /// </summary>
    public class EntityBehaviour : IComponent
    {
        private Entity entity;

        /// <summary>
        /// Access the InputManager for this entity
        /// </summary>
        public UserInputManager Input
        {
            get
            {
                return entity.Input;
            }
        }

        /// <summary>
        /// Return the StateApplication
        /// </summary>
        public StateApplication Application
        {
            get
            {
                return StateApplication.Application;
            }
        }

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                return entity.SpriteRenderer;
            }
        }

        public Transform Transform
        {
            get
            {
                return entity.Transform;
            }
        }

        public Entity Entity
        {
            get
            {
                return entity;
            }

            set
            {
                entity = value;
            }
        }

        /// <summary>
        /// Called when the component is initialized
        /// </summary>
        public virtual void Init()
        {

        }

        public virtual void Awake()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public virtual void BeforeUpdate()
        {

        }

        public virtual void AfterUpdate()
        {

        }

        /// <summary>
        /// Called when the component is added to an entity
        /// </summary>
        /// <param name="entity">The entity it is added to</param>
        /// <exception cref="SetEntityInvalidException">Called if the user tries to set it</exception>
        public void OnComponentInit(Entity entity)
        {
            if (this.entity == null)
            {
                this.entity = entity;
                Init();
            }  
            else
                throw new SetEntityInvalidException();
        }

        public virtual string Name
        {
            get
            {
                return "EntityBehaviour";
            }
        }
    }
}
