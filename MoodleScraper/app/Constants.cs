using System;
using System.Collections.Generic;
using System.Text;

namespace MoodleScraper.app
{
    class Constants
    {
        public static string MoodleHome = "https://moodle.iitb.ac.in";
        public static string MoodleLoginLink = "https://moodle.iitb.ac.in/login/index.php";
        public static string ResponseFormat = "username={0}&password={1}&anchor=";
        
        public static string GetRequest = "GET";
        public static string PostRequest = "POST";
        
        public static string CourseDetails = "../../../config/Courses.json";
        public static string MoodleData = "../../../config/MoodleData.json";
    }
}
