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
        private Timer timer;
        private EditText Money;
        private Button RollButton, StopButton;
        private ImageView Color_1, Color_2, Color_3;
        private Randomizer randomizer;

        private int moneybalance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize 
            RollButton = FindViewById<Button>(Resource.Id.Roll_Button);
            StopButton = FindViewById<Button>(Resource.Id.Stop_Button);
            Money = FindViewById<EditText>(Resource.Id.money);
            Color_1 = FindViewById<ImageView>(Resource.Id.randColor_1);
            Color_2 = FindViewById<ImageView>(Resource.Id.randColor_2);
            Color_3 = FindViewById<ImageView>(Resource.Id.randColor_3);

            // Initialize randomizer
            randomizer = new Randomizer(new List<int>
            {
                Resource.Drawable.blue,
                Resource.Drawable.green,
                Resource.Drawable.red,
                Resource.Drawable.white,
                Resource.Drawable.Yellow
            });

            timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;

            RollButton.Click += RollButton_Click;
            StopButton.Click += StopButton_Click;

            // Initialize money balance
            moneybalance = 100;
            UpdateBalance();
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            randomizer.ApplyConditions(ref moneybalance, Color_1, Color_2, Color_3);
            UpdateBalance();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                try
                {
                    randomizer.RandomizeImages(Color_1, Color_2, Color_3);
                }
                catch (Exception)
                {
                    Toast.MakeText(this, "Failed to randomize images.", ToastLength.Short).Show();
                }
            });
        }

        private void UpdateBalance()
        {
            Money.Text = moneybalance.ToString();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
