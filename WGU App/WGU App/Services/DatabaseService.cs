using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WGU_App.Models;
using Xamarin.Forms;

namespace WGU_App.Services
{
    public static class DatabaseService
    {

        private static SQLiteAsyncConnection db;

         static async Task Init()
        {

            if (db != null)
            {
                return;
            }
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CourseData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Term>();
            await db.CreateTableAsync<Course>();
            await db.CreateTableAsync<CourseInstructor>();
            await db.CreateTableAsync<CourseAssessment>();
        }

        #region Term methods

        public static async Task AddTerm(string termName, DateTime startDate, DateTime endDate)
        {

            await Init();

            var term = new Term()
            {
                TermName = termName,
                StartDate = startDate,
                EndDate = endDate

            };
            await db.InsertAsync(term);
            var id = term.Id;
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();
            await db.DeleteAsync<Term>(id);
        }

        public static async Task<IEnumerable<Term>> GetTerms(int id)
        {
            await Init();
            var term = await db.Table<Term>()
                .Where(i => i.Id == id)
                .ToListAsync();

            return term;
        

        }

        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();

            var terms = await db.Table<Term>().ToListAsync();

            return terms;
      

        }

        public static async Task UpdateTerm(int id, string name, DateTime startDate, DateTime endDate)
        {
            await Init();

            var termQuery = await db.Table<Term>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.TermName = name;
                termQuery.StartDate = startDate;
                termQuery.EndDate = endDate;

            }

