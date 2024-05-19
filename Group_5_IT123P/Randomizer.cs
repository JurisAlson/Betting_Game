using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System.Collections.Generic;
using System.Timers;
using System;

namespace Group_5_IT123P
{
    public class Randomizer
    {
        private List<int> imageResources;
        private System.Random random;
        private Dictionary<int, ColorCondition> colorConditions;

        public Randomizer(List<int> imageResources)
        {
            this.imageResources = imageResources;
            this.colorConditions = InitializeColorConditions();
            random = new System.Random();
        }

        private Dictionary<int, ColorCondition> InitializeColorConditions()
        {
            return new Dictionary<int, ColorCondition>
            {
                { Resource.Drawable.blue, new BlueColorCondition() },
                { Resource.Drawable.green, new GreenColorCondition() },
                { Resource.Drawable.red, new RedColorCondition() },
                { Resource.Drawable.white, new WhiteColorCondition() },
                { Resource.Drawable.Yellow, new YellowColorCondition() }
            };
        }

        public void RandomizeImages(params ImageView[] imageViews)
        {
            foreach (var imageView in imageViews)
            {
                int index = random.Next(imageResources.Count);
                int resource = imageResources[index];
                imageView.SetImageResource(resource);
                imageView.Tag = resource; // Set tag to keep track of the current resource ID
            }
        }

        public void ApplyConditions(ref int balance, params ImageView[] imageViews)
        {
            foreach (var imageView in imageViews)
            {
                if (imageView.Tag != null)
                {
                    int resourceId = (int)imageView.Tag;
                    if (colorConditions.ContainsKey(resourceId))
                    {
                        colorConditions[resourceId].ApplyCondition(ref balance);
                    }
                }
            }
        }

        // Base class for color conditions
        //trial lang conditions kung tama logic
        public abstract class ColorCondition
        {
            public abstract void ApplyCondition(ref int balance);
        }

        public class BlueColorCondition : ColorCondition
        {
            public override void ApplyCondition(ref int balance)
            {
                balance += 10;
            }
        }

        public class GreenColorCondition : ColorCondition
        {
            public override void ApplyCondition(ref int balance)
            {
                balance += 5;
            }
        }

        public class RedColorCondition : ColorCondition
        {
            public override void ApplyCondition(ref int balance)
            {
                balance -= 5;
            }
        }

        public class WhiteColorCondition : ColorCondition
        {
            public override void ApplyCondition(ref int balance)
            {
                balance += 2;
            }
        }

        public class YellowColorCondition : ColorCondition
        {
            public override void ApplyCondition(ref int balance)
            {
                balance -= 3;
            }
        }
    }
}
