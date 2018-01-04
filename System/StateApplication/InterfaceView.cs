using Andromeda.Entities;
using Andromeda.Entities.Components;
using Andromeda.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    /// <summary>
    /// A GameView which has a UserInterface entity by default
    /// </summary>
    public abstract class InterfaceView : EntityGameView
    {
        UserInterface ui;
        public UserInterface UI
        {
            get => ui;
        }

        public abstract void OnInterfaceStart(UserInterface ui);

        public override void OnStart()
        {
            Entity uiEntity = new Entity(this);
            ui = uiEntity.AddComponent<UserInterface>();
            uiEntity.Name = "Interface";

            OnInterfaceStart(ui);
        }
    }
}
