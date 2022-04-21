using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static Do_platform.MainWindow;

namespace Do_platform
{
    public class ApplicatonContext : DbContext
    {
        public static bool IsAddedCourse = false;
        public static bool IsAddedLecture = false;
        public DbSet<Teacher> teacher { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Lecture> lecture { get; set; }
        public DbSet<Lecture_to_Course> lecture_to_course { get; set; }
        public DbSet<Student_to_Course> student_to_course { get; set; }
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
        public static List<Course> GetCourses(int teacherId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Course> c = new List<Course>();
                c = db.course.Where(c => c.Teacher_id == teacherId).ToList();
                return c;
            }
        }

        public static List<Lecture> GetLectures(int teacherId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture> l = new List<Lecture>();
                l = db.lecture.Where(l => l.Teacher_id == teacherId).ToList();
                return l;
            }
        }
        public static List<Lecture_to_Course> GetLecturesToCourses(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture_to_Course> l = new List<Lecture_to_Course>();
                l = db.lecture_to_course.Where(l => l.Course_id == courseId).ToList();
                return l;
            }
        }
        public static List<Lecture> GetLectureToCourse(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture> l = new List<Lecture>();
                
                l = db.lecture.FromSqlInterpolated($"SELECT * FROM `lecture` WHERE id NOT IN (SELECT Lecture_id FROM lecture_to_course WHERE course_id = {courseId})").ToList();
                return l;
            }
        }
        public static List<Lecture> GetLecturesFromCourse(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture> l = new List<Lecture>();

                l = db.lecture.FromSqlInterpolated($"SELECT * FROM `lecture` WHERE id IN (SELECT Lecture_id FROM lecture_to_course WHERE course_id = {courseId})").ToList();
                return l;
            }
        }
        public static List<Student_to_Course> GetStudentsToCourses(int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Student_to_Course> s = new List<Student_to_Course>();
                s = db.student_to_course.Where(s => s.Student_id == studentId).ToList();
                return s;
            }
        }
        public static List<Course> GetStudentToCourse(int teacherId, int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Course> c = new List<Course>();

                c = db.course.FromSqlInterpolated($"SELECT * FROM `course` WHERE Teacher_id = {teacherId} AND id NOT IN (SELECT course_id FROM student_to_course WHERE student_id = {studentId})").ToList();
                return c;
            }
        }
        public static List<Course> GetStudentToCourseForStudent( int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Course> c = new List<Course>();

                c = db.course.FromSqlInterpolated($"SELECT * FROM `course` WHERE Id IN (SELECT Course_id FROM `student_to_course` WHERE student_id = {studentId})").ToList();
                return c;
            }
        }
        public static List<Lecture> GetAllStudentLectures(int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture> c = new List<Lecture>();

                c = db.lecture.FromSqlInterpolated($"SELECT * FROM `lecture` WHERE Id IN (SELECT lecture_id FROM lecture_to_course WHERE course_id IN (SELECT course_id FROM student_to_course WHERE student_id = {studentId}))").ToList();
                return c;
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
        public static void AddCourse(Course _c, int currentTeacher)
        {
            
            using (ApplicatonContext db = new ApplicatonContext())
            {
                Course i = GetCourses(currentTeacher).Find(item => item.Name == _c.Name);
                if (i == null)
                {
                    db.course.Add(_c);
                    db.SaveChanges();
                    IsAddedCourse = true;
                }     
            }
        }
        public static void EditCourse(Lecture_to_Course _l_to_c)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.lecture_to_course.Add(_l_to_c);
                db.SaveChanges();
            }
        }
        public static void AddStudentToCourse(Student_to_Course _s_to_c)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.student_to_course.Add(_s_to_c);
                db.SaveChanges();
            }
        }
        public static void AddLecture(Lecture _l, int currentteacher)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                Lecture i = GetLectures(currentteacher).Find(item => item.Name == _l.Name);
                if (i == null)
                {
                    db.lecture.Add(_l);
                    db.SaveChanges();
                    IsAddedLecture = true;
                }
            }
        }
        public static void EditLecture (int id, Lecture _l)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var l = db.lecture.Single(i => i.Id == id);
                l.Name = _l.Name;
                l.Theme = _l.Theme;
                l.Lecture_body = _l.Lecture_body;
                l.Teacher_id = _l.Teacher_id;
                db.SaveChanges();
            }
        }
        public static void EditStudent(int id, Student _s)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var s = db.student.Single(i => i.Id == id);
                s.Name = _s.Name;
                s.Lastname = _s.Lastname;
                s.Login = _s.Login;
                s.Password = _s.Password;
                s.Course_id = _s.Course_id;
                db.SaveChanges();
            }
        }
    }
}


