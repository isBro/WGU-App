﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WGU_App.Models
{
    public class CourseInstructor
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set;}
        public string InstructorName { get; set;}
        public string InstructorEmail { get; set;}
        public string InstructorPhone { get; set;}

        public CourseInstructor() { }

        public CourseInstructor(int courseId, string instructorName, string instructorEmail, string instructorPhone)
        {

            CourseId = courseId;
            InstructorName = instructorName;
            InstructorEmail = instructorEmail;
            InstructorPhone = instructorPhone;

        }



    }
}
