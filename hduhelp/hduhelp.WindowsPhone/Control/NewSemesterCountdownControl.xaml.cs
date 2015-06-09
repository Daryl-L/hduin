using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace hduhelp.Phone.Control
{
    public sealed partial class NewSemesterCountdownControl : UserControl
    {
        private DispatcherTimer timer = null;
        private TimeSpan time;
        private DateTime termStart;

        public NewSemesterCountdownControl()
        {
            this.InitializeComponent();
        }

        private void NewSemesterCountdownControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(10000000);
            timer.Tick += Timer_Tick;

            termStart = new DateTime(2015, 3, 8, 0, 0, 0);
            time = termStart - DateTime.Now;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            tbDay.Text = time.Days.ToString("00");
            tbHour.Text = time.Hours.ToString("00");
            tbMinute.Text = time.Minutes.ToString("00");
            tbSecond.Text = time.Seconds.ToString("00");
            time = termStart - DateTime.Now;
        }
    }
}
