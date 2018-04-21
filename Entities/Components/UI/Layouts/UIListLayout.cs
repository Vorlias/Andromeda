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
        public override UpdatePriority UpdatePriority => UpdatePriority.Interface + 1;

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
        /// <summary>
        /// The fill direction of the UIListLayout
        /// </summary>
        public UIListFillDirection FillDirection
        {
            get => fillDirection;
            set => fillDirection = value;
        }

        bool sizeToContents = true;
        /// <summary>
        /// Whether or not the UIListLayout will change size to fit contents
        /// </summary>
        public bool SizeToContents
        {
            get => sizeToContents;
            set => sizeToContents = value;
        }

        LayoutSortOrder order = LayoutSortOrder.InstanceOrder;
        /// <summary>
        /// The sort order of this UIListLayout
        /// </summary>
        public LayoutSortOrder LayoutSortOrder
        {
            get => order;
            set => order = value;
        }

        Func<UIComponent, bool> sort;
        /// <summary>
        /// The custom sort function
        /// </summary>
        public Func<UIComponent, bool> CustomSortFunction
        {
            get => sort;
            set => sort = value;
        }

        public override void AfterUpdate()
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


            Transform.LocalSize = new UICoordinates(0, maxSizeX, 0, maxSizeY);
        }

        /// <summary>
        /// Add text to this UIListLayout
        /// </summary>
        /// <param name="text">The text of this UIListLayout</param>
        /// <param name="fontSize">The size of the text</param>
        /// <param name="color">The color of the text</param>
        /// <param name="yAlignment">The Y alignment of the text</param>
        /// <param name="xAlignment">The X alignment of the text</param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a child list layout
        /// </summary>
        /// <param name="padding">The padding of the child list layout</param>
        /// <param name="uiListItemAlignment">The alignment of the child list layout relative to the parent</param>
        /// <returns></returns>
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

        /// <summary>
        /// The items in this UIListLayout
        /// </summary>
        public IEnumerable<UIComponent> Items
        {
            get => Entity.GetComponentsInChildren<UIComponent>().Where(com => com.Visible);
        }

        public override void Update()
        {

        }
    }
}
