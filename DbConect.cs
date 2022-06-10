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
        public static bool IsAddedTest = false;
        public static bool IsAddedTestQuestion = false;
        public static bool IsAddedTestAnswer = false;
        public DbSet<Teacher> teacher { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<Student_to_Course> student_to_course { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Lecture> lecture { get; set; }
        public DbSet<Lecture_to_Course> lecture_to_course { get; set; }
        public DbSet<Test> test { get; set; }
        public DbSet<Test_Question> test_question { get; set; }
        public DbSet<Test_Answer> test_answer { get; set; }
        public DbSet<Test_to_Course> test_to_course { get; set; }
        public DbSet<Estimation> estimation { get; set;  }
        public ApplicatonContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=;database=doplatform-release");
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
        public static List<Test> GetTests(int teacherId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test> t = new List<Test>();
                t = db.test.Where(test => test.Teacher_id == teacherId).ToList();
                return t;
            }
        }
        public static List<Test_Question> GetTestQuestions(int testId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test_Question> t = new List<Test_Question>();
                t = db.test_question.Where(test => test.Test_id == testId).ToList();
                return t;
            }
        }
        public static List<Test_Answer> GetTestAnswers(int questionId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test_Answer> t = new List<Test_Answer>();
                t = db.test_answer.Where(test => test.Question_id == questionId).ToList();
                return t;
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
        public static List<Test_to_Course> GetTestsToCourses(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test_to_Course> t = new List<Test_to_Course>();
                t = db.test_to_course.Where(test => test.Course_id == courseId).ToList();
                return t;
            }
        }
        public static List<Lecture> GetLectureToCourse(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Lecture> l = new List<Lecture>();
                
                l = db.lecture.FromSqlInterpolated($"SELECT * FROM `lecture` WHERE id NOT IN (SELECT Lecture_id FROM lecture_to_course WHERE course_id = {courseId}) AND Teacher_id = (SELECT Teacher_id FROM course WHERE Id = {courseId})").ToList();
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
        public static List<Test> GetTestToCourse(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test> t = new List<Test>();
                t = db.test.FromSqlInterpolated($"SELECT * FROM `test` WHERE id NOT IN (SELECT test_id FROM test_to_course WHERE course_id = {courseId}) AND Teacher_id = (SELECT Teacher_id FROM course WHERE Id = {courseId});").ToList();
                return t;
            }
        }
        public static List<Test> GetTestToCourseInclude(int courseId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test> t = new List<Test>();
                t = db.test.FromSqlInterpolated($"SELECT * FROM `test` WHERE id IN (SELECT test_id FROM test_to_course WHERE course_id = {courseId});").ToList();
                return t;
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
        public static List<Test> GetAllStudentTests(int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Test> t = new List<Test>();

                t = db.test.FromSqlInterpolated($"SELECT * FROM `test` WHERE Id IN (SELECT test_id FROM test_to_course WHERE course_id IN (SELECT course_id FROM student_to_course WHERE student_id = {studentId}))").ToList();
                return t;
            }
        }
        public static List<Estimation> GetEstimationsForStudent(int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Estimation> e = new List<Estimation>();
                e = db.estimation.FromSqlInterpolated($"SELECT * FROM `estimation` WHERE student_id = {studentId}").ToList();
                return e;
            }
        }
        public static List<Estimation> GetEstimationsForTeacher(int teacherId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Estimation> e = new List<Estimation>();
                e = db.estimation.FromSqlInterpolated($"SELECT * FROM `estimation` WHERE test_id IN (SELECT id FROM test WHERE teacher_id = {teacherId})").ToList();
                return e;
            }
        }
        public static Test GetTest(int testId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                return (Test)db.test.First(test => test.Id == testId);
            }
        }
        public static Student GetStudent(int studentId)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                return (Student)db.student.Where(student => student.Id == studentId);
            }
        }
        public static void AddEstimation(Estimation _e)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.estimation.Add(_e);
                db.SaveChanges();
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
                IsAddedCourse = false;
                Course i = GetCourses(currentTeacher).Find(item => item.Name == _c.Name);
                if (i == null)
                {
                    db.course.Add(_c);
                    db.SaveChanges();
                    IsAddedCourse = true;
                }     
            }
        }
        public static void RemoveCourse(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var c = db.course.Single(i => i.Id == id);
                db.course.Remove(c);
                db.SaveChanges();
            }
        }
        public static void RemoveLecture(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var l = db.lecture.Single(i => i.Id == id);
                db.lecture.Remove(l);
                db.SaveChanges();
            }
        }
        public static void RemoveTest(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var t = db.test.Single(i => i.Id == id);
                db.test.Remove(t);
                db.SaveChanges();
            }
        }
        public static void RemoveTestQuestion(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var t = db.test_question.Single(i => i.Id == id);
                db.test_question.Remove(t);
                db.SaveChanges();
            }
        }
        public static void RemoveTestAnswer(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var t = db.test_answer.Single(i => i.Id == id);
                db.test_answer.Remove(t);
                db.SaveChanges();
            }
        }
        public static void RemoveLectureToCourse(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var l = db.lecture_to_course.Single(i => i.Id == id);
                db.lecture_to_course.Remove(l);
                db.SaveChanges();
            }
        }
        public static void RemoveTestToCourse(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var t = db.test_to_course.Single(i => i.Id == id);
                db.test_to_course.Remove(t);
                db.SaveChanges();
            }
        }
        public static void RemoveStudent(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var s = db.student.Single(i => i.Id == id);
                db.student.Remove(s);
                db.SaveChanges();
            }
        }
        public static void RemoveStudentToCourse(int id)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var s = db.student_to_course.Single(i => i.Id == id);
                db.student_to_course.Remove(s);
                db.SaveChanges();
            }
        }
        public static void EditLectureToCourse(Lecture_to_Course _l_to_c)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.lecture_to_course.Add(_l_to_c);
                db.SaveChanges();
            }
        }
        public static void EditTestToCourse(Test_to_Course _t_to_c)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.test_to_course.Add(_t_to_c);
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
                IsAddedLecture = false;
                Lecture i = GetLectures(currentteacher).Find(item => item.Name == _l.Name);
                if (i == null)
                {
                    db.lecture.Add(_l);
                    db.SaveChanges();
                    IsAddedLecture = true;
                }
            }
        }
        public static void AddTest(Test _t, int currentteacher)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                IsAddedTest = false;
                Test i = GetTests(currentteacher).Find(item => item.Name == _t.Name);
                if (i == null)
                {
                    db.test.Add(_t);
                    db.SaveChanges();
                    IsAddedTest = true;
                }
            }
        }
        public static void AddTestQuestion(Test_Question _t, int currentTest)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                IsAddedTestQuestion = false;
                Test_Question i = GetTestQuestions(currentTest).Find(item => item.Question_body == _t.Question_body);
                if (i == null)
                {
                    db.test_question.Add(_t);
                    db.SaveChanges();
                    IsAddedTestQuestion = true;
                }
            }
        }
        public static void AddTestAnswer(Test_Answer _t, int currentQuestion)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                IsAddedTestAnswer = false;
                Test_Answer i = GetTestAnswers(currentQuestion).Find(item => item.Answer_body == _t.Answer_body);
                if (i == null)
                {
                    db.test_answer.Add(_t);
                    db.SaveChanges();
                    IsAddedTestAnswer = true;
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
        public static void EditTest(int id, Test _t)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var l = db.test.Single(i => i.Id == id);
                l.Name = _t.Name;
                l.Theme = _t.Theme;
                l.Teacher_id = _t.Teacher_id;
                db.SaveChanges();
            }
        }
        public static void EditTestQuestion(int id, Test_Question _t)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var t = db.test_question.Single(i => i.Id == id);
                t.Question_body = _t.Question_body;
                t.Test_id = _t.Test_id;
                db.SaveChanges();
            }
        }
        public static void EditTestAnswer(int id, Test_Answer _t)
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                var t = db.test_answer.Single(i => i.Id == id);
                t.Answer_body = _t.Answer_body;
                t.is_true_answer = _t.is_true_answer;
                t.Question_id = _t.Question_id;
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


