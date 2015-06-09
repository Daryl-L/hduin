using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Runtime.Serialization.Json;
using hduhelp.Helper.JsonHelper;
using hduhelp.Helper;
using System.Text;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace hduhelp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //状态栏
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ProgressIndicator.Text = "杭州电子科技大学";
            statusBar.BackgroundColor = Color.FromArgb(255, 23, 56, 132);
            statusBar.BackgroundOpacity = 1;
            await statusBar.ProgressIndicator.ShowAsync();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //tbStuno.Focus(FocusState.Unfocused);
            if (TbxStuno.Text == string.Empty)
            {
                await new MessageDialog("请输入学号").ShowAsync();
                return;
            }
            BtnLogin.IsEnabled = false;
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.ProgressValue = null;
            statusBar.ProgressIndicator.Text = "正在登录";
            await statusBar.ProgressIndicator.ShowAsync();
            var stuno = TbxStuno.Text;
            var password = string.Empty;
            if (TbxPassword.Password != string.Empty)
            {
                password = new AesEcryptHelper(TbxPassword.Password).Encrypt();
            }
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", stuno),
                new KeyValuePair<string, string>("password", password)
            };
            var http = new HttpHelper("http://api.redhome.cc/helper/provxy_login.php", param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            finally
            {
                statusBar.ProgressIndicator.ProgressValue = 0;
                statusBar.ProgressIndicator.Text = "杭州电子科技大学";
                await statusBar.ProgressIndicator.ShowAsync();
                BtnLogin.IsEnabled = true;
            }
            AccessTokenData resObj = null;
            try
            {
                var obj = new DataContractJsonSerializer(typeof(AccessTokenData));
                resObj = obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(res))) as AccessTokenData;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            finally
            {
                var appData = ApplicationData.Current.LocalSettings;
                if (resObj.error == null)
                {
                    appData.Values["accessToken"] = resObj.accessToken;
                    appData.Values["name"] = resObj.Name;
                    appData.Values["stuno"] = stuno;
                    appData.Values["password"] = password;
                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    await new MessageDialog(resObj.error).ShowAsync();
                }
            }
            //appData.Values["accessToken"] = resObj.GetNamedString("accessToken");
            //appData.Values["name"] = resObj.GetNamedString("name");
        }
    }
}
