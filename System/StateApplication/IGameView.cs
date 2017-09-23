using Andromeda2D.Entities.Components;
using SFML.Graphics;

namespace Andromeda2D.System
{
    public interface IGameView
    {
        string Id { get; }
        UserInputManager Input { get; }
        bool IsActive { get; set; }
        IGameState ParentState { get; }
        GameViewPriority Priority { get; set; }

        void Render(RenderWindow window);
        void Update(StateApplication application);
        void Reset();
        void Start();

        void SetParentState(IGameState state);
    }
}