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

        /// <summary>
        /// The SpriteRenderer associated with the entity
        /// </summary>
        public SpriteRenderer SpriteRenderer
        {
            get
            {
                return entity.SpriteRenderer;
            }
        }

        /// <summary>
        /// The Transform associated with the entity
        /// </summary>
        public Transform Transform
        {
            get
            {
                return entity.Transform;
            }
        }

        /// <summary>
        /// The parent entity of this behaviour
        /// </summary>
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

        /// <summary>
        /// Called when the entity is updated
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Called when the entity is rendered
        /// </summary>
        public virtual void Render()
        {

        }

        /// <summary>
        /// Called before the child entities are updated
        /// </summary>
        public virtual void BeforeUpdate()
        {

        }

        /// <summary>
        /// Called after the child entities are updated
        /// </summary>
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
