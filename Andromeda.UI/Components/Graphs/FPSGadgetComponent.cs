using Andromeda.Entities;
using Andromeda.Entities.Components;
using Andromeda.System;
using SFML.Graphics;
using System;

namespace Andromeda.UI.Components
{
    public class FPSGadgetComponent : NumberBarGraphComponent
    {
        public float FPS => StateApplication.Application.FPS;

        public FPSGadgetComponent()
        {
            PrimaryColor = new Color(111, 128, 55);
            SecondaryColor = new Color(55, 64, 28);
        }

        public override void OnUpdateFrame()
        {
            AddValue(FPS);
            Entity.GetComponent<UIText>().Text = Math.Ceiling(Average).ToString() + "FPS";
        }
    }
}
