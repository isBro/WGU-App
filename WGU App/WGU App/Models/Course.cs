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

        //public Picker Status { get; set; }

        public bool StartNotification;
        public int TermId { get; set; }

        public Course(string name, string title, string description, DateTime startDate, DateTime endDate, int termId, bool startNotification)
        {
            Name = name;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            TermId = termId;
            StartNotification = startNotification;
        }

        public Course() { }

    }
}
