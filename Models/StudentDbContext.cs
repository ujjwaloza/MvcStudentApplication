using Microsoft.EntityFrameworkCore;

namespace MvcStudentApp.Models
{
    public class StudentDbContext : DbContext
    {
        // ✅ Correct EF Core constructor — just pass 'options' to base
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
