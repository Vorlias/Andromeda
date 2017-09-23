using Andromeda2D.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Events;
using Andromeda2D.System.Internal;
using Andromeda2D.System.Utility;

namespace Andromeda.System.StateApplication
{
    /// <summary>
    /// A game state that only holds views while it's running
    /// </summary>
    public abstract class LightGameState : GameState
    {
        protected abstract void InitializeViews();
        protected abstract void OnLightDeactivated();
        protected abstract void OnLightActivated();

        public sealed override void OnActivated()
        {
            InitializeViews();
            Views.ForEach(view => view.Start());
            OnLightActivated();
        }

        public sealed override void OnDeactivated()
        {
            OnLightDeactivated();
            ClearAllViews();
        }

        internal override void Start()
        {
            Initialize();
            //Views.ForEach(view => view.Start());
        }
    }
}
