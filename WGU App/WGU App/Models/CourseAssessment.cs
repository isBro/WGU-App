using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WGU_App.Models
{
    public class CourseAssessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentDescription { get; set;}

        public bool IsPerformanceAsessment { get; set;}

        public bool IsPassed { get; set;} = false;
    }
}
