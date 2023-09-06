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
        public string AssessmentType { get; set;}

        public bool IsPassed { get; set;}

        public int CourseId { get; set;}


        public CourseAssessment() { }

        public CourseAssessment(string assessmentName, string assessmentDescription, string assessmentType = "Performance", bool isPassed = false, int courseId = 0)
        {

            AssessmentName = assessmentName;
            AssessmentDescription = assessmentDescription;
            AssessmentType = assessmentType;
            IsPassed = isPassed;
            CourseId = courseId;
        }
    }
}
