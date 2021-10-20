using AppForUniversity.DatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AppForUniversity.DatabaseImplement
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-KCR4TIA;Initial Catalog=UniversityDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<Course> Courses { set; get; }
    }
}