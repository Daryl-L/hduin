using hduhelp.Helper.JsonHelper;
using hduhelp.Model.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace hduhelp.Helper
{
    static class FetchDataHelper
    {
        static public async void GetAccessToken()
        {
            var appData = ApplicationData.Current.LocalSettings;
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", appData.Values["stuno"].ToString()),
                new KeyValuePair<string, string>("password", appData.Values["password"].ToString())
            };
            var http = new HttpHelper("http://api.redhome.cc/helper/provxy_login.php", param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
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
                if (resObj.error == null)
                {
                    appData.Values["accessToken"] = resObj.accessToken;
                    appData.Values["name"] = resObj.Name;
                }
                else
                {
                    await new MessageDialog(resObj.error).ShowAsync();
                }
            }
        }

        static public async Task<List<RecentExamDataModel>> GetRecentExamInfo(string token)
        {
            var exam = new List<RecentExamDataModel>();
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", token),
                new KeyValuePair<string, string>("method", "get")
            };
            var http = new HttpHelper("http://api.redhome.cc/rest/?exam_makeup", param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var obj = new DataContractJsonSerializer(typeof(MakeupExamData));
            MakeupExamData resObj = null;
            try
            {
                resObj = obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(res))) as MakeupExamData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (resObj.error != null)
            {
                throw new Exception(resObj.error);
            }
            else if (Convert.ToInt32(resObj.count) == 0)
            {
                return await Task.Run(() => null as List<RecentExamDataModel>);
            }
            var reg = new Regex(@"(?<year>\d+)年(?<month>\d+)月(?<day>\d+)日\((?<starth>\d+):(?<startm>\d+)");
            foreach (var data in resObj.list)
            {
                var tmp = new RecentExamDataModel();
                var matches = reg.Matches(data.kssj);
                var time = new Dictionary<string, int>();
                foreach (Match item in matches)
                {
                    foreach (var name in reg.GetGroupNames())
                    {
                        if (name == "0")
                        {
                            continue;
                        }
                        time.Add(name, Convert.ToInt32(item.Groups[name].Value));
                    }
                }
                tmp.Name = data.kcmc;
                tmp.Seat = data.zwh;
                tmp.Time = data.kssj;
                tmp.Place = data.jsmc;
                tmp.Timestamp = new DateTime(time["year"], time["month"], time["day"], time["starth"], time["startm"], 0);
                exam.Add(tmp);
                tmp = null;
            }
            
            
            return await Task.Run(() => exam);
        }

        static public async Task<List<ClassDataModel>> GetNetLesson(string token)
        {
            var classBeginEnd = new List<Dictionary<string, string>>()
            {
                new Dictionary<string, string>(),
                new Dictionary<string, string>()
                {
                    { "start", "08:05" },
                    { "end", "08:50" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "08:55" },
                    { "end", "09:40" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "10:00" },
                    { "end", "10:45" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "10:50" },
                    { "end", "11:35" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "11:40" },
                    { "end", "12:25" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "13:35" },
                    { "end", "14:20" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "14:25" },
                    { "end", "15:10" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "15:15" },
                    { "end", "16:00" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "16:05" },
                    { "end", "16:50" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "18:30" },
                    { "end", "19:15" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "19:20" },
                    { "end", "20:05" },
                },
                new Dictionary<string, string>()
                {
                    { "start", "20:10" },
                    { "end", "20:55" },
                }
            };
            var lesson = new List<ClassDataModel>();
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", token.ToString()),
                new KeyValuePair<string, string>("method", "get"),
                new KeyValuePair<string, string>("semester", "2"),
                new KeyValuePair<string, string>("schoolyear", "2014-2015")
            };
            var http = new HttpHelper("http://api.redhome.cc/rest/?schedule", param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var obj = new DataContractJsonSerializer(typeof(ClassData[]));
            ClassData[] resObj = null;
            try
            {
                resObj = obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(res))) as ClassData[];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ClassDataModel tmp = null;
            foreach (var item in resObj)
            {
                tmp = new ClassDataModel
                {
                    Name = item.kcmc,
                    EndWeek = Convert.ToInt32(item.jsz),
                    Place = item.skdd,
                    StartWeek = Convert.ToInt32(item.qsz),
                    Teacher = item.jsxm,
                    Dsz = Convert.ToInt32(item.dsz),
                    Begin = Convert.ToInt32(item.beginat),
                    End = Convert.ToInt32(item.endat),
                    WeekDay = Convert.ToInt32(item.xqj),
                    Duration = string.Format("第{0}~{1}周", item.qsz, item.jsz)
                };
                switch (item.dsz)
                {
                    case "-1":
                        break;
                    case "0":
                        tmp.Duration += "（双周）";
                        break;
                    case "1":
                        tmp.Duration += "（单周）";
                        break;
                    default:
                        break;
                }
                tmp.Time = string.Format("第{0}~{1}节（{2}~{3}）", tmp.Begin, tmp.End, classBeginEnd[tmp.Begin]["start"], classBeginEnd[tmp.End]["end"]);
                lesson.Add(tmp);
                tmp = null;
            }
            return await Task.Run(() => lesson);
        }

        static public async Task<OneCardCashDataModel> GetOneCardCash(string token, string password)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", token),
                new KeyValuePair<string, string>("method", "get"),
                new KeyValuePair<string, string>("password", password)
            };
            var http = new HttpHelper("http://api.redhome.cc/rest/?cas_card", param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var obj = new DataContractJsonSerializer(typeof(OneCardCashData));
            OneCardCashData resObj = null;
            try
            {
                resObj = obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(res))) as OneCardCashData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var oneCard = new OneCardCashDataModel();
            oneCard.Balance = resObj.balance;
            return await Task.Run(() => oneCard);
        }

        static public async Task<Dictionary<string, object>> GetLibraryCenterPerson(string token)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", token),
                new KeyValuePair<string, string>("method", "get")
            };
            var http = new HttpHelper("http://api.redhome.cc/rest/?center_person", param);
            var res = string.Empty;
            try
            {
                res = await http.PostWebRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var obj = new DataContractJsonSerializer(typeof(LibraryCenterPersonData));
            LibraryCenterPersonData resObj = null;
            try
            {
                resObj = obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(res))) as LibraryCenterPersonData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var library = new Dictionary<string, object>()
            {
                { "library", new List<LibraryLendDataModel>() }
            };
            var libraryLend = new List<LibraryLendDataModel>();
            foreach (var item in resObj.lend_mine.now)
            {
                var tmp = new LibraryLendDataModel();
                tmp.Author = item.author;
                tmp.LendTimes = item.times_renew;
                tmp.Name = item.name;
                tmp.OverTime = item.date_deadline;
                libraryLend.Add(tmp);
            }

            return await Task.Run(() => library);
        }
    }
}
