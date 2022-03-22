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
        public class Lecture
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Theme { get; set; }
            public string Lecture_body { get; set; }
            public int Teacher_id { get; set; }
        }
        private List<Course> CoursesIdList = new List<Course>();
        private List<Lecture> LecturesIdList = new List<Lecture>();

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
           foreach (Course i in ApplicatonContext.GetCourses())
            {
                if (i.Teacher_id == currentTeacherId)
                {
                    coursesList.Items.Add(i.Name);
                    CoursesIdList.Add(i);
                } 
            }
        }

        public void DisplayLectures(int currentTeacherId)
        {
            foreach (Lecture l in ApplicatonContext.GetLectures())
            {
                if (l.Teacher_id == currentTeacherId)
                {
                    LecturesList.Items.Add(l.Name);
                    LecturesIdList.Add(l);
                }
            }
        }

        public bool isLogined = false,
            isNewUser = true;
        public void authorizationUsers ()
        {
            foreach (Student i in ApplicatonContext.GetStudents())
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
                foreach (Teacher i in ApplicatonContext.GetTeachers())
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

        

        public void authorizationBtn(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                authorizationUsers();
                if (isNewUser)
                {
                    autoText.Text = "new";
                    ApplicatonContext.AddStudent(new Student()
                    {
                        Name = loginBox.Text,
                        Lastname = "",
                        Login = loginBox.Text,
                        Password = passwordBox.Password,
                        Course_id = null

                    });
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
            if (AddCourseTextBox.Text != "")
            {
                ApplicatonContext.AddCourse(new Course()
                {
                    Name = AddCourseTextBox.Text.Trim(),
                    Teacher_id = CurrentTeacher.Id
                });
                if (ApplicatonContext.GetCourses()[ApplicatonContext.GetCourses().Count - 1].Name == AddCourseTextBox.Text)
                {
                    coursesList.Items.Add(ApplicatonContext.GetCourses()[ApplicatonContext.GetCourses().Count - 1].Name);
                }
                
            } 
        }

        private void EditCourseBtn(object sender, RoutedEventArgs e)
        {
            Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem);
            EditCourse.Content = i.Id;
        }

        private void LectureButton_Click(object sender, RoutedEventArgs e)
        {
            TeacherProfile.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
            if (CurrentUser == "Student")
            {
                DisplayLectures(CurrentStudent.Id);

            }
            else
            {
                DisplayLectures(CurrentTeacher.Id);
            }
        }

        private void AddLecturesButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddLectureTextBox.Text != "")
            {
                ApplicatonContext.AddLecture(new Lecture()
                {
                    Name = AddLectureTextBox.Text.Trim(),
                    Teacher_id = CurrentTeacher.Id
                });
                if (ApplicatonContext.GetLectures()[ApplicatonContext.GetLectures().Count - 1].Name == AddLectureTextBox.Text)
                {
                    LecturesList.Items.Add(ApplicatonContext.GetLectures()[ApplicatonContext.GetLectures().Count - 1].Name);
                }

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
