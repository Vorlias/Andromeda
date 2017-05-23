using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    public class Transform : Transformable, IComponent
    {
        private Entity entity;
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

        public string Name
        {
            get
            {
                return "Transform";
            }
        }
    }
}
