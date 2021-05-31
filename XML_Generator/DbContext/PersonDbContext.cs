using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using XML_Generator.Model;

namespace XML_Generator.Context
{
    public class PersonDbContext: DbContext
    {
        public DbSet<Person> People { get; set; }
        private static bool _created = false;
        public PersonDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=mydb.sqlite");
        }
    }
}
