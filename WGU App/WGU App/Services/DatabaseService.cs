using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WGU_App.Models;

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

        public static async void AddTerm(SQLiteAsyncConnection db, string termName)
        {

            await Init();

            var term = new Term()
            {
                TermName = termName

            };
            await db.InsertAsync(term);
            var id = term.Id;
        }




        #endregion
        #region Course methods
        #endregion
        #region CourseInstructor methods
        #endregion
        #region  CourseAssessment methods
        #endregion
        #region Demo methods
        #endregion







    }
}
