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
            loginBox.Focus();
        }
        public Teacher CurrentTeacher;
        public Student CurrentStudent;
        public string CurrentUser;
        private Course CurrnetCourse;
        private Lecture CurrnetLecture;
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
        public class Test
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Theme { get; set; }
            public int Teacher_id { get; set; }
        }
        public class Test_Question
        {
            public int Id { get; set; }
            public string Question_body { get; set; }
            public int Test_id { get; set; }
        }
        public class Test_Answer
        {
            public int Id { get; set; }
            public string Answer_body { get; set; }
            public bool is_true_answer { get; set; }
            public int Question_id { get; set; }
        }
        public class Test_to_Course
        {
            public int Id { get; set; }
            public int Test_id { get; set; }
            public int Course_id { get; set; }
        }
        public class Estimation
        {
            public int Id { get; set; }
            public int Test_id { get; set; }
            public int Student_id { get; set; }
            public int Mark { get; set; }
            public int Max_mark { get; set; }
        }
        public class EstimationToDisplay
        {
            public int Id { get; set; }
            public string Test_Name { get; set; }
            public string Student_Name { get; set; }
            public int Mark { get; set; }
            public int Max_mark { get; set; }
            public string Teacher_id { get; set; }
        }
        private bool isStudentCoursesDisplay = true;
        private bool isLectureDisplayFromCourse = false;
        private bool isTestOpenFromCourse = true;





        // Авторизация 
        public void authorizationBtn(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                authorizationUsers();
                if (isNewUser)
                {
                    ApplicatonContext.AddStudent(new Student()
                    {
                        Name = loginBox.Text,
                        Lastname = null,
                        Login = loginBox.Text,
                        Password = passwordBox.Password,
                        Course_id = null
                    });
                    authorizationUsers();
                }
                if (!isLogined)
                {
                    passwordBox.Password = "";
                    passwordBox.Focus();
                }
            }
        }
        public bool isLogined = false,
            isNewUser = true;
        public void authorizationUsers()
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





        // Студент-преподаватель
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
        public void DisplayStudents()
        {
            studentsList.Items.Clear();
            foreach (Student s in ApplicatonContext.GetStudents())
            {
                studentsList.Items.Add(s.Login);
            }
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
            studentNameFromTeacher.Text = studentsList.SelectedItem.ToString();
            Student i = ApplicatonContext.GetStudents().Find(item => item.Login == studentsList.SelectedItem.ToString());
            students.Visibility = Visibility.Hidden;
            AddStudentPage.Visibility = Visibility;
            StudentsToCourseList.Items.Clear();
            StudentsToCourseBox.Items.Clear();
            foreach (Student_to_Course s in ApplicatonContext.GetStudentsToCourses(i.Id))
            {
                Course item = ApplicatonContext.GetCourses(CurrentTeacher.Id).Find(i => i.Id == s.Course_id);
                if (item == null)
                {
                    continue;
                }
                StudentsToCourseList.Items.Add(item.Name);
            }
            DisplayStudentToCourse(CurrentTeacher.Id, StudentsToCourseBox, i.Id);
        }
        private void removeStudent_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null)
            {
                return;
            }
            List<Student> s = ApplicatonContext.GetStudents();
            Student currentStudent = s.Find(item => item.Login == studentsList.SelectedItem.ToString());
            ApplicatonContext.RemoveStudent(currentStudent.Id);
            studentsList.Items.Remove(studentsList.SelectedItem);
        }





        // Студент-курс
        private void StudentToCourseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null || StudentsToCourseBox.SelectedItem == null)
            {
                return;
            }
            List<Student> students = ApplicatonContext.GetStudents();
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Student SelectedStudent = students.Find(i => i.Login == studentsList.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == StudentsToCourseBox.SelectedItem.ToString());
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
            DisplayStudentToCourseForStudent(StudentCoursesList, CurrentStudent.Id);
        }
        private void StudentCoursesReturn_Click(object sender, RoutedEventArgs e)
        {
            isStudentCoursesDisplay = true;
            StudentCourses.Visibility = Visibility.Hidden;
            StudentProfile.Visibility = Visibility;
        }
        private void studentCourseReturn_Click(object sender, RoutedEventArgs e)
        {
            StudentCourses.Visibility = Visibility;
            studentCourse.Visibility = Visibility.Hidden;
        }
        private void StudentToCourseRemove_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsToCourseList.SelectedItem == null || studentsList.SelectedItem == null)
            {
                return;
            }
            List<Student> students = ApplicatonContext.GetStudents();
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Student SelectedStudent = students.Find(item => item.Login == studentsList.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == StudentsToCourseList.SelectedItem.ToString());
            List<Student_to_Course> Students_To_Courses = ApplicatonContext.GetStudentsToCourses(SelectedStudent.Id);
            Student_to_Course currentStudent_To_Course = Students_To_Courses.Find(item => item.Course_id == SelectedCourse.Id && item.Student_id == SelectedStudent.Id);
            ApplicatonContext.RemoveStudentToCourse(currentStudent_To_Course.Id);
            StudentsToCourseBox.Items.Add(StudentsToCourseList.SelectedItem);
            StudentsToCourseList.Items.Remove(StudentsToCourseList.SelectedItem);
        }





        //Студент-лекция
        private void StudentLectureButton_Click(object sender, RoutedEventArgs e)
        {
            isStudentCoursesDisplay = false;
            isLectureDisplayFromCourse = false;
            StudentProfile.Visibility = Visibility.Hidden;
            StudentCourses.Visibility = Visibility;
            DisplayAllStudentLectures(StudentCoursesList, CurrentStudent.Id);
        }
        private void studentLectureReturn_Click(object sender, RoutedEventArgs e)
        {
            if (isLectureDisplayFromCourse)
            {
                studentLecture.Visibility = Visibility.Hidden;
                studentCourse.Visibility = Visibility;
            }
            else
            {
                studentLecture.Visibility = Visibility.Hidden;
                StudentCourses.Visibility = Visibility;
            }
        }





        // Курс
        public void DisplayCourses (int currentTeacherId, dynamic name)
        {
            name.Items.Clear();
           foreach (Course i in ApplicatonContext.GetCourses(currentTeacherId))
            {
                name.Items.Add(i.Name);
            }
        }
        public void DisplayStudentToCourse(int currentTeacherId, dynamic name, int currentStudentId)
        {
            foreach (Course c in ApplicatonContext.GetStudentToCourse(currentTeacherId, currentStudentId))
            {
                name.Items.Add(c.Name);
            }
        }
        public void DisplayStudentToCourseForStudent(dynamic name, int currentStudentId)
        {
            name.Items.Clear();
            foreach (Course c in ApplicatonContext.GetStudentToCourseForStudent(currentStudentId))
            {
                TextBlock tb = new TextBlock();
                tb.Text = c.Name;
                tb.Uid = c.Id.ToString();
                name.Items.Add(tb);
            }
        }
        private void CoursesBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCourseTextBox.Text = "";
            TeacherProfile.Visibility = Visibility.Hidden;
            courses.Visibility = Visibility;
            if (CurrentUser == "Student")
            {
                DisplayCourses(CurrentStudent.Id, coursesList);

            }
            else
            {
                DisplayCourses(CurrentTeacher.Id, coursesList);
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
                    AddCourseTextBox.Text = "";
                }
            }
        }
        private void EditCourseBtn(object sender, RoutedEventArgs e)
        {
            if (coursesList.SelectedItem == null)
            {
                return;
            }
            List<Course> courseList = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Course i = courseList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Lecture_to_Course> lc = ApplicatonContext.GetLecturesToCourses(i.Id);
            courses.Visibility = Visibility.Hidden;
            EditCoursePage.Visibility = Visibility;
            LecturesToCourseList.Items.Clear();
            LecturesToCourseBox.Items.Clear();
            TestsToCourseList.Items.Clear();
            TestsToCourseBox.Items.Clear();
            foreach (Lecture l in ApplicatonContext.GetLecturesFromCourse(i.Id))
            {
                LecturesToCourseList.Items.Add(l.Name);
            }
            DisplayLectureToCourse(LecturesToCourseBox, i.Id);
            foreach (Test t in ApplicatonContext.GetTestToCourseInclude(i.Id))
            {
                TestsToCourseList.Items.Add(t.Name);
            }
            DisplayTestToCourse(TestsToCourseBox, i.Id);
        }
        private void couresReturn_Click(object sender, RoutedEventArgs e)
        {
            courses.Visibility = Visibility.Hidden;
            TeacherProfile.Visibility = Visibility;
        }
        private void editCoursesReturn_Click(object sender, RoutedEventArgs e)
        {
            EditCoursePage.Visibility = Visibility.Hidden;
            courses.Visibility = Visibility;
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
                TextBlock tb = new TextBlock();
                tb = (TextBlock)StudentCoursesList.SelectedItem;
                CurrnetLecture = ApplicatonContext.GetAllStudentLectures(CurrentStudent.Id).Find(item => item.Id == Convert.ToInt32(tb.Uid));
                studentLectureName.Text = CurrnetLecture.Name;
                studentLectureTheme.Text = CurrnetLecture.Theme;
                studentLectureBody.Text = CurrnetLecture.Lecture_body;
                studentLecture.Visibility = Visibility;
            }
            else
            {
                TextBlock tb = new TextBlock();
                tb = (TextBlock)StudentCoursesList.SelectedItem;
                CurrnetCourse = ApplicatonContext.GetStudentToCourseForStudent(CurrentStudent.Id).Find(item => item.Id == Convert.ToInt32(tb.Uid));
                studentCourseLecturesList.Items.Clear();
                studentCourseTestsList.Items.Clear();
                studentCourse.Visibility = Visibility;
                DisplayLecturesFromCourse(studentCourseLecturesList, CurrnetCourse.Id);
                foreach (Test t in ApplicatonContext.GetTestToCourseInclude(CurrnetCourse.Id))
                {
                    TextBlock ttb = new TextBlock();
                    ttb.Text = t.Name;
                    ttb.Uid = t.Id.ToString();
                    studentCourseTestsList.Items.Add(ttb);
                }
            }
        }
        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (coursesList.SelectedItem == null)
            {
                return;
            }
            List<Course> i = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Course currentCourse = i.Find(item => item.Name == coursesList.SelectedItem.ToString());
            ApplicatonContext.RemoveCourse(currentCourse.Id);
            coursesList.Items.Remove(coursesList.SelectedItem);
        }





        // Лекция
        public void DisplayLectures(int currentTeacherId, dynamic name)
        {
            name.Items.Clear();
            foreach (Lecture l in ApplicatonContext.GetLectures(currentTeacherId))
            {
                name.Items.Add(l.Name);
            }
        }
        private void LectureButton_Click(object sender, RoutedEventArgs e)
        {
            AddLectureTextBox.Text = "";
            TeacherProfile.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
            if (CurrentUser == "Student")
            {
                DisplayLectures(CurrentStudent.Id, LecturesList);

            }
            else
            {
                DisplayLectures(CurrentTeacher.Id, LecturesList);
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
               
                ApplicatonContext.AddLecture(NewLecture, CurrentTeacher.Id);
                if (ApplicatonContext.IsAddedLecture)
                {
                    LecturesList.Items.Add(ApplicatonContext.GetLectures(CurrentTeacher.Id)[ApplicatonContext.GetLectures(CurrentTeacher.Id).Count - 1].Name);
                    AddLectureTextBox.Text = "";
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
            EditLectureName.Text = LecturesList.SelectedItem.ToString();
            EditLecturePage.Visibility = Visibility;
            EditLectureTheme.Text = SelectedLecture.Theme;
            EditLectureBody.Text = SelectedLecture.Lecture_body;
        }
        private void EditLectureSave_Click(object sender, RoutedEventArgs e)
        {
            AddLectureTextBox.Text = "";
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            List<Lecture> i = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            Lecture SelectedLecture = i.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.EditLecture(SelectedLecture.Id, new Lecture()
            {
                Name = EditLectureName.Text,
                Theme = EditLectureTheme.Text,
                Lecture_body = EditLectureBody.Text,
                Teacher_id = SelectedLecture.Teacher_id
            });
            DisplayLectures(CurrentTeacher.Id, LecturesList);
            EditLecturePage.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
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
        private void openLectureFromCourse_Click(object sender, RoutedEventArgs e)
        {
            if (studentCourseLecturesList.SelectedItem == null)
            {
                return;
            }
            isLectureDisplayFromCourse = true;
            studentCourse.Visibility = Visibility.Hidden;
            TextBlock tb = new TextBlock();
            tb = (TextBlock)studentCourseLecturesList.SelectedItem;
            Lecture lecture = ApplicatonContext.GetAllStudentLectures(CurrentStudent.Id).Find(item => item.Id == Convert.ToInt32(tb.Uid));
            studentLectureName.Text = lecture.Name;
            studentLectureTheme.Text = lecture.Theme;
            studentLectureBody.Text = lecture.Lecture_body;
            studentLecture.Visibility = Visibility;
        }
        private void removeLecture_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            List<Lecture> i = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            Lecture currentLecture = i.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.RemoveLecture(currentLecture.Id);
            LecturesList.Items.Remove(LecturesList.SelectedItem);
        }
        public void DisplayAllStudentLectures(dynamic name, int currentStudentId)
        {
            name.Items.Clear();
            foreach (Lecture l in ApplicatonContext.GetAllStudentLectures(currentStudentId))
            {
                TextBlock tb = new TextBlock();
                tb.Text = l.Name;
                tb.Uid = l.Id.ToString();
                name.Items.Add(tb);
            }
        }





        //Лекция-курс
        private void LectureToCourseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesToCourseBox.SelectedItem == null || coursesList.SelectedItem == null)
            {
                return;
            }
            List<Lecture> lectures = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Lecture SelectedLecture = lectures.Find(item => item.Name == LecturesToCourseBox.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == coursesList.SelectedItem.ToString());
            ApplicatonContext.EditLectureToCourse(new Lecture_to_Course()
            {
                Lecture_id = SelectedLecture.Id,
                Course_id = SelectedCourse.Id
            });
            LecturesToCourseList.Items.Add(LecturesToCourseBox.SelectedItem);
            LecturesToCourseBox.Items.Remove(LecturesToCourseBox.SelectedItem);
        }
        private void LectureToCourseRemove_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesToCourseList.SelectedItem == null || coursesList.SelectedItem == null)
            {
                return;
            }
            List<Lecture> lectures = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Lecture SelectedLecture = lectures.Find(item => item.Name == LecturesToCourseList.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Lecture_to_Course> lecture_To_Courses = ApplicatonContext.GetLecturesToCourses(SelectedCourse.Id);
            Lecture_to_Course currentLecture_To_Course = lecture_To_Courses.Find(item => item.Course_id == SelectedCourse.Id && item.Lecture_id == SelectedLecture.Id);
            ApplicatonContext.RemoveLectureToCourse(currentLecture_To_Course.Id);
            LecturesToCourseBox.Items.Add(LecturesToCourseList.SelectedItem);
            LecturesToCourseList.Items.Remove(LecturesToCourseList.SelectedItem);
        }
        public void DisplayLectureToCourse(dynamic name, int currentCourseId)
        {

            foreach (Lecture l in ApplicatonContext.GetLectureToCourse(currentCourseId))
            {
                name.Items.Add(l.Name);
            }
        }
        public void DisplayLecturesFromCourse(dynamic name, int currentCourseId)
        {

            foreach (Lecture l in ApplicatonContext.GetLecturesFromCourse(currentCourseId))
            {
                TextBlock tb = new TextBlock();
                tb.Text = l.Name;
                tb.Uid = l.Id.ToString();
                name.Items.Add(tb);
            }
        }





        //Тест
        public void DisplayTests(int currentTeacherId, dynamic name)
        {
            name.Items.Clear();
            foreach (Test t in ApplicatonContext.GetTests(currentTeacherId))
            {
                name.Items.Add(t.Name);
            }
        }
        public void DisplayTestQuestions(int currentTestId, dynamic name)
        {
            name.Items.Clear();
            foreach (Test_Question t in ApplicatonContext.GetTestQuestions(currentTestId))
            {
                name.Items.Add(t.Question_body);
            }
        }
        public void DisplayTestAnswers(int currentQuestionId, dynamic name)
        {
            name.Items.Clear();
            foreach (Test_Answer t in ApplicatonContext.GetTestAnswers(currentQuestionId))
            {
                name.Items.Add(t.Answer_body);
            }
        }
        private void EditTest_Click(object sender, RoutedEventArgs e)
        {
            testAddQuestionBox.Text = "";
            if (TestsList.SelectedItem == null)
            {
                return;
            }
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == TestsList.SelectedItem.ToString());
            tests.Visibility = Visibility.Hidden;
            lectureNamelabel.Text = "Вопросы";
            EditTestPage.Visibility = Visibility;
            EditTestName.Text = TestsList.SelectedItem.ToString();
            EditTestTheme.Text = SelectedTest.Theme;
            DisplayTestQuestions(SelectedTest.Id, testQuestionsListBox);
        } 
        private void EditTestSave_Click(object sender, RoutedEventArgs e)
        {
            if (TestsList.SelectedItem == null)
            {
                return;
            }
            List<Test> i = ApplicatonContext.GetTests(CurrentTeacher.Id);
            Test SelectedTest = i.Find(item => item.Name == TestsList.SelectedItem.ToString());
            ApplicatonContext.EditTest(SelectedTest.Id, new Test()
            {
                Name = EditTestName.Text,
                Theme = EditTestTheme.Text,
                Teacher_id = SelectedTest.Teacher_id
            });
            DisplayTests(CurrentTeacher.Id, TestsList);
            EditTestPage.Visibility = Visibility.Hidden;
            tests.Visibility = Visibility;
        }
        private void EditTestReturn_Click(object sender, RoutedEventArgs e)
        {
            EditTestPage.Visibility = Visibility.Hidden;
            tests.Visibility = Visibility;
        }
        private void testsReturn_Click(object sender, RoutedEventArgs e)
        {
            tests.Visibility = Visibility.Hidden;
            TeacherProfile.Visibility = Visibility;
        }
        private void OpenTest_Click(object sender, RoutedEventArgs e)
        {
            isTestOpenFromCourse = false;
            currentQuesionIndex = 0;
            trueAnswersCounter = 0;
            maxTestMarks = 0;
            TextBlock tb = new TextBlock();
            tb = (TextBlock)StudentTestsList.SelectedItem;
            CurrentTest = ApplicatonContext.GetAllStudentTests(CurrentStudent.Id).Find(i => i.Id == Convert.ToInt32(tb.Uid));
            StudentTests.Visibility = Visibility.Hidden;
            test.Visibility = Visibility;
            renderTestQuestion(CurrentTest.Id, currentQuesionIndex);
        }
        private int currentQuesionIndex = 0;
        private int trueAnswersCounter = 0;
        private int maxTestMarks = 0;
        private Test CurrentTest;
        private void openTestFromCourse_Click(object sender, RoutedEventArgs e)
        {
            isTestOpenFromCourse = true;
            studentCourse.Visibility = Visibility.Hidden;
            currentQuesionIndex = 0;
            trueAnswersCounter = 0;
            maxTestMarks = 0;
            TextBlock tb = new TextBlock();
            tb = (TextBlock)studentCourseTestsList.SelectedItem;
            CurrentTest = ApplicatonContext.GetAllStudentTests(CurrentStudent.Id).Find(i => i.Id == Convert.ToInt32(tb.Uid));
            test.Visibility = Visibility;
            renderTestQuestion(CurrentTest.Id, currentQuesionIndex);
        }
        private void testExit_Click(object sender, RoutedEventArgs e)
        {
            TestFinishedPage.Visibility = Visibility.Hidden;
            testQuestionBox.Visibility = Visibility;
            testAnserButton.Visibility = Visibility;
            testAnsersContainer.Visibility = Visibility;
            if (isTestOpenFromCourse)
            {
                studentCourse.Visibility = Visibility;

            }
            else
            {
                StudentTests.Visibility = Visibility;
            }
            test.Visibility = Visibility.Hidden;
        }
        private void renderTest(ListBox name)
        {
            bool isAnswerTrue = false;
            var allButtons = testAnsersContainer.Children.OfType<dynamic>().ToList();
            List<Test_Answer> answers = ApplicatonContext.GetTestAnswers(ApplicatonContext.GetTestQuestions(CurrentTest.Id)[currentQuesionIndex].Id);
            List<dynamic> selectedAnswers = allButtons.FindAll(i => i.IsChecked);
            if (selectedAnswers == null && allButtons.Count > 0)
            {
                return;
            }
            if (allButtons.Count == 0)
            {
                trueAnswersCounter++;
            }
            else
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    if (answers[i].is_true_answer == allButtons[i].IsChecked)
                    {
                        isAnswerTrue = true;
                    }
                    else
                    {
                        isAnswerTrue = false;
                        break;
                    }
                }
                if (isAnswerTrue)
                {
                    trueAnswersCounter++;
                }

                if (isAnswerTrue && allButtons[0] is System.Windows.Controls.CheckBox)
                {
                    trueAnswersCounter++;
                }
            }
            maxTestMarks++;
            if (allButtons.Count != 0 && allButtons[0] is System.Windows.Controls.CheckBox)
            {
                maxTestMarks++;
            }
            currentQuesionIndex++;
            if (currentQuesionIndex == ApplicatonContext.GetTestQuestions(CurrentTest.Id).Count)
            {
                testQuestionBox.Visibility = Visibility.Hidden;
                testAnserButton.Visibility = Visibility.Hidden;
                testAnsersContainer.Visibility = Visibility.Hidden;
                TestFinishedPage.Text = $"Ваш результат: {trueAnswersCounter} из {maxTestMarks}.";
                TestFinishedPage.Visibility = Visibility;
                ApplicatonContext.AddEstimation(new Estimation()
                {
                    Student_id = CurrentStudent.Id,
                    Test_id = CurrentTest.Id,
                    Mark = trueAnswersCounter,
                    Max_mark = maxTestMarks
                });
            }
            else
            {
                renderTestQuestion(CurrentTest.Id, currentQuesionIndex);
            }
        }
        public void renderTestQuestion(int testId, int questionId)
        {
            List<Test_Question> testQuestions = ApplicatonContext.GetTestQuestions(testId);
            if (testQuestions.Count == 0)
            {
                testQuestionBox.Text = "В этом тесте пока нет вопросов :)";
                testAnsersContainer.Children.Clear();
                testAnserButton.IsEnabled = false;
                testAnserButton.Foreground = Brushes.LightGray;
            }
            else
            {
                testAnsersContainer.Children.Clear();
                testAnserButton.IsEnabled = true;
                testAnserButton.Foreground = Brushes.AliceBlue;
                testQuestionBox.Text = testQuestions[questionId].Question_body;
                if (ApplicatonContext.GetTestAnswers(testQuestions[questionId].Id).Count == 0)
                {
                    testAbsenceAnswers.Visibility = Visibility;
                    testAnsersContainer.Visibility = Visibility.Hidden;
                    return;
                }
                else
                {
                    testAnsersContainer.Visibility = Visibility;
                    testAbsenceAnswers.Visibility = Visibility.Hidden;
                    int TrueAnswerCount = 0;
                    foreach (Test_Answer ans in ApplicatonContext.GetTestAnswers(testQuestions[questionId].Id))
                    {
                        if (ans.is_true_answer)
                        {
                            TrueAnswerCount++;
                        }
                    }
                    foreach (Test_Answer ans in ApplicatonContext.GetTestAnswers(testQuestions[questionId].Id))
                    {
                        if (TrueAnswerCount > 1)
                        {
                            CheckBox cb = new CheckBox();
                            cb.Content = ans.Answer_body;
                            cb.IsChecked = ans.is_true_answer;
                            testAnsersContainer.Children.Add(cb);
                        }
                        else
                        {
                            RadioButton rb = new RadioButton();
                            rb.Content = ans.Answer_body;
                            rb.IsChecked = ans.is_true_answer;
                            testAnsersContainer.Children.Add(rb);
                        }
                    }
                }
            }
        }
        private void testAnserButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTestOpenFromCourse)
            {
                renderTest(studentCourseTestsList);
            }
            else
            {
                renderTest(StudentTestsList);
            }
        }
        private void removeTest_Click(object sender, RoutedEventArgs e)
        {
            if (TestsList.SelectedItem == null)
            {
                return;
            }
            List<Test> i = ApplicatonContext.GetTests(CurrentTeacher.Id);
            Test currentTest = i.Find(item => item.Name == TestsList.SelectedItem.ToString());
            ApplicatonContext.RemoveTest(currentTest.Id);
            TestsList.Items.Remove(TestsList.SelectedItem);
        }
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            AddTestTextBox.Text = "";
            TeacherProfile.Visibility = Visibility.Hidden;
            tests.Visibility = Visibility;
            DisplayTests(CurrentTeacher.Id, TestsList);
        }
        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddTestTextBox.Text != "")
            {
                Test NewTest = new Test()
                {
                    Name = AddTestTextBox.Text.Trim(),
                    Teacher_id = CurrentTeacher.Id
                };
                ApplicatonContext.AddTest(NewTest, CurrentTeacher.Id);
                if (ApplicatonContext.IsAddedTest)
                {
                    TestsList.Items.Add(ApplicatonContext.GetTests(CurrentTeacher.Id)[ApplicatonContext.GetTests(CurrentTeacher.Id).Count - 1].Name);
                    AddLectureTextBox.Text = "";
                }
            }
        }
        private void testAddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (testAddQuestionBox.Text != "")
            {
                Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == TestsList.SelectedItem.ToString());
                Test_Question NewTestQuestion = new Test_Question()
                {
                    Question_body = testAddQuestionBox.Text.Trim(),
                    Test_id = SelectedTest.Id
                };
                ApplicatonContext.AddTestQuestion(NewTestQuestion, SelectedTest.Id);
                if (ApplicatonContext.IsAddedTestQuestion)
                {
                    testQuestionsListBox.Items.Add(ApplicatonContext.GetTestQuestions(SelectedTest.Id)[ApplicatonContext.GetTestQuestions(SelectedTest.Id).Count - 1].Question_body);
                    testAddQuestionBox.Text = "";
                }
            }
        }
        private void removeTestQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (testQuestionsListBox.SelectedItem == null)
            {
                return;
            }
            List<Test_Question> i = ApplicatonContext.GetTestQuestions(SelectedTest.Id);
            Test_Question currentTestQuestion = i.Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            ApplicatonContext.RemoveTestQuestion(currentTestQuestion.Id);
            testQuestionsListBox.Items.Remove(testQuestionsListBox.SelectedItem);
        }
        Test SelectedTest;
        private void EditTestQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddTestAnswerTextBox.Text = "";
            SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == TestsList.SelectedItem.ToString());
            if (testQuestionsListBox.SelectedItem == null)
            {
                return;
            }
            List<Test_Question> i = ApplicatonContext.GetTestQuestions(SelectedTest.Id);
            Test_Question currentTestQuestion = i.Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            EditEssense.Visibility = Visibility.Hidden;
            TestQuestionEditTextBox.Text = currentTestQuestion.Question_body;
            DisplayTestAnswers(currentTestQuestion.Id, TestAnswersList);
            testAnswersEdit.Visibility = Visibility;

        }
        private void EditTestAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (TestAnswersList.SelectedItem == null)
            {
                return;
            }
            testAnswersEdit.Visibility = Visibility.Hidden;
            Test_Question currentTestQuestion = ApplicatonContext.GetTestQuestions(SelectedTest.Id).Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            Test_Answer currentTestAnswer = ApplicatonContext.GetTestAnswers(currentTestQuestion.Id).Find(item => item.Answer_body == TestAnswersList.SelectedItem.ToString());
            testAnswerEditTextBox.Text = currentTestAnswer.Answer_body;
            testAnswerEditCheckBox.IsChecked = currentTestAnswer.is_true_answer;
            testAnswerEdit.Visibility = Visibility;
        }
        private void DeleteTestAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (TestAnswersList.SelectedItem == null)
            {
                return;
            }
            Test_Question currentTestQuestion = ApplicatonContext.GetTestQuestions(SelectedTest.Id).Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            Test_Answer currentTestAnswer = ApplicatonContext.GetTestAnswers(currentTestQuestion.Id).Find(item => item.Answer_body == TestAnswersList.SelectedItem.ToString());
            ApplicatonContext.RemoveTestAnswer(currentTestAnswer.Id);
            TestAnswersList.Items.Remove(TestAnswersList.SelectedItem);
        }
        private void TestAnswerReturn_Click(object sender, RoutedEventArgs e)
        {
            testAnswersEdit.Visibility = Visibility.Hidden;
            EditEssense.Visibility = Visibility;
        }
        private void AddTestAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddTestAnswerTextBox.Text != "")
            {
                List<Test_Question> i = ApplicatonContext.GetTestQuestions(SelectedTest.Id);
                Test_Question currentTestQuestion = i.Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
                Test_Answer NewTestAnswer = new Test_Answer()
                {
                    Answer_body = AddTestAnswerTextBox.Text.Trim(),
                    Question_id = currentTestQuestion.Id
                };
                ApplicatonContext.AddTestAnswer(NewTestAnswer, currentTestQuestion.Id);
                if (ApplicatonContext.IsAddedTestAnswer)
                {
                    TestAnswersList.Items.Add(ApplicatonContext.GetTestAnswers(currentTestQuestion.Id)[ApplicatonContext.GetTestAnswers(currentTestQuestion.Id).Count - 1].Answer_body);
                    AddTestAnswerTextBox.Text = "";
                }
            }
        }
        private void TestAnswerEditReturn_Click(object sender, RoutedEventArgs e)
        {
            testAnswerEdit.Visibility = Visibility.Hidden;
            testAnswersEdit.Visibility = Visibility;
        }
        private void EditTestAnswerSave_Click(object sender, RoutedEventArgs e)
        {
            if (TestAnswersList.SelectedItem == null)
            {
                return;
            }
            Test_Question currentTestQuestion = ApplicatonContext.GetTestQuestions(SelectedTest.Id).Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            Test_Answer currentTestAnswer = ApplicatonContext.GetTestAnswers(currentTestQuestion.Id).Find(item => item.Answer_body == TestAnswersList.SelectedItem.ToString());
            ApplicatonContext.EditTestAnswer(currentTestAnswer.Id, new Test_Answer()
            {
                Answer_body = testAnswerEditTextBox.Text,
                is_true_answer = Convert.ToBoolean(testAnswerEditCheckBox.IsChecked),
                Question_id = currentTestQuestion.Id
            });
            DisplayTestAnswers(currentTestQuestion.Id, TestAnswersList);
            testAnswerEdit.Visibility = Visibility.Hidden;
            testAnswersEdit.Visibility = Visibility;
        }
        private void TestQuestionSaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (testQuestionsListBox.SelectedItem == null)
            {
                return;
            }
            Test_Question currentTestQuestion = ApplicatonContext.GetTestQuestions(SelectedTest.Id).Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            ApplicatonContext.EditTestQuestion(currentTestQuestion.Id, new Test_Question()
            {
                Question_body = TestQuestionEditTextBox.Text,
                Test_id = SelectedTest.Id
            });
            DisplayTestQuestions(SelectedTest.Id, testQuestionsListBox);
        }
        public void DisplayAllStudentTests(dynamic name, int currentStudentId)
        {
            name.Items.Clear();
            foreach (Test t in ApplicatonContext.GetAllStudentTests(currentStudentId))
            {
                TextBlock tb = new TextBlock();
                tb.Text = t.Name;
                tb.Uid = t.Id.ToString();
                name.Items.Add(tb);
            }
        }





        //Тест-курс
        private void TestToCourseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TestsToCourseBox.SelectedItem == null || coursesList.SelectedItem == null)
            {
                return;
            }
            List<Test> tests = ApplicatonContext.GetTests(CurrentTeacher.Id);
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Test SelectedTest = tests.Find(item => item.Name == TestsToCourseBox.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == coursesList.SelectedItem.ToString());

            ApplicatonContext.EditTestToCourse(new Test_to_Course()
            {
                Test_id = SelectedTest.Id,
                Course_id = SelectedCourse.Id
            });
            TestsToCourseList.Items.Add(TestsToCourseBox.SelectedItem);
            TestsToCourseBox.Items.Remove(TestsToCourseBox.SelectedItem);
        }
        private void TestToCourseRemove_Click(object sender, RoutedEventArgs e)
        {
            if (TestsToCourseList.SelectedItem == null || coursesList.SelectedItem == null)
            {
                return;
            }
            List<Test> tests = ApplicatonContext.GetTests(CurrentTeacher.Id);
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Test SelectedTest = tests.Find(item => item.Name == TestsToCourseList.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Test_to_Course> test_To_Courses = ApplicatonContext.GetTestsToCourses(SelectedCourse.Id);
            Test_to_Course CurrentTest_To_Course = test_To_Courses.Find(item => item.Test_id == SelectedTest.Id);
            ApplicatonContext.RemoveTestToCourse(CurrentTest_To_Course.Id);
            TestsToCourseBox.Items.Add(TestsToCourseList.SelectedItem);
            TestsToCourseList.Items.Remove(TestsToCourseList.SelectedItem);
        }
        public void DisplayTestToCourse(dynamic name, int currentCourseId)
        {
            foreach (Test t in ApplicatonContext.GetTestToCourse(currentCourseId))
            {
                name.Items.Add(t.Name);
            }
        }





        //Тест-студент
        private void StudentTestButton_Click(object sender, RoutedEventArgs e)
        {
            StudentProfile.Visibility = Visibility.Hidden;
            StudentTests.Visibility = Visibility;
            DisplayAllStudentTests(StudentTestsList, CurrentStudent.Id);
        }
        private void StudentTestReturn_Click(object sender, RoutedEventArgs e)
        {
            StudentTests.Visibility = Visibility.Hidden;
            StudentProfile.Visibility = Visibility;
        }





        // Оценки
        public EstimationToDisplay EstimationTableFilling(Estimation est)
        {
            Teacher teacher = ApplicatonContext.GetTeachers().Find(t => t.Id == ApplicatonContext.GetTest(est.Test_id).Teacher_id);
            Student student = ApplicatonContext.GetStudents().Find(i => i.Id == est.Student_id);
            return new EstimationToDisplay()
            {
                Id = est.Id,
                Student_Name = student.Name + " " + student.Lastname,
                Test_Name = ApplicatonContext.GetTest(est.Test_id).Name,
                Mark = est.Mark,
                Max_mark = est.Max_mark,
                Teacher_id = teacher.Name + " " + teacher.Lastname
            };
        }
        private void EstimationReturn_Click(object sender, RoutedEventArgs e)
        {
            estimations.Visibility = Visibility.Hidden;
            StudentProfile.Visibility = Visibility;
        }
        private void EstimationTeacherReturn_Click(object sender, RoutedEventArgs e)
        {
            estimations.Visibility = Visibility.Hidden;
            TeacherProfile.Visibility = Visibility;
        }

        private void StudentEstimationButton_Click(object sender, RoutedEventArgs e)
        {
            StudentProfile.Visibility = Visibility.Hidden;
            estimations.Visibility = Visibility;
            EstimationReturn.Click -= EstimationTeacherReturn_Click;
            EstimationReturn.Click += EstimationReturn_Click;
            List<EstimationToDisplay> estimationTableSourse = new List<EstimationToDisplay>();
            foreach(Estimation est in ApplicatonContext.GetEstimationsForStudent(CurrentStudent.Id))
            {
                estimationTableSourse.Add(EstimationTableFilling(est));
            }

            estimationTable.ItemsSource = estimationTableSourse;
        }

        private void TeacherEstimationButton_Click(object sender, RoutedEventArgs e)
        {
            TeacherProfile.Visibility = Visibility.Hidden;
            estimations.Visibility = Visibility;
            EstimationReturn.Click -= EstimationReturn_Click;
            EstimationReturn.Click += EstimationTeacherReturn_Click;
            List<EstimationToDisplay> estimationTableSourse = new List<EstimationToDisplay>();
            foreach(Estimation est in ApplicatonContext.GetEstimationsForTeacher(CurrentTeacher.Id))
            {
                estimationTableSourse.Add(EstimationTableFilling(est));
            }
            estimationTable.ItemsSource = estimationTableSourse;
        }
    }
}
