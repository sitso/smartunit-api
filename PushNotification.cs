using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartUnitApi
{
    public class PushNotifications
    {
        public static void AlarmNotification(object obj)
        {
            var data = new
            {
                to = "eW-EoRhvJfY:APA91bEuoefVdMVTCZBqw-L3uZYPUzl2G02H_vk_tdv48G6vGN_7ksOGObUqDlkmKPJY3C_1IZf1voUtn7Kmc4XJ1FLrozWqg-92zZ_14ZVoJMEypKR2NAtKa0O9J6omYr-lR9TTRktf",
                notification = new
                {
                    title = "Alarm",
                    body = "dette er body",
                    icon = "icon"
                },
                data =  new
                {
                    type = "Alarm logged",
                    obj = JsonConvert.SerializeObject(obj)
                }
             };

            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + Constants.FirebaseAPIKey);

                    string json = JsonConvert.SerializeObject(data);

                    Task.WaitAll(client.PostAsync(url,
                        new StringContent(json, Encoding.UTF8, "application/json")));
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void DataChangeNotification(object obj, string msgType)
        {
            var data = new
            {
                to = "eW-EoRhvJfY:APA91bEuoefVdMVTCZBqw-L3uZYPUzl2G02H_vk_tdv48G6vGN_7ksOGObUqDlkmKPJY3C_1IZf1voUtn7Kmc4XJ1FLrozWqg-92zZ_14ZVoJMEypKR2NAtKa0O9J6omYr-lR9TTRktf",
                data = new
                {
                    type = msgType,
                    obj = JsonConvert.SerializeObject(obj)
                }
            };
            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + Constants.FirebaseAPIKey);

                    string json = JsonConvert.SerializeObject(data);

                    Task.WaitAll(client.PostAsync(url,
                        new StringContent(json, Encoding.UTF8, "application/json")));
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
