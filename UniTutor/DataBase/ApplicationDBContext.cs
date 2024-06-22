using Microsoft.EntityFrameworkCore;
using UniTutor.Model;
namespace UniTutor.DataBase;


public class ApplicationDBContext:DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admin{ get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Request> Request { get; set; }


}
