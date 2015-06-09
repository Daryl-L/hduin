using hduhelp.Controls;
using hduhelp.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using hduhelp.Model.DataModel;
using System.Runtime.Serialization.Json;
using hduhelp.Helper.JsonHelper;
using System.IO;
using System.Text;
using Windows.System;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;
using Windows.UI.Xaml.Data;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace hduhelp
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer _appData;
        private List<object> _newsList;
        private List<object> _queryList;
        private DateTime _now;

        public MainPage()
        {
            this.InitializeComponent();

            
            _now = DateTime.Now;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            _appData = ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ProgressIndicator.Text = "杭州电子科技大学";
            statusBar.BackgroundColor = Color.FromArgb(255, 23, 56, 132);
            statusBar.BackgroundOpacity = 1;
            await statusBar.ProgressIndicator.ShowAsync();
            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _appData.Values["isFirstLoad"] = null;
        }

        private async Task GetNewsSource(string link, string logo)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", _appData.Values["accessToken"].ToString()),
                new KeyValuePair<string, string>("method", "get")
            };
            var http = new HttpHelper("http://api.redhome.cc/rest/?" + link, param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var obj = new DataContractJsonSerializer(typeof(HduActivityData));
            HduActivityData resObj = null;
            try
            {
                resObj = obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(res))) as HduActivityData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (resObj.error != null)
            {
                throw new Exception(resObj.error);
            }
            foreach (var item in resObj.list)
            {
                var ncItem = new NewsControl
                {
                    Title = item.title,
                    Date = item.time,
                    LogoSource = "/Assets/" + logo,
                    Link = new Uri(item.link)
                };
                ncItem.Click += NcItem_Click;
                _newsList.Add(ncItem);
            }
            //icNews.ItemsSource = newsList;
        }

        private async void NcItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Launcher.LaunchUriAsync((sender as NewsControl).Link);
            }
            catch (FormatException)
            {
                // Bad address.
            }

        }

        private async Task PviSelectClassRefresh()
        {
            wvSelectClass.Navigate(new Uri("http://xk.redhome.cc/", UriKind.Absolute));
        }

        private async Task PviNewsRefresh()
        {
            //var statusBar = StatusBar.GetForCurrentView();
            //statusBar.ProgressIndicator.ProgressValue = null;
            //statusBar.ProgressIndicator.Text = "正在刷新";
            //await statusBar.ProgressIndicator.ShowAsync();

            var nextLesson = await AddNextLesson();
            if (null != nextLesson)
            {
                nextLesson.Margin = new Thickness(0, 0, 0, 10);
                _newsList.Add(nextLesson);
            }
            else
            {
                var lessonOver = new NewsOverControl {Margin = new Thickness(0, 0, 0, 10)};
                _newsList.Add(lessonOver); 
            }
            var makeUpExam = await AddMakeupExam();
            if (null != makeUpExam)
            {
                makeUpExam.Margin = new Thickness(0, 10, 0, 10);
                _newsList.Add(makeUpExam);
            }

            var forumTitle = new NewsTitleControl {Margin = new Thickness(0, 10, 0, 0)};
            forumTitle.Tapped += ForumTitle_Tapped;
            forumTitle.DataContext = new NewsTitleDataModel("讲座论坛");
            forumTitle.Link = new Uri("http://m.hdu.edu.cn/?cat=22");
            _newsList.Add(forumTitle);
            await GetNewsSource("hdu_activity", "random_5.png");
            var newsTile = new NewsTitleControl {Margin = new Thickness(0, 20, 0, 0)};
            newsTile.Tapped += ForumTitle_Tapped;
            newsTile.DataContext = new NewsTitleDataModel("校园新闻");
            newsTile.Link = new Uri("http://m.hdu.edu.cn/?cat=21");
            _newsList.Add(newsTile);
            await GetNewsSource("hdu_news", "random_2.png");
            //statusBar.ProgressIndicator.ProgressValue = 0;
            //statusBar.ProgressIndicator.Text = "杭州电子科技大学";
            //await statusBar.ProgressIndicator.ShowAsync();
            icNews.ItemsSource = _newsList;
            ////}

        }

        private void ForumTitle_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void pvFunction_Loaded(object sender, RoutedEventArgs e)
        {
            if (_appData.Values["isFirstLoad"] != null)
            {
                return;
            }
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.ProgressValue = null;
            statusBar.ProgressIndicator.Text = "正在刷新";
            await statusBar.ProgressIndicator.ShowAsync();

            FetchDataHelper.GetAccessToken();
            _newsList = new List<object>();
            await PviNewsRefresh();
            _queryList = new List<object>();
            await PviQueryRefresh();

            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ProgressIndicator.Text = "杭州电子科技大学";
            await statusBar.ProgressIndicator.ShowAsync();
            _appData.Values["isFirstLoad"] = 1;
        }

        private async Task<MakeUpExamControl> AddMakeupExam()
        {
            RecentExamDataModel makeUpExamData = null;
            var makeUpExamDataList = new List<RecentExamDataModel>();
            try
            {
                makeUpExamDataList = await FetchDataHelper.GetRecentExamInfo(_appData.Values["accessToken"].ToString());
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString()).ShowAsync();
            } 
            if (null == makeUpExamDataList)
            {
                return await Task.Run(() => null as MakeUpExamControl);
            }
            var smallest = makeUpExamDataList[0].Timestamp;
            foreach (var item in makeUpExamDataList)
            {
                if (item.Timestamp >= _now && item.Timestamp <= smallest)
                {
                    makeUpExamData = item;
                }
            }
            if (null == makeUpExamData)
            {
                return await Task.Run(() => null as MakeUpExamControl);
            }
            makeUpExamData.Type = "最近补考";
            var makeUpExam = new MakeUpExamControl {DataContext = makeUpExamData};
            return await Task.Run(() => makeUpExam);
        }

        private async Task<NextLessonControl> AddNextLesson()
        {
            var year = _now.Year;
            var month = _now.Month;
            var day = _now.Day;
            var classTime = new List<DateTime>()
            {
                _now,
                new DateTime(year, month, day, 8, 5, 0),
                new DateTime(year, month, day, 8, 55, 0),
                new DateTime(year, month, day, 10, 0, 0),
                new DateTime(year, month, day, 10, 50, 0),
                new DateTime(year, month, day, 11, 40, 0),
                new DateTime(year, month, day, 13, 30, 0),
                new DateTime(year, month, day, 14, 20, 0),
                new DateTime(year, month, day, 15, 10, 0),
                new DateTime(year, month, day, 16, 5, 0),
                new DateTime(year, month, day, 18, 30, 0),
                new DateTime(year, month, day, 19, 20, 0),
                new DateTime(year, month, day, 20, 10, 0)
            };
            var week = Convert.ToInt32(Math.Ceiling((_now - new DateTime(2015, 3, 9)).TotalDays / 7.0) + 1);
            List<ClassDataModel> classList = null;
            try
            {
                classList = await FetchDataHelper.GetNetLesson(_appData.Values["accessToken"].ToString());
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString()).ShowAsync();
            }

            var timeTmp = classTime[12];
            ClassDataModel classData = null;
            if (_now > classTime[12])
            {
                return await Task.Run(() => null as NextLessonControl);
            }
            else
            {
                foreach (var item in classList)
                {
                    if (item.WeekDay == (int)_now.DayOfWeek)
                        if (item.Dsz == -1 || item.Dsz == week % 2)
                        {
                            if (item.StartWeek <= week && item.EndWeek >= week)
                            {
                                if (classTime[item.Begin] >= _now && classTime[item.Begin] <= timeTmp)
                                {
                                    classData = item;
                                    timeTmp = classTime[item.Begin];
                                }
                            }
                        }
                }
            }
            
            if (null == classData)
            {
                return await Task.Run(() => null as NextLessonControl);
            }
            //classData.Duration = string.Format("第{0}~{1}周", classData.StartWeek, classData.EndWeek);
            //classData.Time = string.Format("第{0}~{1}节（{2}~{3}）", classData.Begin, classData.End, classBeginEnd[classData.Begin]["start"], classBeginEnd[classData.End]["end"]);
            var nextLesson = new NextLessonControl {DataContext = classData};
            return await Task.Run(() => nextLesson);
        }

        private async Task<OneCardCashControl> AddOneCardCash()
        {
            OneCardCashDataModel oneCardCashData = null;
            try
            {
                oneCardCashData = await FetchDataHelper.GetOneCardCash(_appData.Values["accessToken"].ToString(), _appData.Values["password"].ToString());
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString()).ShowAsync();
            }
            var oneCardCash = new OneCardCashControl {DataContext = oneCardCashData};
            return await Task.Run(() => oneCardCash);
        }

        private async Task PviQueryRefresh()
        {
            //var statusBar = StatusBar.GetForCurrentView();
            //statusBar.ProgressIndicator.ProgressValue = null;
            //statusBar.ProgressIndicator.Text = "正在刷新";
            //await statusBar.ProgressIndicator.ShowAsync();

            var oneCardCashTitle = new QueryTitleControl
            {
                DataContext = new QueryTitleDataModel("一卡通"),
                Margin = new Thickness(0, 0, 0, 0)
            };
            _queryList.Add(oneCardCashTitle);
            var oneCardCash = await AddOneCardCash();
            oneCardCash.Margin = new Thickness(0, 10, 0, 10);
            _queryList.Add(oneCardCash);
            var wholeClassTitle = new QueryTitleControl
            {
                DataContext = new QueryTitleDataModel("一周课表"),
                Margin = new Thickness(0, 10, 0, 0)
            };
            _queryList.Add(wholeClassTitle);
            var wholeWeekClass = new WholeWeekClassControl();
            await wholeWeekClass.SetClass(_appData.Values["accessToken"].ToString());
            wholeWeekClass.Margin = new Thickness(0, 0, 0, 10);
            _queryList.Add(wholeWeekClass);

            
            icQuery.ItemsSource = _queryList;
        }
        //private void pviQuery_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (appData.Values["isPivQueryFirstLoad"] == null)
        //    {
        //        FetchDataHelper.GetAccessToken();
        //        queryList = new List<object>();
        //        pviQueryRefresh();
        //    }
        //}

        private void abLogoff_Click(object sender, RoutedEventArgs e)
        {
            _appData.Values["accessToken"] = null;
            Frame.Navigate(typeof(Login));
        }

        private async void absFeedBack_Click(object sender, RoutedEventArgs e)
        {
            var emailMessage = new EmailMessage {Subject = "杭电客户端wp反馈", Body = "hahaha"};
            emailMessage.To.Add(new EmailRecipient("sai9375@hotmail.com"));
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private async void abRefresh_Click(object sender, RoutedEventArgs e)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.ProgressValue = null;
            statusBar.ProgressIndicator.Text = "正在刷新";
            await statusBar.ProgressIndicator.ShowAsync();
            if (_newsList == null)
            {
                _newsList = new List<object>();
            }

            if (_queryList == null)
            {
                _queryList = new List<object>();
            }

            FetchDataHelper.GetAccessToken();

            if (pvFunction.SelectedItem == pviNews)
            {
                await PviNewsRefresh();
            }
            else if (pvFunction.SelectedItem == pviQuery)
            {
                await PviQueryRefresh();
            }
            else if (pvFunction.SelectedItem == pviSelectClass)
            {
                await PviSelectClassRefresh();
            }

            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ProgressIndicator.Text = "杭州电子科技大学";
            await statusBar.ProgressIndicator.ShowAsync();
        }

        private void absAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void FunctionButtonControl_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LibraryPage));
        }

        private async void pviQuery_GotFocus(object sender, RoutedEventArgs e)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.ProgressValue = null;
            statusBar.ProgressIndicator.Text = "正在刷新";
            await statusBar.ProgressIndicator.ShowAsync();

            FetchDataHelper.GetAccessToken();
            _queryList = new List<object>();
            await PviQueryRefresh();

            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ProgressIndicator.Text = "杭州电子科技大学";
            await statusBar.ProgressIndicator.ShowAsync();
            _appData.Values["isFirstLoad"] = 1;
        }

        //private async void pviQuery_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var statusBar = StatusBar.GetForCurrentView();
        //    statusBar.ProgressIndicator.ProgressValue = null;
        //    statusBar.ProgressIndicator.Text = "正在刷新";
        //    await statusBar.ProgressIndicator.ShowAsync();

        //    FetchDataHelper.GetAccessToken();
        //    _queryList = new List<object>();
        //    await PviQueryRefresh();

        //    statusBar.ProgressIndicator.ProgressValue = 0;
        //    statusBar.ProgressIndicator.Text = "杭州电子科技大学";
        //    await statusBar.ProgressIndicator.ShowAsync();
        //    _appData.Values["isFirstLoad"] = 1;
        //}
    }
}
