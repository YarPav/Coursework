using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Do_platform
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public Teacher CurrentTeacher;
        public Student CurrentStudent;
        public string CurrentUser;
        public class Teacher
        {
            public string role = "Teacher";
            public int Id { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }
        public class Student
        {
            public string role = "Student";
            public int Id { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public int? Course_id {get; set;}

        }
        public class Course
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Teacher_id { get; set; }
        }
        public List<Teacher> getTeachers()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Teacher> t = new List<Teacher>();
                t = db.teacher.ToList();
                return t;
            }
        }
        public List<Student> getStudents()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Student> s = new List<Student>();
                s = db.student.ToList();
                return s;
            }
        }
        public List<Course> GetCourses()
        {
            using (ApplicatonContext db = new ApplicatonContext())
            {
                List<Course> c = new List<Course>();
                c = db.course.ToList();
                return c;
            }
        }

        public void DisplayStudentProfile (Student i)
        {
            authorization.Visibility = Visibility.Hidden;
            userName.Text = i.role + ": " + i.Name + " " + i.Lastname;
            
        }
        public void DisplayTeacherProfile (Teacher i)
        {
            authorization.Visibility = Visibility.Hidden;
            userName.Text = i.Name + " " + i.Lastname;
            TeacherProfile.Visibility = Visibility;
        }

        public void DisplayCourses (int currentTeacherId)
        {
           foreach (Course i in GetCourses())
            {
                if (i.Teacher_id == currentTeacherId)
                {
                    coursesList.Items.Add(i.Name);
                } 
            }
        }

        public bool isLogined = false,
            isNewUser = true;
        public void authorizationUsers ()
        {
            foreach (Student i in getStudents())
            {
                if (loginBox.Text == i.Login)
                {
                    isNewUser = false;
                    if (passwordBox.Password == i.Password)
                    {
                        CurrentUser = i.role;
                        CurrentStudent = i;
                        DisplayStudentProfile(i);
                        isLogined = true;
                        break;
                    }
                }
            }
            if (!isLogined)
            {
                foreach (Teacher i in getTeachers())
                {
                    if (loginBox.Text == i.Login)
                    {
                        isNewUser = false;
                        if (passwordBox.Password == i.Password)
                        {
                            CurrentUser = i.role;
                            CurrentTeacher = i;
                            DisplayTeacherProfile(i);
                            isLogined = true;
                            break;
                        }
                    }
                }
            }
        }

        public class ApplicatonContext : DbContext
        {
            public DbSet<Teacher> teacher { get; set; }
            public DbSet<Student> student { get; set; }
            public DbSet<Course> course { get; set; }
            public ApplicatonContext()
            {
                Database.EnsureCreated();
           
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=;database=doplatform");
            }
        }

        public void authorizationBtn(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                authorizationUsers();
                if (isNewUser)
                {
                    autoText.Text = "new";
                    Student newStudent = new Student()
                    {
                        Name = loginBox.Text,
                        Lastname = "",
                        Login = loginBox.Text,
                        Password = passwordBox.Password,
                        Course_id = null

                    };
                    using (ApplicatonContext db = new ApplicatonContext())
                    {
                        db.student.Add(newStudent);
                        db.SaveChanges();
                    }
                }
                if (!isLogined)
                {
                    passwordBox.Password = "";
                }
            }
        }

  
        private void CoursesBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherProfile.Visibility = Visibility.Hidden;
            courses.Visibility = Visibility;
            if (CurrentUser == "Student")
            {
                DisplayCourses(CurrentStudent.Id);
                
            } else
            {
                DisplayCourses(CurrentTeacher.Id);
            }
        }

        private void AddCoursesButton_Click(object sender, RoutedEventArgs e)
        {
            Course newCourse = new Course()
            {
                Name = AddCourseTextBox.Text,
                Teacher_id = CurrentTeacher.Id

            };
            using (ApplicatonContext db = new ApplicatonContext())
            {
                db.course.Add(newCourse);
                db.SaveChanges();
                coursesList.Items.Add(GetCourses()[GetCourses().Count - 1].Name);
            }
        }

        private void loginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text != "")
            {
                isNewUser = true;
                authorizationUsers();
                if (isNewUser)
                {
                    passBtn.Content = "Зарегистрироваться";
                    autoText.Text = "Регистрация";
                }
                else
                {
                    passBtn.Content = "Войти";
                    autoText.Text = "Авторизация";
                }
            }
        }
    }
}
