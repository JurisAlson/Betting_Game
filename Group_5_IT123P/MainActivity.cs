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
    [Activity(Label = "Color Game", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button RollButton, StopButton;
        ImageView Color1, Color_2, Color_3;

        Randomizer randomizer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize
            RollButton = FindViewById<Button> (Resource.Id.Roll_Button);
            StopButton = FindViewById<Button> (Resource.Id.Stop_Button);
            Color1 = FindViewById<ImageView>  (Resource.Id.randColor_1);
            Color_2 = FindViewById<ImageView> (Resource.Id.randColor_2);
            Color_3 = FindViewById<ImageView> (Resource.Id.randColor_3);

            // Initialize randomizer
            randomizer = new Randomizer(new List <int> { 
            
                Resource.Drawable.blue,
                Resource.Drawable.green


      
            });

            // Roll button click event
            RollButton.Click += RollButton_Click;

            // Stop button click event
            StopButton.Click += StopButton_Click;
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            // Randomize images
            randomizer.RandomizeImages(Color1, Color_2, Color_3);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            //tanginamo
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}