using VorliasEngine2D.Entities;
using VorliasEngine2D.System.Internal;

namespace VorliasEngine2D.System
{
    public static class DestroyableExtension
    {
        public static void Destroy(this IDestroyable destroyable, float time)
        {
            DestructionService.Instance.Add(destroyable, time);
        }
    }
}
