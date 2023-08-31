using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WGU_App.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsPassed { get; set; }

        public bool StartNotification { get; set; }
        public int TermId { get; set; }

        //public CourseInstructor Instructor = null;

        public Course(string name, string title, string description, DateTime startDate, DateTime endDate, int termId, bool startNotification = true, bool isPassed = false)
        {
            Name = name;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            TermId = termId;
            StartNotification = startNotification;
            IsPassed = isPassed;
        }

        public Course() { }

    }
}
