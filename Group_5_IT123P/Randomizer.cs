using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Provider.MediaStore;

namespace Group_5_IT123P
{
    public class Randomizer
    {
        private List<int> imageResources;
        private System.Random random;

        public Randomizer(List<int> imageResources)
        {
            this.imageResources = imageResources;
            random = new System.Random();
        }

        public void RandomizeImages(params ImageView[] imageViews)
        {
            foreach (var imageView in imageViews)
            {
                int index = random.Next(imageResources.Count);
                imageView.SetImageResource(imageResources[index]);
            }
        }
    }
}





