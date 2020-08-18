using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppDemo.http
{
    class HttpUtil
    {
        //携带cookie请求数据
        public static string requestWithCookie(JObject jObject)
        {
            string content = null;
            try
            {
                //data
                string cookieStr = "JSESSIONID=7676503FD2328470EDBEFBC4E413ADE1";
                string url = "http://localhost:1088/user/logout";
                //string postData = string.Format("userid={0}&password={1}", "guest", "123456");
                //byte[] data = Encoding.UTF8.GetBytes(postData);
                byte[] data = Encoding.UTF8.GetBytes(jObject.ToString());

                // Prepare web request...
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                request.Headers.Add("Cookie", cookieStr);
                request.ContentLength = data.Length;
                Stream newStream = request.GetRequestStream();

                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                // Get response
                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                content = reader.ReadToEnd();
                Console.WriteLine(content);
                Console.ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
            return content;
        }


    }





}
