using Andromeda2D.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Events;
using Andromeda2D.System.Internal;

namespace Andromeda.System.StateApplication
{
    public class LightGameState : IGameState
    {


        public IEnumerable<GameView> ActiveViewsByPriority => throw new NotImplementedException();

        public Andromeda2D.System.StateApplication Application => throw new NotImplementedException();

        public ViewEvents Events => throw new NotImplementedException();

        public bool HasStarted => throw new NotImplementedException();

        public UserInputManager Input => throw new NotImplementedException();

        public bool IsTempState => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public StateManager StateManager => throw new NotImplementedException();

        public IEnumerable<GameView> UpdatableViewsByPriority => throw new NotImplementedException();

        public IEnumerable<GameView> Views => throw new NotImplementedException();

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public GameView FindFirstView(string name)
        {
            throw new NotImplementedException();
        }

        public ViewType FindFirstView<ViewType>() where ViewType : GameView
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameView> GetViewsByName(string viewName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ViewType> GetViewsByType<ViewType>() where ViewType : GameView
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
