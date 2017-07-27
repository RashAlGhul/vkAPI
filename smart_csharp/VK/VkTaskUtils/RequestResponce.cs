using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebdriverFramework.VK.VkTaskUtils
{
    internal class RequestResponce
    {
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
            return $@"https://api.vk.com/method/{methodName}?{par}&access_token={TestData.Token}";
        }

        internal string PostMessage(string postMessage)
        {
            return ApiRequestString(TestData.CreatePost, $"{TestData.PostMessage}{postMessage}");
        }

        internal string DeletePost(int postId)
        {
            return ApiRequestString(TestData.DeletePost, $"{TestData.UserId}{TestData.PostId}{postId}");
        }

        internal string EditPost(int postId, string postMessage)
        {
            return ApiRequestString(TestData.EditPost, 
                $"{TestData.UserId}{TestData.PostId}{postId}&{TestData.PostMessage}{postMessage}");
        }

        internal string AddComment(int postId, string commentMessage)
        {
            return ApiRequestString(TestData.CreateComment, 
                $"{TestData.UserId}{TestData.PostId}{postId}&{TestData.PostMessage}{commentMessage}");
        }

        internal string LikedPost(int postId)
        {
            return ApiRequestString(TestData.PostIsLiked,
                $"{TestData.UserId}{TestData.LikeType}&{TestData.UserId}{TestData.LikedObject}{postId}");
        }
    }
}
