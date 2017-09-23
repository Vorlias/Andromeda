using System.Collections.Generic;
using Andromeda2D.Events;

namespace Andromeda2D.System
{
    /// <summary>
    /// GameState interface
    /// </summary>
    public interface IGameState
    {
        IEnumerable<GameView> ActiveViewsByPriority { get; }
        StateApplication Application { get; }
        ViewEvents Events { get; }
        bool HasStarted { get; }
        UserInputManager Input { get; }
        bool IsTempState { get; }
        string Name { get; }
        StateManager StateManager { get; }
        IEnumerable<GameView> UpdatableViewsByPriority { get; }
        IEnumerable<GameView> Views { get; }

        void Activate();
        GameView FindFirstView(string name);
        ViewType FindFirstView<ViewType>() where ViewType : GameView;
        IEnumerable<GameView> GetViewsByName(string viewName);
        IEnumerable<ViewType> GetViewsByType<ViewType>() where ViewType : GameView;
        void Initialize();
        void Render();
        void Update();
    }
}