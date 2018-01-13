using Andromeda.Entities.Components;
using Andromeda.Entities.Components.Internal;
using Andromeda.System;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.UILayoutComponents
{
    public class UIListLayout : UIComponent, ISortableInterfaceLayout
    {
        Vector2f padding = new Vector2f(0, 0);

        public Vector2f Padding
        {
            get => padding;
            set => padding = value;
        }

        UIListItemAlignment alignment = UIListItemAlignment.Left;
        public UIListItemAlignment ListItemAlignment
        {
            get => alignment;
            set => alignment = value;
        }

        UIListFillDirection fillDirection = UIListFillDirection.Vertical;
        public UIListFillDirection FillDirection
        {
            get => fillDirection;
            set => fillDirection = value;
        }

        bool sizeToContents = true;
        public bool SizeToContents
        {
            get => sizeToContents;
            set => sizeToContents = value;
        }

        LayoutSortOrder order = LayoutSortOrder.InstanceOrder;
        public LayoutSortOrder LayoutSortOrder
        {
            get => order;
            set => order = value;
        }

        Func<UIComponent, bool> sort;
        public Func<UIComponent, bool> CustomSortFunction
        {
            get => sort;
            set => sort = value;
        }

        public override void AfterUpdate()
        {
            Transform.LocalSize = new UICoordinates(0, maxSizeX, 0, maxSizeY);
        }

        public UIText AddText(string text, uint fontSize, Color color,
            TextYAlignment yAlignment = TextYAlignment.Center, TextXAlignment xAlignment = TextXAlignment.Left
            )
        {
            UIText obj = Add<UIText>();
            obj.TextYAlignment = yAlignment;
            obj.TextXAlignment = xAlignment;
            obj.Color = color;
            obj.Text = text;
            obj.FontSize = fontSize;

            return obj;
        }

        public UIListLayout AddListLayout(Vector2f padding = default(Vector2f), UIListItemAlignment uiListItemAlignment = UIListItemAlignment.Left)
        {
            var uiListLayout = Add<UIListLayout>();
            uiListLayout.padding = padding;
            uiListLayout.ListItemAlignment = uiListItemAlignment;
            return uiListLayout;
        }

        void Test()
        {

        }

        public UIComponentType Add<UIComponentType>() where UIComponentType : UIComponent, new()
        {
            Entity child = Entity.CreateChild();
            return child.AddComponent<UIComponentType>();
        }

        public override void BeforeUpdate()
        {

        }

        float maxSizeX = 0;
        float maxSizeY = 0;

        public IEnumerable<UIComponentType> ItemsOfType<UIComponentType>() where UIComponentType : UIComponent
        {
            return Items.OfType<UIComponentType>();
        }

        public IEnumerable<UIComponent> Items
        {
            get => Entity.GetComponentsInChildren<UIComponent>().Where(com => com.Visible);
        }

        public override void Update()
        {
            var uiChildren = Entity.GetComponentsInChildren<UIComponent>().Where(com => com.Visible);

            if (FillDirection == UIListFillDirection.Vertical)
            {

                float offsetY = padding.Y;
                foreach (var child in uiChildren)
                {
                    child.Transform.LocalPosition = new UICoordinates(0, padding.X, 0, offsetY);
                    offsetY += child.Size.GlobalAbsolute.Y + padding.Y;

                    if (sizeToContents)
                    {
                        maxSizeX = Math.Max(child.Size.GlobalAbsolute.X + (padding.X * 2), maxSizeX);
                    }

                }

                maxSizeY = offsetY;

                if (ListItemAlignment != UIListItemAlignment.Left)
                {
                    var center = maxSizeX / 2;
                    foreach (var child in uiChildren)
                    {
                        var globalSize = child.Size.GlobalAbsolute;
                        var localPosition = child.Transform.LocalPosition.GlobalAbsolute;

                        if (ListItemAlignment == UIListItemAlignment.Center)
                            child.Transform.LocalPosition = new UICoordinates(0, center - (globalSize.X / 2), 0, localPosition.Y);
                        else if (ListItemAlignment == UIListItemAlignment.Right)
                            child.Transform.LocalPosition = new UICoordinates(0, maxSizeX - globalSize.X - Padding.X, 0, localPosition.Y);
                    }
                }

            }
            else if (FillDirection == UIListFillDirection.Horizontal)
            {
                float offsetX = padding.X;
                foreach (var child in uiChildren)
                {
                    child.Transform.LocalPosition = new UICoordinates(0, offsetX, 0, padding.Y);
                    offsetX += child.Size.GlobalAbsolute.X + padding.X;

                    if (sizeToContents)
                    {
                        maxSizeY = Math.Max(maxSizeY, child.Size.GlobalAbsolute.Y + (padding.Y * 2));
                    }

                }

                maxSizeX = offsetX;
            }
        }
    }
}
