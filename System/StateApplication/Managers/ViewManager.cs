namespace VorliasEngine2D.System
{
    public class ViewManager : GameCollectionService<GameView>
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
