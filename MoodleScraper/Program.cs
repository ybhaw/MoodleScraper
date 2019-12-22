using MoodleScraper.models;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace MoodleScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Updater updater = new Updater();
            updater.GetCourses();
        }
    }
}
