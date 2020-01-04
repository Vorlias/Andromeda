namespace Andromeda.Resources
{
    public interface ILoadableResource : IResource
    {
        string Id { get; }
        void Load();
    }
}
