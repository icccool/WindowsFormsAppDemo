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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  
                throw;
            }
            return content;
        }
        
        
        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static bool HttpDownload(string url, string path)
        {
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(path, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }





}
