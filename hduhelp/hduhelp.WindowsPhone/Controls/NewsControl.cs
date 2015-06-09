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
using Windows.UI.Xaml.Media.Imaging;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234235 上有介绍

namespace hduhelp.Controls
{
    public sealed class NewsControl : Button
    {
        private string _title, _date, _imgSource;

        public string Date
        {
            set
            {
                _date = value;
            }
        }
        public string Title
        {
            set
            {
                _title = value;
            }
        }
        
        public string LogoSource
        {
            set
            {
                _imgSource = value;
            }
        }
        public Uri Link { get; set; }
        

        public NewsControl()
        {
            this.DefaultStyleKey = typeof(NewsControl);
        }

        protected override void OnApplyTemplate()
        {
            (GetTemplateChild("tbTitle") as TextBlock).Text = _title;
            (GetTemplateChild("tbDate") as TextBlock).Text = _date;
            var bitmapImage = new BitmapImage();
            (GetTemplateChild("imgLogo") as Image).Source = new BitmapImage(new Uri("ms-appx://" + _imgSource, UriKind.RelativeOrAbsolute));
            base.OnApplyTemplate();
        }
    }
}