            await db.UpdateAsync(termQuery);

        }


        #endregion

        #region Course methods

        public static async Task AddCourse(int termId, string name, string title,  string description, DateTime startDate, DateTime endDate, bool startNotification, bool isPassed)
        {
            await Init();
            var course = new Course
            {
                TermId = termId,
                Name = name,
                Title = title,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                StartNotification = true,
                IsPassed = false
                

            };
            await  db.InsertAsync(course);
            var id = course.Id;
        }

        public static async Task RemoveCourse(int id)
        {
            await Init();
            await db.DeleteAsync<Course>(id);
        }

        public static async Task<IEnumerable<Course>> GetCourses(int termId)
        {
            await Init();
            var courses = await db.Table<Course>()
                .Where(i => i.TermId == termId) 
                .ToListAsync();

            return courses;
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();
            var courses = await db.Table<Course>().ToListAsync();
           
            return courses;


        }

        public static async Task UpdateCourse(int id, string name, string title, string description, DateTime startDate, DateTime endDate, bool startNotification, int termId, bool isPassed)
        {
            await Init();

            var courseQuery = await db.Table<Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.Title = title;
                courseQuery.Name = name;
                courseQuery.Description = description;
                courseQuery.StartDate = startDate;
                courseQuery.EndDate = endDate;
                courseQuery.StartNotification = startNotification;
                courseQuery.TermId = termId;
                courseQuery.IsPassed = isPassed;

                await db.UpdateAsync(courseQuery);

                
            }
        }


        #endregion

        #region CourseInstructor methods

        public static async Task AddCourseInstructor(int courseId, string instructorName, string instructorEmail, string instructorPhone)
        {
            await Init();

            var courseInstructor = new CourseInstructor(courseId, instructorName, instructorEmail, instructorPhone);

            await db.InsertAsync(courseInstructor);

            int id = courseInstructor.Id;

        }

        public static async Task<IEnumerable<CourseInstructor>> GetCourseInstructor(int courseId)
        {
            await Init();

            var instructor = await db.Table<CourseInstructor>()

                .Where(i=>i.CourseId == courseId)
                .ToListAsync();

            
            return instructor;
        }

        public static async Task<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            await Init();
            var instructors = await db.Table<CourseInstructor>().ToListAsync();

            return instructors;


        }

        public static async Task RemoveCourseInstructor(int id)
        {
            await Init();
            await db.DeleteAsync<CourseInstructor>(id);
        }

        public static async Task UpdateCourseInstructor(int id, string name, string email, string phone, int courseId)
        {
            await Init();

            var instructorQuery = await db.Table<CourseInstructor>()
                .Where(i=>i.Id == id)
                .FirstOrDefaultAsync();

            if (instructorQuery != null)
            {
                instructorQuery.InstructorName = name;
                instructorQuery.InstructorEmail = email;
                instructorQuery.InstructorPhone = phone;
                instructorQuery.CourseId = courseId;

                await db.UpdateAsync(instructorQuery);

            }
        }

        #endregion

        #region  CourseAssessment methods

        public static async Task AddCourseAssessment(string name, string description, string type, int courseId, bool isPassed)
        {
            await Init();
            var assessment = new CourseAssessment
            {
                CourseId = courseId,
                AssessmentName = name,
                AssessmentDescription = description,
                AssessmentType = type,
                IsPassed = isPassed

            };

            await db.InsertAsync(assessment);
        }

        public static async Task RemoveCourseAssessment(int id) 
        { 
            
            await Init();

            await db.DeleteAsync<CourseAssessment>(id);
        
        }

        public static async Task<IEnumerable<CourseAssessment>> GetCourseAssessments()
        {
            await Init();
            var courses = await db.Table<CourseAssessment>().ToListAsync();

            return courses;
        }

        public static async Task<IEnumerable<CourseAssessment>> GetCourseAssessments(int courseId)
        {
            await Init();
            var courses = await db.Table<CourseAssessment>()
                .Where(i=>i.CourseId == courseId)
                .ToListAsync();

            return courses;
        }

        public static async Task UpdateCourseAssessment(int id, string name, string description, string type, int courseId, bool isPassed)
        {
            await Init();
            var assessmentQuery = await db.Table<CourseAssessment>()
                .Where(i=>i.Id == id)
                .FirstOrDefaultAsync();

            if (assessmentQuery != null)
            {
                assessmentQuery.CourseId = courseId;
                assessmentQuery.AssessmentName = name;
                assessmentQuery.AssessmentDescription = description;
                assessmentQuery.AssessmentType = type;
                assessmentQuery.IsPassed = isPassed;
            }

            await db.UpdateAsync(assessmentQuery);
        }
            

        #endregion

        #region Demo methods

        public static async void LoadSampleData()
        {
            await Init();

            var term = new Term("Fall Term", DateTime.Parse("2023-01-09"), DateTime.Parse("2023-11-01"));
            await db.InsertAsync(term);

            var term2 = new Term("Winter Term", DateTime.Parse("2023-01-01"), DateTime.Parse("2023-05-01"));
            await db.InsertAsync(term2);

            var term3 = new Term("Spring Term", DateTime.Parse("2023-04-01"), DateTime.Parse("2023-07-01"));
            await db.InsertAsync(term3);

            var term4 = new Term("summer Term", DateTime.Parse("2023-07-01"), DateTime.Parse("2023-09-01"));
            await db.InsertAsync(term4);

            var course1 = new Course("C101", "English", "Prerequisite English", DateTime.Parse("2023-01-01"),DateTime.Parse("2023-05-01"), term.Id);
            await db.InsertAsync(course1);

            var course2 = new Course("C347", "Public Speaking", "Develop and reinforce public speaking skills", DateTime.Parse("2023-01-01"), DateTime.Parse("2023-05-01"), term2.Id);
            await db.InsertAsync(course2);

            var course3 = new Course("C767", "Python 2", "Algorithms and data analysis", DateTime.Parse("2023-01-01"), DateTime.Parse("2023-05-01"), term3.Id);
            await db.InsertAsync(course3);

            var course4 = new Course("Test123", "Test Course", "Testing the edit term page", DateTime.Parse("2023-04-01"), DateTime.Parse("2023-07-01"), term.Id);
            await db.InsertAsync(course4);

            var instructor1 = new CourseInstructor(course1.Id, "Joe Lopez", "Joe@joepez.com", "2035409326");
            await db.InsertAsync(instructor1);

            var assessment = new CourseAssessment("Final Test", "final test for testing","Objective",false, course1.Id);
            await db.InsertAsync(assessment);
        }

        public static async void ClearSampleData()
        {
            await Init();

            await db.DropTableAsync<Course>();
            await db.DropTableAsync<Term>();

            db = null;
            
        }

        public static async void LoadSampleDataSQL()
        {
            await Init();
        }

        #endregion

        #region Count Determinations
        public static async Task<int> GetCourseCountAsync(int selectedTermId)
        {
            int courseCount = await db.ExecuteScalarAsync<int>($" SELECT COUNT(*) FROM Course WHERE TermId = ?", selectedTermId);
            return courseCount;
        }

        public static async Task<int> GetInstructorCountAsync(int selectedCourseId)
        {
            int instructorCount = await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM CourseInstructor WHERE CourseId = ?", selectedCourseId);
            return instructorCount;
        }

        

        #endregion

        #region Looping methods

        public static async void LoopingTermTable()
        {
             await Init();

            var allTermRecords = await db.QueryAsync<Term>("SELECT * FROM Term");
            foreach (var termRecord in allTermRecords)
            {
                Console.WriteLine("Name: " + termRecord.TermName);

            }
        }

        public static async Task<List<Term>> GetNotifyTermAsync()
        {
            await Init();
            var records = await db.QueryAsync<Term>("SELECT * FROM Term");

            return records;
        }

        public static async Task<IEnumerable<Term>> GetNotificationTerms()
        {
            await Init();
            var allTermRecords = await db.QueryAsync<Term>("SELECT * FROM Term");

            return allTermRecords;
        }

        #endregion








    }
}
