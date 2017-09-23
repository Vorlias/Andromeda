using SFML.Graphics;
using SFML.System;
using System;
using Andromeda2D.Entities;
using Andromeda2D.Entities.Components;
using Andromeda2D.System.Types;
using Andromeda2D.System.Utility;

namespace Andromeda2D.System.Debug
{
    public static class DebugRenderer
    {
#if DEBUG
        public const bool DEBUG_RENDERING_DEFAULT = true;
#else
        public const bool DEBUG_RENDERING_DEFAULT = false;
#endif

        public static bool DebugRenderingEnabled = DEBUG_RENDERING_DEFAULT;
    }

    public static class DebugExtension
    {
        public static void DebugRender(this EntityGameView view, RenderTarget target)
        {
#if DEBUG
            if (DebugRenderer.DebugRenderingEnabled)
                foreach (var child in view.Descendants)
                {
                    var pos = child.Transform.Position;
                    var rot = child.Transform.Rotation;

                    if (child.IsBeingDestroyed)
                    {
                        CircleShape cs = new CircleShape(5);
                        cs.Position = pos - new Vector2f(-2.5f, -2.5f);
                        cs.FillColor = new Color(255, 0, 0, 50);
                        target.Draw(cs);
                    }

                    UITransform uiTransform;
                    if (child.FindComponent(out uiTransform))
                    {
                        RectangleShape rs = new RectangleShape(uiTransform.Size);
                        rs.Position = pos;
                        rs.FillColor = Color.Transparent;
                        rs.OutlineColor = new Color(255, 255, 0, 100);
                        rs.OutlineThickness = 1;
                        target.Draw(rs);

                        Entities.Components.Transform parentTransform;
                        if (child.Parent != null && child.Parent.FindComponent(out parentTransform))
                        {
                            VertexArray yOffset = new VertexArray(PrimitiveType.LineStrip);
                            yOffset.Append(new Vertex(new Vector2f(pos.X, parentTransform.Position.Y), new Color(0, 255, 0, 50)));
                            yOffset.Append(new Vertex(pos, new Color(0, 255, 0, 50)));

                            target.Draw(yOffset);

                            VertexArray xOffset = new VertexArray(PrimitiveType.LineStrip);
                            xOffset.Append(new Vertex(new Vector2f(parentTransform.Position.X, pos.Y), new Color(255, 0, 0, 50)) );
                            xOffset.Append(new Vertex(pos, new Color(255, 0, 0, 50)));

                            target.Draw(xOffset);
                        }
                    }

                    /*if (child.FindComponent(out UIText text))
                    {
                        RectangleShape rs = new RectangleShape(text.Transform.Size);
                        rs.Position = text.Transform.Position;
                        rs.FillColor = Color.Transparent;
                        rs.OutlineColor = Color.Cyan;
                        rs.OutlineThickness = 1;
                        target.Draw(rs);
                    }*/

                    VertexArray transformLeft = new VertexArray(PrimitiveType.LineStrip);
                    transformLeft.Append(new Vertex(pos, Color.Red));
                    transformLeft.Append(new Vertex(pos + new Vector2f(15, 0).Rotate(rot), Color.Red));
                    target.Draw(transformLeft);

                    VertexArray transformUp = new VertexArray(PrimitiveType.LineStrip);
                    transformUp.Append(new Vertex(pos, Color.Green));
                    transformUp.Append(new Vertex(pos + new Vector2f(0, 15).Rotate(rot), Color.Green));
                    target.Draw(transformUp);


                }
#endif
        }

        public static void DebugRender(this Camera camera, RenderTarget target)
        {
#if DEBUG
            CircleShape cameraPositionRect = new CircleShape();
            cameraPositionRect.FillColor = Color.Magenta;
            cameraPositionRect.Origin = new Vector2f(5, 5);
            cameraPositionRect.Position = camera.WorldPosition;
            cameraPositionRect.Radius = 5; // new Vector2f(10, 10);

            CircleShape originPositionRect = new CircleShape();
            originPositionRect.FillColor = Color.Cyan;
            originPositionRect.Origin = new Vector2f(5, 5);
            originPositionRect.Position = camera.WorldZeroPosition;
            originPositionRect.Radius = 5; //new Vector2f(10, 10);

            target.Draw(cameraPositionRect);
            target.Draw(originPositionRect);

            VertexArray verts = new VertexArray();
            verts.PrimitiveType = PrimitiveType.LineStrip;
            verts.Append(new Vertex(camera.WorldPosition, Color.Magenta));
            verts.Append(new Vertex(camera.WorldZeroPosition, Color.Cyan));
            target.Draw(verts);
#endif
        }

        /// <summary>
        /// Draws the polygon collider
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="target"></param>
        /// <param name="renderColor"></param>
        public static void DebugRender(this IPolygonColliderComponent collider, RenderTarget target, Color renderColor)
        {
#if DEBUG
            Entities.Components.Transform transform = collider.Entity.Transform;

            Polygon polygon = collider.Polygon?.Transform(transform.Position, collider.Origin, transform.Rotation);

            if (polygon != null)
            { 
                VertexArray vert = polygon.ToVertexArray(renderColor);
   
                vert.PrimitiveType = PrimitiveType.LineStrip;

                target.Draw(vert);
            }
#endif
        }

        public static void DebugRender(this UserInterface intf, RenderTarget target)
        {
#if DEBUG
            var descendants = intf.Entity.Descendants;
            foreach (var descendant in descendants)
            {
                UITransform transform;
                if (descendant.FindComponent(out transform))
                {
                    RectangleShape uiRect = new RectangleShape(transform.Size.GlobalAbsolute);
                    uiRect.Position = transform.GlobalPosition.GlobalAbsolute;
                    uiRect.FillColor = new Color(255, 255, 255, 50);
                    uiRect.OutlineColor = new Color(0, 0, 0);
                    uiRect.OutlineThickness = 1;

                    target.Draw(uiRect);
                }
            }
#endif
        }


        public static void DebugInstanceTree(this Internal.IEntityContainer instance, int level = 0, string prefix = " ")
        {
#if DEBUG
            if (instance is EntityGameView)
                Console.WriteLine(" ■ Parent: " + instance);
            else if (instance is Entity && level == 0)
            {
                Console.WriteLine(" ■ " + (instance as Entity).Name);
            }

            Entity[] children = instance.Children;
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
#endif
        }
    }
}
