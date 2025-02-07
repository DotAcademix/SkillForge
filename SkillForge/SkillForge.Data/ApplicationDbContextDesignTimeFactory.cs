using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Monkey.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SkillForge.Data.Entities; -- To be added.
using SkillForge.Data;

namespace Monkey.Data.Data
{
    public class ApplicationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            if (args.Length != 1) throw new InvalidOperationException
                ("You need to pass the connection string to use the only argument!");
            string connectionString = args[0];

            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}