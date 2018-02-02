namespace Andromeda.System
{
    /// <summary>
    /// A service that's part of the application
    /// </summary>
    public abstract class ApplicationService
    {
        public Application Application { get; internal set; }

        public abstract void Started(Application application);
        public abstract void Updated(Application application);

        internal void Start(Application application)
        {
            Started(application);
        }
    }
}
