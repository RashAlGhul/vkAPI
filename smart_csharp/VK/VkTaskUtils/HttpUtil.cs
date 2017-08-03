using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WebdriverFramework.VK.VkTaskUtils
{
    internal class HttpUtil
    {
        internal string imageId;

        internal string GET(string request)
        {
            WebRequest req = WebRequest.Create(request);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }

        internal string POST(string request)
        {
            WebRequest req = WebRequest.Create(request);
            req.Method = "POST";
            int.TryParse(TestData.POSTTimeOut, out int timeout);
            req.Timeout = timeout;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(request);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream receiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(receiveStream, Encoding.UTF8);
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = string.Empty;
            while (count > 0)
            {
                string str = new string(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }

        private string ApiRequestString(string methodName, params string [] param)
        {
            string par = "";
            foreach (var s in param)
            {
                par += s;
            }
            return string.Format(TestData.Host,methodName, par, TestData.Token); //($@"https://api.vk.com/method/{methodName}?{par}&access_token={TestData.Token}");
        }

        internal string PostMessage(string postMessage)
        {
            return ApiRequestString(TestData.CreatePost, $"{TestData.PostMessage}{postMessage}");
        }

        internal string DeletePost(int postId)
        {
            return ApiRequestString(TestData.DeletePost, $"{TestData.OwnerId}{TestData.PostId}{postId}");
        }

        internal string EditPost(int postId, string postMessage)
        {
            string x = POST(UploadImage());
            var image = JObject.Parse(x);
            imageId = image.SelectToken("response[0].id").ToString();
            return ApiRequestString(TestData.EditPost, 
                TestData.OwnerId, TestData.PostId, $"{postId}&{TestData.PostMessage}",postMessage, 
                TestData.Attachment,imageId);
        }

        internal string AddComment(int postId, string commentMessage)
        {
            return ApiRequestString(TestData.CreateComment, 
                $"{TestData.OwnerId}{TestData.PostId}{postId}&{TestData.PostMessage}{commentMessage}");
        }

        internal string LikedPost(int postId)
        {
            return ApiRequestString(TestData.PostIsLiked,
                $"{TestData.OwnerId}{TestData.LikeType}&{TestData.OwnerId}{TestData.LikedObject}{postId}");
        }

        ///
        /// 
        private string UplaodImageServer()
        {
            return ApiRequestString(TestData.WallUploadServer);
        }

        private string UploadServerUrl()
        {
            var response = JObject.Parse(POST(UplaodImageServer()));
            return response
                .Descendants()
                .OfType<JProperty>()
                .First(p => p.Name == "response")
                .Value
                .OfType<JProperty>()
                .First(p => p.Name == "upload_url")
                .Value.ToString();
        }

        private string ImageUploadData()
        {
            string url = UploadServerUrl();
            var http = new WebClient();
            var responseArray = http.UploadFile(url, TestData.ImagePath);
            var ascii = new ASCIIEncoding();
            var result = ascii.GetString(responseArray);
            return result;
        }

        public string UploadImage()
        {
            var data = JObject.Parse(ImageUploadData());
            string server = data
                .Descendants()
                .OfType<JProperty>()
                .First(p => p.Name == "server")
                .Value.ToString();
            string photo = data
                .Descendants()
                .OfType<JProperty>()
                .First(p => p.Name == "photo")
                .Value.ToString();
            string hash = data
                .Descendants()
                .OfType<JProperty>()
                .First(p => p.Name == "hash")
                .Value.ToString();
            return ApiRequestString(TestData.SavePhoto, TestData.UserId, TestData.Photo, photo, 
                TestData.Server, server, TestData.Hash, hash);
        }
    }
}
