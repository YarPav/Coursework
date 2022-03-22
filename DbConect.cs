using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static Do_platform.MainWindow;

namespace Do_platform
{
    public class ApplicatonContext : DbContext
    {
        public DbSet<Teacher> teacher { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Lecture> lecture { get; set; }
        public ApplicatonContext()
        {
            Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=;database=doplatform");
        }
        public static List<Teacher> GetTeachers()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Teacher> t = new List<Teacher>();
                t = db.teacher.ToList();
                return t;
            }
        }
        public static List<Student> GetStudents()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Student> s = new List<Student>();
                s = db.student.ToList();
                return s;
            }
        }
        public static List<Course> GetCourses()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Course> c = new List<Course>();
                c = db.course.ToList();
                return c;
            }
        }

        public static List<Lecture> GetLectures()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture> l = new List<Lecture>();
                l = db.lecture.ToList();
                return l;
            }
        }

        public static void AddStudent(Student _s)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.student.Add(_s);
                db.SaveChanges();
            }
        }
        public static void AddCourse(Course _c)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                Course i = GetCourses().Find(item => item.Name == _c.Name);
                if (i == null)
                {
                    db.course.Add(_c);
                    db.SaveChanges();
                }           
            }
        }
        public static void AddLecture(Lecture _l)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                Lecture i = GetLectures().Find(item => item.Name == _l.Name);
                if (i == null)
                {
                    db.lecture.Add(_l);
                    db.SaveChanges();
                }
            }
        }
    }

}
