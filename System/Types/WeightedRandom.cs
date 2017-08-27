using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorlias2D.System
{

    public class WeightedRandom<WeightedItem>
    {
        private struct WeightedRandomItem
        {
            public WeightedItem Item { get; set; }
            public float Weight { get; set; }
        }

        List<WeightedRandomItem> items = new List<WeightedRandomItem>();
        float totalWeight;
        float maxWeight = -1.0f;

        public float MaxWeight
        {
            get => maxWeight;
        }

        /// <summary>
        /// Alias of Next()
        /// </summary>
        public WeightedItem Random
        {
            get
            {
                return Next();
            }
        }

        static Random random = new Random();

        double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Gets the next weighted random item
        /// </summary>
        /// <returns>The next weighted random item</returns>
        public WeightedItem Next()
        {
            float total = 0;

            foreach (var item in items)
            {
                total += item.Weight;
            }

            float result = 0;

            if (total != 0)
            {
                result = (float) GetRandomNumber(0.0, total);
            }

            foreach (var item in items)
            {
                if (result < item.Weight)
                    return item.Item;

                result -= item.Weight;
            }

            throw new IndexOutOfRangeException("No items set for WeightedRandom!");
        }

        public float TotalWeight
        {
            get => totalWeight;
        }

        public WeightedRandomType Type
        {
            get;
        }

        public WeightedRandom(WeightedRandomType type = WeightedRandomType.Numerical, float maxWeight = 0.0f)
        {
            Type = type;
            this.maxWeight = maxWeight;
        }

        public static implicit operator WeightedItem(WeightedRandom<WeightedItem> weightedRandom)
        {
            return weightedRandom.Random;
        }

        public void AddItem(WeightedItem item, float weight)
        {
            if (Type == WeightedRandomType.Numerical || (totalWeight + weight) <= maxWeight)
            {
                WeightedRandomItem wi = new WeightedRandomItem()
                {
                    Item = item,
                    Weight = weight
                };

                totalWeight += weight;
                items.Add(wi);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Weighted random items exceed maximum of " + maxWeight + " (weight: " + (totalWeight + weight) + ")");
            }
        }
    }
}
