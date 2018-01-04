using System.Collections.Generic;
using Andromeda.Events;
using SFML.System;
using System;

namespace Andromeda.System
{
    /// <summary>
    /// GameState interface
    /// </summary>
    public interface IGameState
    {
        IEnumerable<IGameView> ActiveViewsByPriority { get; }
        StateApplication Application { get; }

        bool HasStarted { get; }
        UserInputManager Input { get; }
        bool IsTempState { get; }
        string Name { get; }
        StateManager StateManager { get; }
        IEnumerable<IGameView> UpdatableViewsByPriority { get; }
        IEnumerable<IGameView> Views { get; }

        Vector2f MouseCenterDelta { get; }
        MouseConstraintType MouseConstraint { get; set; }

        void Activate();
        IGameView FindFirstView(string name);
        IEnumerable<IGameView> GetViewsByName(string viewName);
        IEnumerable<ViewType> GetViewsByType<ViewType>() where ViewType : IGameView;
        void Initialize();
        void Render();
        void Update();
    }
}