using System;
using System.Collections.Generic;
using System.Text;

namespace MoodleScraper.models
{
    class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        //TODO: Implement this later
        public string Instructor { get; set; }
        public string FullName { get; set; }
        public Course(string i)
        {
            var temp = i.Split("view.php?id=")[1].Split("</a>")[0];
            int temp1 = temp.IndexOf('"');
            Id = Convert.ToInt32(temp.Substring(0, temp1));
            FullName = temp.Substring(temp1 + 2);
            Code = FullName.Substring(0, 6);
            Name = FullName.Substring(FullName.IndexOf(" ", 6) + 1);
        }
        public void Print()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Code: {0}", Code);
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Id: {0}", Id);
        }
    }
}
