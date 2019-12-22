using MoodleScraper.app;
using MoodleScraper.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MoodleScraper
{
    class Updater
    {
        string CookieResponse { get; set; }
        User user { get; set; }
        public Updater()
        {
            using(StreamReader file = new StreamReader(Constants.MoodleData))
                user = JsonConvert.DeserializeObject<User>(file.ReadToEnd());
        }
        public void GetCookie()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Constants.MoodleHome);
            req.Method = Constants.GetRequest;
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            CookieResponse = res.Headers[HttpResponseHeader.SetCookie];
        }

        public void GetCourses()
        {
            if (CookieResponse == null)
                GetCookie();
            
            //create Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Constants.MoodleLoginLink);
            request.Method = Constants.PostRequest;
            request.CookieContainer = new CookieContainer();

            //add data to request
            string postData = String.Format(Constants.ResponseFormat, user.Ldap, user.Password);
            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            request.CookieContainer.SetCookies(request.RequestUri, CookieResponse);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(postBytes, 0, postBytes.Length);
            stream.Close();

            //get response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            StreamReader resultText = new StreamReader(stream);
            string htmlText = resultText.ReadToEnd();
            resultText.Close();
            stream.Flush();
            stream.Close();
            response.Close();

            var courseString = htmlText.Split("coursebox clearfix");
            List<Course> courses = new List<Course>();
            for (int i = 1; i < courseString.Length - 1; ++i)
                courses.Add(new Course(courseString[i]));
            using (StreamWriter file = new StreamWriter(Constants.CourseDetails))
                file.Write(JsonConvert.SerializeObject(courses,Formatting.Indented));
        }
    }
}
