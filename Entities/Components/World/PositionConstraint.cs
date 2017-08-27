using SFML.System;

namespace Vorlias2D.Entities.Components
{
    public sealed class PositionConstraint
    {
        public Vector2f Min
        {
            get;
            set;
        }

        public Vector2f Max
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get
            {
                return (Max - Min) != new Vector2f(0, 0);
            }
        }

        public PositionConstraint()
        {
            Min = new Vector2f(0, 0);
            Max = new Vector2f(0, 0);
        }

        public PositionConstraint(Vector2f min, Vector2f max)
        {
            Min = min;
            Max = max;
        }
    }
}
