using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Timers;

namespace ClockDrawerNavigationbar
{
    public class StopwatchFragment : Fragment
    {
        Timer timer;
        TextView timeTextView;
        TimeSpan ticks;
        Button StartStopButton;
        Button ResetButton;

        public StopwatchFragment()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnElapsed;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Stopwatch, container, false);

            timeTextView = view.FindViewById<TextView>(Resource.Id.stopwatchTimeTextView);

            StartStopButton = view.FindViewById<Button>(Resource.Id.StartStopButton);
            StartStopButton.Click += OnStartStopClick;
            ResetButton = view.FindViewById<Button>(Resource.Id.ResetButton);
            ResetButton.Click += OnResetClick;

            return view;
        }

        void OnStartStopClick(object sender, EventArgs e)
        {
            if(StartStopButton.Text=="Start")
            {
                timer.Start();
                StartStopButton.Text = "Stop";
            }
            else
            {
                timer.Stop();
                StartStopButton.Text = "Start";
            }
        }

        void OnResetClick(object sender, EventArgs e)
        {
            ticks = TimeSpan.Zero;
            timer.Stop();
            StartStopButton.Text = "Start";
            timeTextView.Text = "0:00:00";
        }

        void OnElapsed(object sender, ElapsedEventArgs e)
        {
            ticks = ticks.Add(TimeSpan.FromSeconds(1));
            base.Activity.RunOnUiThread(()=>timeTextView.Text = ticks.ToString("g"));
        }
    }
}