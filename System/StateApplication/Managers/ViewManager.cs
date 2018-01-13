namespace Andromeda2D.System
{
    public class ViewManager : GameCollectionService<EntityGameView>
    {
        public StateManager States
        {
            get;
        }

        public ViewManager(StateGameManager parent, StateManager states) : base(parent)
        {
            States = states;
        }
    }
}
