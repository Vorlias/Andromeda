using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    class Transform : Transformable, IComponent
    {
        public string Name
        {
            get
            {
                return "Transform";
            }
        }
    }
}
