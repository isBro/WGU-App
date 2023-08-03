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

        public static async Task AddTerm(string termName)
        {

            await Init();

            var term = new Term()
            {
                TermName = termName

            };
            await db.InsertAsync(term);
            var id = term.Id;
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();
            await db.DeleteAsync(id);
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

        public static async Task UpdateTerm(int id, string name)
        {
            await Init();

            var termQuery = await db.Table<Term>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.TermName = name;
            }

            await db.UpdateAsync(termQuery);

        }


        #endregion
        #region Course methods

        public static async Task AddCourse(int termId, string name, string title,  string description, DateTime startDate, DateTime endDate)
        {
            await Init();
            var course = new Course
            {
                TermId = termId,
                Name = name,
                Title = title,
                Description = description,
                StartDate = startDate,
                EndDate = endDate

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
                .Where(i => i.Id == termId) 
                .ToListAsync();

            return courses;
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();
            var courses = await db.Table<Course>().ToListAsync();
           
            return courses;


        }

        public static async Task UpdateCourse(int id, string name, string title, string description, DateTime startDate, DateTime endDate)
        {
            await Init();

            var courseQuery = await db.Table<Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.Title = title;
                courseQuery.Description = description;
                courseQuery.StartDate = startDate;
                courseQuery.EndDate = endDate;

                await db.UpdateAsync(courseQuery);
            }
        }


        #endregion
        #region CourseInstructor methods
        #endregion
        #region  CourseAssessment methods
        #endregion
        #region Demo methods

        public static async void LoadSampleData()
        {
            await Init();

            var term = new Term("Fall Term");
            await db.InsertAsync(term);

            var term2 = new Term("Winter Term");
            await db.InsertAsync(term2);

            var term3 = new Term("Spring Term");
            await db.InsertAsync(term3);

            var term4 = new Term("summer Term");
            await db.InsertAsync(term4);

            var course1 = new Course("C101", "English", "Prerequisite English", DateTime.Parse("2023-01-01"),DateTime.Parse("2023-05-01"), term.Id );
            await db.InsertAsync(course1);

            var course2 = new Course("C347", "Public Speaking", "Develop and reinforce public speaking skills", DateTime.Parse("2023-01-01"), DateTime.Parse("2023-05-01"), term2.Id);
            await db.InsertAsync(course2);

            var course3 = new Course("C767", "Python 2", "Algorithms and data analysis", DateTime.Parse("2023-01-01"), DateTime.Parse("2023-05-01"), term3.Id);
            await db.InsertAsync(course3);
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
            int courseCount = await db.ExecuteScalarAsync<int>($" SELECT COUNT(*) FROM Term WHERE TermId = ?", selectedTermId);
            return courseCount;
        }

        #endregion
        #region Looping methods

        public static async void LoopingTermTable()
        {
             await Init();

            var allTermRecords = await db.QueryAsync<Term>("SELECT * FROM Gadget");
            foreach (var termRecord in allTermRecords)
            {
                Console.WriteLine("Name: " + termRecord.TermName);

            }
        }

        public static async Task<List<Term>> GetNotifyTermAsync()
        {
            await Init();
            var records = await db.QueryAsync<Term>("SELECT * FROM Gadget");

            return records;
        }

        public static async Task<IEnumerable<Term>> GetNotificationTerms()
        {
            await Init();
            var allTermRecords = await db.QueryAsync<Term>("SELECT * FROM Gadget");

            return allTermRecords;
        }

        #endregion







    }
}
