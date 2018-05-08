using Andromeda.Entities;
using Andromeda.Entities.Components;
using Andromeda.Entities.Components.Internal;
using Andromeda.System;
using Andromeda.System.Types;
using Andromeda.System.Utility;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.UI.Components
{
    [RequireComponents(typeof(UIText))]
    public abstract class NumberBarGraphComponent : UIComponent, IRenderableComponent, IUpdatableComponent
    {
        public UpdatePriority UpdatePriority => UpdatePriority.Normal;

        float _maximum;
        List<float> _numbers = new List<float>();
        RenderTexture _barGraphTexture;
        ColorRange _color = new Color(200, 200, 200);
        ColorRange _colorSecondary = new Color(150, 150, 150);

        public float Average
        {
            get;
            private set;
        }

        public float Maximum
        {
            get => _maximum;
        }

        public ColorRange PrimaryColor
        {
            get => _color;
            set => _color = value;
        }

        public ColorRange SecondaryColor
        {
            get => _colorSecondary;
            set => _colorSecondary = value;
        }

        public UICoordinates GraphSize
        {
            get
            {
                return Transform.LocalSize;
            }
            set
            {
                var abs = value.GlobalAbsolute.ToInt();
                _barGraphTexture = new RenderTexture((uint)abs.X, (uint)abs.Y);
                Transform.LocalSize = value;
            }
        }

        protected override void OnComponentInit(Entity entity)
        {
            GraphSize = new UICoordinates(0, 150, 0, 25);

        }

        protected void AddValue(float value)
        {
            if (value > Maximum)
            {
                _maximum = value;
            }

            _numbers.Add(value);
            if (_numbers.Count > (int)(Transform.LocalSize.GlobalAbsolute.X / 4))
            {
                _numbers.RemoveAt(0);
            }
        }

        public override void AfterUpdate()
        {

        }

        public override void BeforeUpdate()
        {

        }

        public override sealed void Draw(RenderTarget target, RenderStates states)
        {
            _barGraphTexture.Clear(new Color(30, 30, 30, 100));
            int size = _numbers.Count;

            float average = 0;
            if (size > 0)
            {
                int i = 0;

                for (i = 0; i < size; i++)
                {
                    RectangleShape rs2 = new RectangleShape();
                    rs2.FillColor = i % 2 == 0 ? _color.Lerp(i / (float)(size / 2)) : _colorSecondary.Lerp(i / (float)(size / 2));
                    float ySize = (_numbers[i] / Maximum) * _barGraphTexture.Size.Y * 0.95f;

                    rs2.Size = new Vector2f(_barGraphTexture.Size.X / (int)(Size.GlobalAbsolute.X / 4), -ySize);

                    float xPos = (_barGraphTexture.Size.X / size) * i;

                    rs2.Position = new Vector2f(xPos, ySize);
                    _barGraphTexture.Draw(rs2);

                    average += _numbers[i];
                }

                Average = average / size;
            }

            RectangleShape rs = new RectangleShape();
            rs.FillColor = new Color(0, 0, 0, 50);
            rs.Size = _barGraphTexture.Size.ToFloat() + new Vector2f(2, 2);
            rs.Position = Position.GlobalAbsolute - new Vector2f(1, 1);
            target.Draw(rs);

            Sprite renderSprite = new Sprite(_barGraphTexture.Texture);
            renderSprite.Position = Position.GlobalAbsolute;

            target.Draw(renderSprite);


        }

        float DeltaTime => StateApplication.Application.DeltaTime;


        float lastFrame = 0;
        float targetSeconds = 1 / 5;

        public float FramesPerSecond
        {
            get => targetSeconds;
            set => targetSeconds = value;
        }

        public virtual void OnUpdateFrame()
        {

        }

        public override void Update()
        {
            if (lastFrame < targetSeconds)
            {
                lastFrame += DeltaTime;
            }
            else
            {
                OnUpdateFrame();
                lastFrame = 0;
            }
        }
    }
}
