using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WGU_App.Models
{
    public class Term
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TermName { get; set; }

        public Term(string termName)
        {
            TermName = termName;
        }

        public Term() { }
    }
}
