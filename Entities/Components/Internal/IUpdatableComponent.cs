using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components.Internal
{
    /// <summary>
    /// The priority of updates in the game update queue
    /// </summary>
    public enum UpdatePriority
    {
        /// <summary>
        /// Updates before everything else
        /// </summary>
        First = 1,

        /// <summary>
        /// Updates when physics does
        /// </summary>
        Physics = 5,

        /// <summary>
        /// Updates when the camera does
        /// </summary>
        Camera = 10,

        

        /// <summary>
        /// Updates when the interfaces do
        /// </summary>
        Interface = 50,

        /// <summary>
        /// Updates at normal
        /// </summary>
        Normal = 100,

        /// <summary>
        /// Updates last, after everything else.
        /// </summary>
        Last = 1000,
    }

    public interface IUpdatableComponent : IComponent
    {
        UpdatePriority UpdatePriority
        {
            get;
        }

        void Update();
        void AfterUpdate();
        void BeforeUpdate();
    }
}
