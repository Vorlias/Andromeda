using Andromeda.Entities.Components;
using SFML.Graphics;
using SFML.Window;

namespace Andromeda.System
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

        void ProcessInput(Application application, Mouse.Button button, InputState state);
        void ProcessInput(Application application, Keyboard.Key key, InputState state);

        void Added(StateGameManager manager, string id);
    }
}