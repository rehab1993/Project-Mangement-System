using Microsoft.EntityFrameworkCore;
using Project_Mangement_System.Models;

namespace Project_Mangement_System.Data
{
    public class Context : DbContext
    {

        public Context():base() { 

        }


        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        //DbSets
        public DbSet<Project> Projects { get; set; }

    }

   
}
