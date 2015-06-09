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
    public sealed partial class HduNewsControl : UserControl
    {
        public string Title
        {
            get
            {
                return "a";
            }
            set
            {
                TbTitle.Text = value;
            }
        }
        
        public HduNewsControl()
        {
            this.InitializeComponent();
            var b = new Button();
        }
        //public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
    }
}
