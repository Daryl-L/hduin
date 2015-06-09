using hduhelp.Helper;
using hduhelp.Model.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class WholeWeekClassControl : UserControl
    {
        public WholeWeekClassControl()
        {
            this.InitializeComponent();
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    PvClassTable.SelectedItem = PviFri;
                    break;
                case DayOfWeek.Monday:
                    PvClassTable.SelectedItem = PviMon;
                    break;
                case DayOfWeek.Saturday:
                    PvClassTable.SelectedItem = PviSat;
                    break;
                case DayOfWeek.Sunday:
                    PvClassTable.SelectedItem = PviSun;
                    break;
                case DayOfWeek.Thursday:
                    PvClassTable.SelectedItem = PviThu;
                    break;
                case DayOfWeek.Tuesday:
                    PvClassTable.SelectedItem = PviTue;
                    break;
                case DayOfWeek.Wednesday:
                    PvClassTable.SelectedItem = PviWed;
                    break;
                default:
                    break;
            }
        }

        public async Task SetClass(string token)
        {
            var classList = await FetchDataHelper.GetNetLesson(token);
            var lessonControlList = new List<List<LessonControl>>
            {
                new List<LessonControl>(),
                new List<LessonControl>(),
                new List<LessonControl>(),
                new List<LessonControl>(),
                new List<LessonControl>(),
                new List<LessonControl>(),
                new List<LessonControl>()
            };
            foreach (var item in classList)
            {
                try
                {
                    var lesson = new LessonControl();
                    lesson.Margin = new Thickness(5, 0, 5, 0);
                    lesson.DataContext = item;
                    lessonControlList[item.WeekDay - 1].Add(lesson);
                    lesson = null;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            IcMon.ItemsSource = lessonControlList[0];
            IcTue.ItemsSource = lessonControlList[1];
            IcWed.ItemsSource = lessonControlList[2];
            IcThu.ItemsSource = lessonControlList[3];
            IcFri.ItemsSource = lessonControlList[4];
            IcSat.ItemsSource = lessonControlList[5];
            IcSun.ItemsSource = lessonControlList[6];
        }
    }
}
