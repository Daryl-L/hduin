using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234235 上有介绍

namespace hduhelp.Controls
{
    public sealed class HolidayCountdownControl : Control
    {
        private DispatcherTimer _timer = null;
        private TimeSpan _time;
        private DateTime _termStart;
        private TextBlock _tbDay, _tbHour, _tbMinute, _tbSecond;
        //private Dictionary<string, DateTime> holiday;
        //private string recentHolidayName;

        public HolidayCountdownControl()
        {
            this.DefaultStyleKey = typeof(HolidayCountdownControl);
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(10000000);
            _timer.Tick += Timer_Tick;

            _termStart = new DateTime(2015, 3, 8, 0, 0, 0);
            _time = _termStart - DateTime.Now;
        }

        protected override void OnApplyTemplate()
        {
            _tbDay = GetTemplateChild("tbDay") as TextBlock;
            _tbHour = GetTemplateChild("tbHour") as TextBlock;
            _tbMinute = GetTemplateChild("tbMinute") as TextBlock;
            _tbSecond = GetTemplateChild("tbSecond") as TextBlock;
            _timer.Start();
            base.OnApplyTemplate();
        }

        private void Timer_Tick(object sender, object e)
        {
            _tbDay.Text = _time.Days.ToString("00");
            _tbHour.Text = _time.Hours.ToString("00");
            _tbMinute.Text = _time.Minutes.ToString("00");
            _tbSecond.Text = _time.Seconds.ToString("00");
            _time = _termStart - DateTime.Now;
        }
    }
}
