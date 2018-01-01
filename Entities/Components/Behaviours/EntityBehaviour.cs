using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System;
using SFML.Graphics;

namespace Andromeda2D.Entities.Components
{
    [Obsolete("Inherit 'Component' instead, see docs about Components")]
    public class EntityBehaviour : Component, IUpdatableComponent, IRenderableComponent
    {
        private bool initialized = false;

        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        bool enabled = true;
        public bool IsEnabled
        {
            get => enabled;
            set
            {
                enabled = value;

                if (value)
                    OnEnabled();
                else
                    OnDisabled();
            }
        }

        public virtual void OnEnabled()
        {

        }

        public virtual void OnDisabled()
        {

        }

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
        /// The TextureManager instance
        /// </summary>
        protected TextureManager TextureManager => TextureManager.Instance;

        /// <summary>
        /// The FontManager instance
        /// </summary>
        protected FontManager FontManager => FontManager.Instance;

        /// <summary>
        /// The SoundManager instance
        /// </summary>
        protected SoundManager SoundManager => SoundManager.Instance;

        /// <summary>
        /// The position information about the mouse
        /// </summary>
        public MouseCoordinates MousePosition
        {
            get => new MouseCoordinates(Application, Entity.GameView);
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
        /// The UI Transform associated with the entity
        /// </summary>
        public UITransform UITransform
        {
            get => entity.GetComponent<UITransform>();
        }

        /// <summary>
        /// Called when the component is initialized
        /// </summary>
        public virtual void Init()
        {

        }

        public virtual void Start()
        {

        }

        /// <summary>
        /// Called when the entity is updated
        /// </summary>
        public virtual void Update()
        {

        }

        public virtual void Collision(Entity other)
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
        protected override void OnComponentInit(Entity entity)
        {
            if (!initialized)
            {
                initialized = true;
                Init();
            }
                
        }

        public virtual UpdatePriority UpdatePriority => UpdatePriority.Normal;


        private RenderOrder renderOrder = RenderOrder.Normal;
        public virtual RenderOrder RenderOrder
        {
            get => renderOrder;
            set => renderOrder = value;
        }

        public new virtual void OnComponentCopy(Entity source, Entity copy)
        {
            // Do nothing, this is a custom component.
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Render();
        }

        public virtual void OnDestroy()
        {

        }
    }
}
