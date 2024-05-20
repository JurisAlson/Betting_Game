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
        private Dictionary<int, int> colorBets;

        public Randomizer(List<int> imageResources)
        {
            this.imageResources = imageResources;
            this.colorConditions = InitializeColorConditions();
            this.colorBets = new Dictionary<int, int>();
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

        //Conditions for colorsss
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

        public void SetBets(int redBet, int blueBet, int yellowBet, int pinkBet, int greenBet, int whiteBet)
        {

            //sets the betting colors
            colorBets[Resource.Drawable.red] = redBet;
            colorBets[Resource.Drawable.blue] = blueBet;
            colorBets[Resource.Drawable.Yellow] = yellowBet;
            colorBets[Resource.Drawable.pink] = pinkBet;
            colorBets[Resource.Drawable.green] = greenBet;
            colorBets[Resource.Drawable.white] = whiteBet;
        }

        public bool ValidateBets(int balance)
        {
            int totalBet = 0;
            foreach (var bet in colorBets.Values)
            {
                totalBet += bet;
            }

            if (totalBet > balance)
            {
                return false;
            }

            return true;
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
