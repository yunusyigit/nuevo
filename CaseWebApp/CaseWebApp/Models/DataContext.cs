using CaseWebApp.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseWebApp.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Users> Users{ get; set; }
        public DbSet<URLList> URLList { get; set; }
        public DbSet<LogList> LogList { get; set; }


    }
}
