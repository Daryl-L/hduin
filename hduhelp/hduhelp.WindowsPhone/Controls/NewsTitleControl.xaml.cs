using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace hduhelp.Controls
{
    public sealed partial class NewsTitleControl : UserControl
    {
        public Uri Link { get; set; }
        public NewsTitleControl()
        {
            this.InitializeComponent();
        }

        protected override async void OnTapped(TappedRoutedEventArgs e)
        {
            if (e.OriginalSource is Image)
            {
                await Launcher.LaunchUriAsync(Link);
            }
            base.OnTapped(e);
        }
    }
}
