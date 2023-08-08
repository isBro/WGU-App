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


        public CourseAssessment() { }

        public CourseAssessment(int id, string assessmentName, string assessmentDescription, bool isPerformanceAsessment, bool isPassed)
        {
            Id = id;
            AssessmentName = assessmentName;
            AssessmentDescription = assessmentDescription;
            IsPerformanceAsessment = isPerformanceAsessment;
            IsPassed = isPassed;
        }
    }
}
