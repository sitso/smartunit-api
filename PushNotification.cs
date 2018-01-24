using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SmartUnitApi
{
    public class PushNotifications
    {
        public static void AlarmNotification(object obj)
        {
            var data = new
            {
                to = "",
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
                to = "",
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
