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

namespace hduhelp.Controls
{
    public sealed partial class FunctionButtonControl : UserControl
    {
        public FunctionButtonControl()
        {
            this.InitializeComponent();
            //this.tbTitle.DataContext = Title;
            //this.imgLogo.DataContext = ImageSource;
        }

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            //base.OnTapped(e);
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(FunctionButtonControl), new PropertyMetadata(null));



        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(FunctionButtonControl), new PropertyMetadata(null));



    }

    //public class Func : DependencyObject
    //{
    //    public static readonly DependencyProperty NamePropety =
    //        DependencyProperty.Register("Name", typeof(string), typeof(Func), new PropertyMetadata(null));
    //    public static readonly DependencyProperty ImageSourcePropety =
    //        DependencyProperty.Register("ImageSource", typeof(string), typeof(Func), new PropertyMetadata(null));

    //    public string Name
    //    {
    //        get
    //        {
    //            return (string)GetValue(NamePropety);
    //        }
    //        set
    //        {
    //            SetValue(NamePropety, value);
    //        }
    //    }

    //    public string ImageSource
    //    {
    //        get
    //        {
    //            return (string)GetValue(ImageSourcePropety);
    //        }
    //        set
    //        {
    //            SetValue(ImageSourcePropety, value);
    //        }
    //    }
    //}
}
