using Andromeda.System;
using SFML.Graphics;
using SFML.System;

namespace Andromeda.Entities.Components.Internal
{
    /// <summary>
    /// A component that is used by a UserInterface
    /// </summary>
    interface IInterfaceComponent : IRenderableComponent, IComponent, IUpdatableComponent
    {
        int ZIndex { get; set; }
        bool Visible { get; set; }
        UITransform Transform { get; }
        Color Color { get; set; }

        Vector2f AbsoluteSize { get; }
        Vector2f AbsolutePosition { get; }

        UICoordinates Position { get; set; }
        UICoordinates Size { get; set; }

        void Update();
        void AfterUpdate();
        void BeforeUpdate();

    }
}
