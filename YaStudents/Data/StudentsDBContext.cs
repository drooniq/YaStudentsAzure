using Microsoft.EntityFrameworkCore;
using YaStudents.Models;

namespace YaStudents.Data
{
    public class StudentsDBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentsDBContext(DbContextOptions<StudentsDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
