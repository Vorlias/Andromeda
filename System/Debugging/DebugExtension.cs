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
        public static void DebugInstanceTree(this IInstanceTree state, string prefix = " ")
        {
            if (state is GameState)
                Console.WriteLine(" ■ GameState");
            else if (state is Entity)
            {
                //Entity parent = state as Entity;
                //Console.WriteLine(prefix + " * " + parent.Name);
            }

            Entity[] children = state.GetChildren();
            foreach (Entity child in children)
            {
                Console.WriteLine(prefix + "└─■≡ " + child.Name);

                if (child.HasComponent<Transform>())
                {
                    var transform = child.GetComponent<Transform>();
                    Console.WriteLine(prefix + "  │└≡[Transform " + transform.Position + " Rot(" + transform.Rotation + ")]");
                }
                    

                if (child.HasComponent<SpriteRenderer>())
                {
                    var renderer = child.GetComponent<SpriteRenderer>();
                    Console.WriteLine(prefix + "  │└≡[SpriteRenderer `" + renderer.TextureId + "`]");
                }


                if (child.HasComponent<EntityBehaviour>())
                {
                    child.Behaviours.ForEach(behaviour => Console.WriteLine(prefix + "  │└≡[EntityBehaviour `" + behaviour.GetType().Name + "`]"));
                }

                child.DebugInstanceTree(prefix + "  ");
            }
        }
    }
}
