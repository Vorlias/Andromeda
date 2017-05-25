using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Base class for custom scripted components
    /// </summary>
    public class EntityBehaviour : IComponent
    {
        private Entity entity;

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

        public virtual string Name
        {
            get
            {
                return "EntityBehaviour";
            }
        }
    }
}
