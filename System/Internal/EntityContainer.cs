using System;
using System.Collections.Generic;
using System.Linq;
using Andromeda2D.Entities;

namespace Andromeda2D.System.Internal
{
    public abstract class EntityContainer : IEntityContainer
    {
        HashSet<Entity> children;

        public EntityContainer()
        {
            children = new HashSet<Entity>();
        }

        /// <summary>
        /// The child entities of this entity
        /// </summary>
        public Entity[] Children => children.ToArray();

        /// <summary>
        /// The descendants of this EntityContainer (Children + Children of Children)
        /// </summary>
        public List<Entity> Descendants
        {
            get
            {
                List<Entity> entities = new List<Entity>();
                entities.AddRange(children);

                foreach (var child in children)
                {
                    entities.AddRange(child.Descendants);
                }

                return entities;
            }
        }

        /// <summary>
        /// Removes a child entity from this container
        /// </summary>
        /// <param name="child"></param>
        internal void RemoveChild(Entity child)
        {
            children.Remove(child);
        }

        /// <summary>
        /// Adds a child entity to this container
        /// </summary>
        /// <param name="child"></param>
        internal void AddChild(Entity child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Add a child entity to this entity
        /// </summary>
        /// <param name="child">The entity</param>
        public void AddEntity(Entity child)
        {
            if (child.ParentContainer == null)
                child.Init();
            else
                child.ParentContainer.RemoveChild(child);

            if (this is GameView)
            {
                var view = this as GameView;
                child.SetParentView(view);
                child.SetParent(view);
            }
            else if (this is Entity)
            {
                var parent = this as Entity;
                child.SetParentView(parent.GameView);
                child.SetParent(parent);
            }

            AddChild(child);
        }

        /// <summary>
        /// Clears all children in this entity container
        /// </summary>
        public void DestroyAllChildren()
        {
            children.Clear();
        }

        /// <summary>
        /// Creates a child entity parented to this container
        /// </summary>
        /// <returns>The child entity</returns>
        public abstract Entity CreateChild();

        /// <summary>
        /// Gets the first child entity with the specified name
        /// </summary>
        /// <param name="name">The name of the entity to find</param>
        /// <returns>An entity if it's found, otherwise null</returns>
        public Entity FindFirstChild(string name)
        {
            var matches = children.Where(entity => entity.Name == name);
            if (matches.Count() > 0)
                return matches.First();
            else
                return null;
        }

        /// <summary>
        /// Finds all the children with the specified tag
        /// </summary>
        /// <param name="tag">The tag</param>
        /// <returns>The children with the specified tag</returns>
        public Entity[] FindChildrenWithTag(object tag)
        {
            return children.Where(child => child.Tags.Contains(tag)).ToArray();
        }

        HashSet<DelayedAction> delayedActions = new HashSet<DelayedAction>();

        /// <summary>
        /// Clears all the delayed actions
        /// </summary>
        protected void ClearDelayedActions()
        {
            delayedActions.Clear();
        }

        /// <summary>
        /// Delays an action with this container, it will count time based on this container's update (e.g. will pause if container is paused)
        /// </summary>
        /// <param name="action">The action to delay</param>
        /// <param name="time">The amount of time to delay by</param>
        public void DelayAction(Action action, float time)
        {
            delayedActions.Add(new DelayedAction(action, time));
        }

        /// <summary>
        /// Process all actions attached to this container
        /// </summary>
        /// <param name="application">The application</param>
        protected void ProcessActions(Application application)
        {
            foreach (var action in delayedActions.ToArray())
            {
                if (action.ElapsedTime < action.TotalTime)
                    action.ElapsedTime += application.DeltaTime;
                else
                {
                    action.Action();
                    delayedActions.Remove(action);
                }
            }
        }

        /// <summary>
        /// Finds all the descendants with the specified tag
        /// </summary>
        /// <param name="tag">The tag</param>
        /// <returns>The descendants with the specified tag</returns>
        public Entity[] FindDescendantsWithTag(object tag)
        {
            return Descendants.Where(descendant => descendant.Tags.Contains(tag)).ToArray();
        }
    }
}
