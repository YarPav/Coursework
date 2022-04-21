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
        public class Lecture_to_Course
        {
            public int Id { get; set; }
            public int Lecture_id { get; set; }
            public int Course_id { get; set; }
        }
        public class Student_to_Course
        {
            public int Id { get; set; }
            public int Student_id { get; set; }
            public int Course_id { get; set; }
        }
        private List<Course> CoursesIdList = new List<Course>();
        private List<Lecture> LecturesIdList = new List<Lecture>();
        private List<Student> StudentsIdList = new List<Student>();
        private bool isStudentCoursesDisplay = true;
        private bool isLectureDisplayFromCourse = false;

        public void DisplayStudentProfile (Student i)
        {
            authorization.Visibility = Visibility.Hidden;
            studentName.Content = i.Name;
            StudentProfile.Visibility = Visibility;
        }
        public void DisplayTeacherProfile (Teacher i)
        {
            authorization.Visibility = Visibility.Hidden;
            userName.Text = i.Name + " " + i.Lastname;
            TeacherProfile.Visibility = Visibility;
        }

        public void DisplayCourses (int currentTeacherId, dynamic name, List<Course> list)
        {
            name.Items.Clear();
           foreach (Course i in ApplicatonContext.GetCourses(currentTeacherId))
            {
                name.Items.Add(i.Name);
                if (list != null)
                {
                    list.Add(i);
                }
            }
        }

        public void DisplayLectures(int currentTeacherId, dynamic name, List<Lecture> list)
        {
            name.Items.Clear();
            foreach (Lecture l in ApplicatonContext.GetLectures(currentTeacherId))
            {
                name.Items.Add(l.Name);
                if (list != null)
                {
                    list.Add(l);
                }
            }
        }

        public void DisplayLectureToCourse (dynamic name, List<Lecture> list, int currentCourseId)
        {
            
            foreach (Lecture l in ApplicatonContext.GetLectureToCourse(currentCourseId))
            {
                name.Items.Add(l.Name);
                if (list != null)
                {
                    list.Add(l);
                }
            }
        }
        public void DisplayLecturesFromCourse(dynamic name, List<Lecture> list, int currentCourseId)
        {

            foreach (Lecture l in ApplicatonContext.GetLecturesFromCourse(currentCourseId))
            {
                name.Items.Add(l.Name);
                if (list != null)
                {
                    list.Add(l);
                }
            }
        }
        public void DisplayStudents ()
        {
            studentsList.Items.Clear();
            foreach (Student s in ApplicatonContext.GetStudents())
            {
                studentsList.Items.Add(s.Name);
                StudentsIdList.Add(s);
            }
          
        }

        public void DisplayStudentToCourse(int currentTeacherId, dynamic name, List<Course> list, int currentStudentId)
        {
            foreach (Course c in ApplicatonContext.GetStudentToCourse(currentTeacherId, currentStudentId))
            {
                name.Items.Add(c.Name);
                if (list != null)
                {
                    list.Add(c);
                }
            }
        }

        public void DisplayStudentToCourseForStudent(dynamic name, List<Course> list, int currentStudentId)
        {
            name.Items.Clear();
            foreach (Course c in ApplicatonContext.GetStudentToCourseForStudent(currentStudentId))
            {
                name.Items.Add(c.Name);
                if (list != null)
                {
                    list.Add(c);
                }
            }
        }
        public void DisplayAllStudentLectures(dynamic name, List<Lecture> list, int currentStudentId)
        {
            name.Items.Clear();
            foreach (Lecture c in ApplicatonContext.GetAllStudentLectures(currentStudentId))
            {
                name.Items.Add(c.Name);
                if (list != null)
                {
                    list.Add(c);
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
                        Lastname = loginBox.Text,
                        Login = loginBox.Text,
                        Password = passwordBox.Password,
                        Course_id = null

                    });
                    authorizationUsers();
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
                DisplayCourses(CurrentStudent.Id, coursesList, CoursesIdList);
                
            } else
            {
                DisplayCourses(CurrentTeacher.Id, coursesList, CoursesIdList);
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
                }, CurrentTeacher.Id);
                
                if (ApplicatonContext.IsAddedCourse)
                {
                    coursesList.Items.Add(ApplicatonContext.GetCourses(CurrentTeacher.Id)[ApplicatonContext.GetCourses(CurrentTeacher.Id).Count - 1].Name);
                }
                
            } 
        }

        private void EditCourseBtn(object sender, RoutedEventArgs e)
        {
            if (coursesList.SelectedItem == null)
            {
                return;
            }
            Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Lecture_to_Course> lc = ApplicatonContext.GetLecturesToCourses(i.Id);
            courses.Visibility = Visibility.Hidden;
            EditCoursePage.Visibility = Visibility;
            LecturesToCourseList.Items.Clear();
            LecturesToCourseBox.Items.Clear();
            foreach (Lecture_to_Course l in ApplicatonContext.GetLecturesToCourses(i.Id))
            {
                LecturesToCourseList.Items.Add(ApplicatonContext.GetLectures(CurrentTeacher.Id).Find(i => i.Id == l.Lecture_id).Name);
            }
            DisplayLectureToCourse(LecturesToCourseBox, LecturesIdList, i.Id);
        }

        private void LectureButton_Click(object sender, RoutedEventArgs e)
        {
            TeacherProfile.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
            if (CurrentUser == "Student")
            {
                DisplayLectures(CurrentStudent.Id, LecturesList, LecturesIdList);

            }
            else
            {
                DisplayLectures(CurrentTeacher.Id, LecturesList, LecturesIdList);
            }
        }

        private void AddLecturesButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddLectureTextBox.Text != "")
            {
                Lecture NewLecture = new Lecture()
                {
                    Name = AddLectureTextBox.Text.Trim(),
                    Teacher_id = CurrentTeacher.Id
                };
                LecturesIdList.Add(NewLecture);
                ApplicatonContext.AddLecture(NewLecture, CurrentTeacher.Id);
                if (ApplicatonContext.IsAddedLecture)
                {
                    LecturesList.Items.Add(ApplicatonContext.GetLectures(CurrentTeacher.Id)[ApplicatonContext.GetLectures(CurrentTeacher.Id).Count - 1].Name);
                }
            }
        }

        private void EditLecture_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            Lecture SelectedLecture = ApplicatonContext.GetLectures(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            lectures.Visibility = Visibility.Hidden;
            EditLecturePage.Visibility = Visibility;
            EditLectureName.Text = LecturesList.SelectedItem.ToString();
            EditLectureTheme.Text = SelectedLecture.Theme;
            EditLectureBody.Text = SelectedLecture.Lecture_body;
        }

        private void EditLectureSave_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            Lecture SelectedLecture = LecturesIdList.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.EditLecture(SelectedLecture.Id, new Lecture() 
            { 
                Name = EditLectureName.Text,
                Theme = EditLectureTheme.Text,
                Lecture_body = EditLectureBody.Text,
                Teacher_id = SelectedLecture.Teacher_id
            });
            EditLecturePage.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
        }

        private void LectureToCourseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesToCourseBox.SelectedItem == null || coursesList.SelectedItem == null)
            {
                return;
            }
            Lecture SelectedLecture = LecturesIdList.Find(item => item.Name == LecturesToCourseBox.SelectedItem.ToString());
            Course SelectedCourse = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            
            ApplicatonContext.EditCourse(new Lecture_to_Course()
            {
                Lecture_id = SelectedLecture.Id,
                Course_id = SelectedCourse.Id
            });
            LecturesToCourseList.Items.Add(LecturesToCourseBox.SelectedItem);
            LecturesToCourseBox.Items.Remove(LecturesToCourseBox.SelectedItem);
        }

        private void couresReturn_Click(object sender, RoutedEventArgs e)
        {
            courses.Visibility = Visibility.Hidden;
            TeacherProfile.Visibility = Visibility;
        }

        private void lecturesReturn_Click(object sender, RoutedEventArgs e)
        {
            lectures.Visibility = Visibility.Hidden;
            TeacherProfile.Visibility = Visibility;
        }

        private void editLecturesReturn_Click(object sender, RoutedEventArgs e)
        {
            EditLecturePage.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
        }

        private void editCoursesReturn_Click(object sender, RoutedEventArgs e)
        {
            EditCoursePage.Visibility = Visibility.Hidden;
            courses.Visibility = Visibility;
        }

        private void AddStudentsReturn_Click(object sender, RoutedEventArgs e)
        {
            AddStudentPage.Visibility = Visibility.Hidden;
            students.Visibility = Visibility;
        }
        private void StudentButton_Click(object sender, RoutedEventArgs e)
        {
            TeacherProfile.Visibility = Visibility.Hidden;
            students.Visibility = Visibility;
            DisplayStudents();
        }

        private void studentsReturn_Click(object sender, RoutedEventArgs e)
        {
            students.Visibility = Visibility.Hidden;
            TeacherProfile.Visibility = Visibility;
        }
        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null)
            {
                return;
            }
            Student i = StudentsIdList.Find(item => item.Name == studentsList.SelectedItem.ToString());
            List<Student_to_Course> sc = ApplicatonContext.GetStudentsToCourses(i.Id);
            students.Visibility = Visibility.Hidden;
            AddStudentPage.Visibility = Visibility;
            StudentsToCourseList.Items.Clear();
            StudentsToCourseBox.Items.Clear();
            foreach (Student_to_Course s in ApplicatonContext.GetStudentsToCourses(i.Id))
            {
                StudentsToCourseList.Items.Add(ApplicatonContext.GetCourses(CurrentTeacher.Id).Find(i => i.Id == s.Course_id).Name);
            }
            //DisplayLectureToCourse(CurrentTeacher.Id, LecturesToCourseBox, LecturesIdList, i.Id);
            DisplayStudentToCourse(CurrentTeacher.Id, StudentsToCourseBox, CoursesIdList, i.Id);
        }

        private void StudentToCourseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null || StudentsToCourseBox.SelectedItem == null)
            {
                return;
            }
            Student SelectedStudent = StudentsIdList.Find(i => i.Name == studentsList.SelectedItem.ToString());
            Course SelectedCourse = CoursesIdList.Find(item => item.Name == StudentsToCourseBox.SelectedItem.ToString());
            ApplicatonContext.AddStudentToCourse(new Student_to_Course()
            {
                Student_id = SelectedStudent.Id,
                Course_id = SelectedCourse.Id
            });
            StudentsToCourseList.Items.Add(StudentsToCourseBox.SelectedItem);
            StudentsToCourseBox.Items.Remove(StudentsToCourseBox.SelectedItem);
        }

        private void StudentCoursesButton_Click(object sender, RoutedEventArgs e)
        {
            StudentProfile.Visibility = Visibility.Hidden;
            StudentCourses.Visibility = Visibility;
            DisplayStudentToCourseForStudent(StudentCoursesList, CoursesIdList, CurrentStudent.Id);
        }

        private void StudentLectureButton_Click(object sender, RoutedEventArgs e)
        {
            isStudentCoursesDisplay = false;
            StudentProfile.Visibility = Visibility.Hidden;
            StudentCourses.Visibility = Visibility;
            DisplayAllStudentLectures(StudentCoursesList, LecturesIdList, CurrentStudent.Id);
        }    

        private void StudentCoursesReturn_Click(object sender, RoutedEventArgs e)
        {
            isStudentCoursesDisplay = true;
            StudentCourses.Visibility = Visibility.Hidden;
            StudentProfile.Visibility = Visibility;
        }

        private void OpenCourse_Click(object sender, RoutedEventArgs e)
        {
            if (StudentCoursesList.SelectedItem == null)
            {
                return;
            }
            StudentCourses.Visibility = Visibility.Hidden;
            if (!isStudentCoursesDisplay)
            {
                Lecture lecture = LecturesIdList.Find(item => item.Name == StudentCoursesList.SelectedItem.ToString());
                studentLectureName.Text = lecture.Name;
                studentLectureTheme.Text = lecture.Theme;
                studentLectureBody.Text = lecture.Lecture_body;
                studentLecture.Visibility = Visibility;
            } else
            {
                studentCourseLecturesList.Items.Clear();
                studentCourse.Visibility = Visibility;
                Course i = CoursesIdList.Find(item => item.Name == StudentCoursesList.SelectedItem.ToString());
                DisplayLecturesFromCourse(studentCourseLecturesList, LecturesIdList, i.Id);
            }
            
        }

        private void studentLectureReturn_Click(object sender, RoutedEventArgs e)
        {
            if (isLectureDisplayFromCourse)
            {
                studentLecture.Visibility = Visibility.Hidden;
                studentCourse.Visibility = Visibility;
            } else
            {
                studentLecture.Visibility = Visibility.Hidden;
                StudentCourses.Visibility = Visibility;
            }
            
        }

        private void studentCourseReturn_Click(object sender, RoutedEventArgs e)
        {
            StudentCourses.Visibility = Visibility;
            studentCourse.Visibility = Visibility.Hidden;
        }

        private void openLectureFromCourse_Click(object sender, RoutedEventArgs e)
        {
            if (studentCourseLecturesList.SelectedItem == null)
            {
                return;
            }
            isLectureDisplayFromCourse = true;
            studentCourse.Visibility = Visibility.Hidden;
            Lecture lecture = LecturesIdList.Find(item => item.Name == studentCourseLecturesList.SelectedItem.ToString());
            studentLectureName.Text = lecture.Name;
            studentLectureTheme.Text = lecture.Theme;
            studentLectureBody.Text = lecture.Lecture_body;
            studentLecture.Visibility = Visibility;
        }

        private void openTestFromCourse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void studentName_Click(object sender, RoutedEventArgs e)
        {
            StudentProfile.Visibility = Visibility.Hidden;
            studentSettingsName.Text = CurrentStudent.Name;
            studentSettingsLastname.Text = CurrentStudent.Lastname;
            studentSettings.Visibility = Visibility;

        }

        private void studentSettingSave_Click(object sender, RoutedEventArgs e)
        {
            Student s = new Student()
            {
                Id = CurrentStudent.Id,
                Name = studentSettingsName.Text,
                Lastname = studentSettingsLastname.Text,
                Password = CurrentStudent.Password,
                Login = CurrentStudent.Login,
                Course_id = CurrentStudent.Course_id
            };
            CurrentStudent = s;
            ApplicatonContext.EditStudent(CurrentStudent.Id, s);
            studentSettings.Visibility = Visibility.Hidden;
            studentName.Content = CurrentStudent.Name;
            StudentProfile.Visibility = Visibility;
        }

        private void studentSettingsReturn_Click(object sender, RoutedEventArgs e)
        {
            studentSettings.Visibility = Visibility.Hidden;
            StudentProfile.Visibility = Visibility;
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
