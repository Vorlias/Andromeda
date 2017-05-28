using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System.Debug
{
    public static class DebugExtension
    {
        public static void DebugInstanceTree(this IInstanceTree state, int level = 0, string prefix = " ")
        {
            if (state is GameState)
                Console.WriteLine(" ■ GameState");
            else if (state is Entity && level == 0)
            {
                Console.WriteLine(" ■ " + (state as Entity).Name);
            }

            Entity[] children = state.GetChildren();
            foreach (Entity child in children)
            {
                Console.WriteLine(prefix + "└─■≡ " + child.Name);

                child.Components.ForEach(component => {
                    if (component is Transform)
                    {
                        var transform = component as Transform;
                        Console.WriteLine(prefix + "  │└≡[Transform " + transform.Position + " Rot(" + transform.Rotation + ")]");
                    }
                    else if (component is SpriteRenderer)
                    {
                        var renderer = component as SpriteRenderer;
                        Console.WriteLine(prefix + "  │└≡[SpriteRenderer `" + renderer.TextureId + "`]");
                    }
                    else if (component is EntityBehaviour)
                    {
                        Console.WriteLine(prefix + "  │└≡[EntityBehaviour `" + component.GetType().Name + "`]");
                    }
                });

                child.DebugInstanceTree(level + 1, prefix + "  ");
            }
        }
    }
}
