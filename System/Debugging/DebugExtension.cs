using SFML.Graphics;
using SFML.System;
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
        public static void DebugRender(this Camera camera, RenderTarget target)
        {
            RectangleShape cameraPositionRect = new RectangleShape();
            cameraPositionRect.FillColor = Color.Magenta;
            cameraPositionRect.Origin = new Vector2f(-5, -5);
            cameraPositionRect.Position = camera.WorldPosition;
            cameraPositionRect.Size = new Vector2f(10, 10);

            RectangleShape originPositionRect = new RectangleShape();
            originPositionRect.FillColor = Color.Cyan;
            originPositionRect.Origin = new Vector2f(-5, -5);
            originPositionRect.Position = camera.WorldZeroPosition;
            originPositionRect.Size = new Vector2f(10, 10);

            target.Draw(cameraPositionRect);
            target.Draw(originPositionRect);
        }

        /// <summary>
        /// Draws the polygon collider
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="target"></param>
        /// <param name="renderColor"></param>
        public static void DebugRenderPolygonCollider(this IPolygonColliderComponent collider, RenderTarget target, Color renderColor)
        {
            Entities.Components.Transform transform = collider.Entity.Transform;

            Polygon polygon = collider.Polygon.Transform(transform.Position, collider.Origin, transform.Rotation);
            VertexArray vert = polygon.ToVertexArray(renderColor);
   
            vert.PrimitiveType = PrimitiveType.LineStrip;

            target.Draw(vert);
        }

        public static void DebugInstanceTree(this IInstanceTree instance, int level = 0, string prefix = " ")
        {
            if (instance is GameView)
                Console.WriteLine(" ■ GameState");
            else if (instance is Entity && level == 0)
            {
                Console.WriteLine(" ■ " + (instance as Entity).Name);
            }

            Entity[] children = instance.GetChildren();
            foreach (Entity child in children)
            {
                Console.WriteLine(prefix + "└─■≡ " + child.Name);

                child.Components.ForEach(component => {
                    if (component is Entities.Components.Transform)
                    {
                        var transform = component as Entities.Components.Transform;
                        Console.WriteLine(prefix + "  │└≡[Transform " + transform.Position + " " + transform.LocalPosition + " [float] Rot(" + transform.Rotation + ")]");
                    }
                    else if (component is SpriteRenderer)
                    {
                        var renderer = component as SpriteRenderer;
                        Console.WriteLine(prefix + "  │└≡[SpriteRenderer `" + renderer.TextureId + "`]");
                    }
                    else if (component is EntityBehaviour)
                    {
                        Console.WriteLine(prefix + "  │└≡[" + component.GetType().Name + " (Script)]");
                    }
                    else if (component is ICollisionComponent)
                    {
                        Console.WriteLine(prefix + "  │└≡[" + component.GetType().Name + " (Collider)]");
                    }
                    else
                    {
                        Console.WriteLine(prefix + "  │└≡[" + component.ToString() + "]");
                    }
                });

                child.DebugInstanceTree(level + 1, prefix + "  ");
            }
        }
    }
}
