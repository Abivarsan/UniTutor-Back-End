using Microsoft.EntityFrameworkCore;
using UniTutor.Model;
namespace UniTutor.DataBase;


public class ApplicationDBContext:DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
   // public DbSet<Review> Reviews { get; set; }
    public DbSet<Request> Requests { get; set; }


}
