﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.Entities.Components
{
    // TODO: Figure out why it requires IRenderableComponent to render child UIRenderer
    public class UserInterface : Component, IContainerComponent, IRenderableComponent
    {
        public override bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a UIComponent of the specified type to this UserInterface
        /// </summary>
        /// <typeparam name="UIComponentType"></typeparam>
        /// <returns></returns>
        public UIComponentType Add<UIComponentType>() where UIComponentType : UIComponent, new()
        {
            Entity child = Entity.SpawnEntity();
            return child.AddComponent<UIComponentType>();
        }

        public override void OnComponentInit(Entity entity)
        {
            entity.Name = "UserInterface";
        }

        public void ChildAdded(Entity entity)
        {
            entity.AddComponent<UITransform>();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            
        }

        public override string Name
        {
            get
            {
                return "UserInterface";
            }
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var ui = copy.AddComponent<UserInterface>();
        }

        public override string ToString()
        {
            int count = Entity.GetComponentsInDescendants<UIComponent>().Count;

            if (count == 1)
                return Name + " - " + count + " UIComponent";
            else
                return Name + " - " + count +" UIComponents";
        }

        public RenderOrder RenderOrder
        {
            get
            {
                return RenderOrder.Foreground;
            }
            set
            {

            }
        }
    }
}