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
        private EditText inputred, inputblue, inputyellow, inputpink, inputgreen, inputwhite;
        private Randomizer randomizer;

        //for betting
        private ImageButton Image_Button1, Image_Button2, Image_Button3, Image_button4, Image_button5, Image_button6;

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

            //betting colors initialize
            Image_Button1 = FindViewById<ImageButton>(Resource.Id.Red_Button);
            Image_Button2 = FindViewById<ImageButton>(Resource.Id.Blue_Button);
            Image_Button3 = FindViewById<ImageButton>(Resource.Id.Yellow_Button);

            Image_button4 = FindViewById<ImageButton>(Resource.Id.Pink_Button);
            Image_button5 = FindViewById<ImageButton>(Resource.Id.Green_Button);
            Image_button6 = FindViewById<ImageButton>(Resource.Id.White_Button);

            //set Colors
            Image_Button1.SetImageResource(Resource.Drawable.red);
            Image_Button2.SetImageResource(Resource.Drawable.blue);
            Image_Button3.SetImageResource(Resource.Drawable.Yellow);

            Image_button4.SetImageResource(Resource.Drawable.pink);
            Image_button5.SetImageResource(Resource.Drawable.green);
            Image_button6.SetImageResource(Resource.Drawable.white);

            // Initialize Inputs colors
            inputred = FindViewById<EditText>(Resource.Id.Inputfor_red);
            inputblue = FindViewById<EditText>(Resource.Id.Inputfor_blue);
            inputyellow = FindViewById<EditText>(Resource.Id.Inputfor_yellow);

            inputpink = FindViewById<EditText>(Resource.Id.Inputfor_pink);
            inputgreen = FindViewById<EditText>(Resource.Id.Inputfor_green);
            inputwhite = FindViewById<EditText>(Resource.Id.Inputfor_white);

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
            if (ValidateAndSetBets())
            {
                if (randomizer.ValidateBets(moneybalance))
                {
                    timer.Start();
                }
                else
                {
                    Toast.MakeText(this, "You cannot bet more than your current balance.", ToastLength.Short).Show();
                }
            }
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

        private bool ValidateAndSetBets()
        {
            int redBet = GetBetAmount(inputred);
            int blueBet = GetBetAmount(inputblue);
            int yellowBet = GetBetAmount(inputyellow);
            int pinkBet = GetBetAmount(inputpink);
            int greenBet = GetBetAmount(inputgreen);
            int whiteBet = GetBetAmount(inputwhite);

            int totalBet = redBet + blueBet + yellowBet + pinkBet + greenBet + whiteBet;

            if (totalBet > moneybalance)
            {
                Toast.MakeText(this, "You cannot bet more than your current balance.", ToastLength.Short).Show();
                return false;
            }

            randomizer.SetBets(redBet, blueBet, yellowBet, pinkBet, greenBet, whiteBet);
            return true;
        }

        private int GetBetAmount(EditText input)
        {
            int bet;
            if (int.TryParse(input.Text, out bet) && bet >= 0)
            {
                return bet;
            }
            else
            {
                Toast.MakeText(this, "Invalid bet amount. Please enter a valid number.", ToastLength.Short).Show();
                return 0;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
