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

namespace hduhelp.Model.ControlModel
{
    public sealed partial class NewSemesterCountdownControl : UserControl
    {
        private DispatcherTimer _timer = null;
        private TimeSpan _time;
        private DateTime _termStart;

        public NewSemesterCountdownControl()
        {
            this.InitializeComponent();
        }

        private void NewSemesterCountdownControl_Loaded(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(10000000);
            _timer.Tick += Timer_Tick;

            _termStart = new DateTime(2015, 3, 8, 0, 0, 0);
            _time = _termStart - DateTime.Now;
            _timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            TbDay.Text = _time.Days.ToString("00");
            TbHour.Text = _time.Hours.ToString("00");
            TbMinute.Text = _time.Minutes.ToString("00");
            TbSecond.Text = _time.Seconds.ToString("00");
            _time = _termStart - DateTime.Now;
        }
    }
}
