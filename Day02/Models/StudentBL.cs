using Day02.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Day02.Models
{
    public class StudentBL
    {
        SchoolDbContext db = new SchoolDbContext();
        public List<Student> GetAll()
        {
            return db.Students
                     .Include(S => S.Department)
                     .Include(S => S.StuCrsRes)
                     .ToList();
        }

        public Student? GetById(int id)
        {
            return db.Students
                     .Include(S => S.Department)
                     .Include(S => S.StuCrsRes)
                     .ThenInclude(R => R.Course) 
                     .FirstOrDefault(S => S.Id == id);
        }
    }
}
