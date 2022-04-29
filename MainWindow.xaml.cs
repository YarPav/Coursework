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
        public class Test
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Theme { get; set; }
            //public int test_time { get; set; }
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
        public void DisplayTestToCourse(dynamic name, int currentCourseId)
        {

            foreach (Test t in ApplicatonContext.GetTestToCourse(currentCourseId))
            {
                name.Items.Add(t.Name);
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
            AddCourseTextBox.Text = "";
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
            DisplayLectureToCourse(LecturesToCourseBox, LecturesIdList, i.Id);
            foreach (Test t in ApplicatonContext.GetTestToCourseInclude(i.Id))
            {
                TestsToCourseList.Items.Add(t.Name);
            }
            DisplayTestToCourse(TestsToCourseBox, i.Id);
        }

        private void LectureButton_Click(object sender, RoutedEventArgs e)
        {
            AddLectureTextBox.Text = "";
            addEssence.Click += AddLecturesButton_Click;
            addEssence.Click -= AddTestButton_Click;
            removeEssence.Click += removeLecture_Click;
            removeEssence.Click -= removeTest_Click;
            EditEssense.Click += EditLecture_Click;
            EditEssense.Click -= EditTest_Click;
            EditLectureSave.Click += EditLectureSave_Click;
            EditLectureSave.Click -= EditTestSave_Click;
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
            lectureNamelabel.Text = "Текст лекции";
            testAddQuestionBox.Visibility = Visibility.Hidden;
            testAddQuestionButton.Visibility = Visibility.Hidden;
            testQuestionsListBox.Visibility = Visibility.Hidden;
            lectureNameWrapper.Visibility = Visibility;
            removeTestQuestion.Visibility = Visibility.Hidden;
            EditTestQuestion.Visibility = Visibility.Hidden;
            EditLecturePage.Visibility = Visibility.Hidden;
            EditLectureName.Text = LecturesList.SelectedItem.ToString();
            EditLecturePage.Visibility = Visibility;
            EditLectureTheme.Text = SelectedLecture.Theme;
            EditLectureBody.Text = SelectedLecture.Lecture_body;
        }
        private void EditTest_Click(object sender, RoutedEventArgs e)
        {
            testAddQuestionBox.Text = "";
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            lectures.Visibility = Visibility.Hidden;
            lectureNamelabel.Text = "Вопросы";
            testAddQuestionBox.Visibility = Visibility;
            testAddQuestionButton.Visibility = Visibility;
            testQuestionsListBox.Visibility = Visibility;
            lectureNameWrapper.Visibility = Visibility.Hidden;
            EditLecturePage.Visibility = Visibility;
            removeTestQuestion.Visibility = Visibility;
            EditTestQuestion.Visibility = Visibility;
            EditLectureName.Text = LecturesList.SelectedItem.ToString();
            EditLectureTheme.Text = SelectedTest.Theme;
            DisplayTestQuestions(SelectedTest.Id, testQuestionsListBox);
            
        }

        private void EditLectureSave_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            //Lecture SelectedLecture = LecturesIdList.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            List<Lecture> i = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            Lecture SelectedLecture = i.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.EditLecture(SelectedLecture.Id, new Lecture() 
            { 
                Name = EditLectureName.Text,
                Theme = EditLectureTheme.Text,
                Lecture_body = EditLectureBody.Text,
                Teacher_id = SelectedLecture.Teacher_id
            });
            DisplayLectures(CurrentTeacher.Id, LecturesList, LecturesIdList);
            EditLecturePage.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
        }
        private void EditTestSave_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            //Lecture SelectedLecture = LecturesIdList.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            List<Test> i = ApplicatonContext.GetTests(CurrentTeacher.Id);
            Test SelectedTest = i.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.EditTest(SelectedTest.Id, new Test()
            {
                Name = EditLectureName.Text,
                Theme = EditLectureTheme.Text,
                //test_time = 1000,
                Teacher_id = SelectedTest.Teacher_id
            });
            DisplayTests(CurrentTeacher.Id, LecturesList);
            EditLecturePage.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
        }

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
            studentNameFromTeacher.Text = studentsList.SelectedItem.ToString();
            Student i = StudentsIdList.Find(item => item.Name == studentsList.SelectedItem.ToString());
            //List<Student_to_Course> sc = ApplicatonContext.GetStudentsToCourses(i.Id);
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
            //DisplayLectureToCourse(CurrentTeacher.Id, LecturesToCourseBox, LecturesIdList, i.Id);
            DisplayStudentToCourse(CurrentTeacher.Id, StudentsToCourseBox, CoursesIdList, i.Id);
        }

        private void StudentToCourseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null || StudentsToCourseBox.SelectedItem == null)
            {
                return;
            }
            List<Student> students = ApplicatonContext.GetStudents();
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Student SelectedStudent = students.Find(i => i.Name == studentsList.SelectedItem.ToString());
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

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (coursesList.SelectedItem == null)
            {
                return;
            }
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Course> i = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Course currentCourse = i.Find(item => item.Name == coursesList.SelectedItem.ToString());
            ApplicatonContext.RemoveCourse(currentCourse.Id);
            coursesList.Items.Remove(coursesList.SelectedItem);
        }

        private void LectureToCourseRemove_Click(object sender, RoutedEventArgs e)
        {
            
            if (LecturesToCourseList.SelectedItem == null || coursesList.SelectedItem == null)
            {
                return;
            }

            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
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

        private void removeLecture_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Lecture> i = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            Lecture currentLecture = i.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.RemoveLecture(currentLecture.Id);
            LecturesList.Items.Remove(LecturesList.SelectedItem);
        }
        private void removeTest_Click(object sender, RoutedEventArgs e)
        {
            if (LecturesList.SelectedItem == null)
            {
                return;
            }
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Test> i = ApplicatonContext.GetTests(CurrentTeacher.Id);
            Test currentTest = i.Find(item => item.Name == LecturesList.SelectedItem.ToString());
            ApplicatonContext.RemoveTest(currentTest.Id);
            LecturesList.Items.Remove(LecturesList.SelectedItem);
        }

        private void removeStudent_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null)
            {
                return;
            }
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Student> s = ApplicatonContext.GetStudents();
            Student currentStudent = s.Find(item => item.Name == studentsList.SelectedItem.ToString());
            ApplicatonContext.RemoveStudent(currentStudent.Id);
            studentsList.Items.Remove(studentsList.SelectedItem);
        }

        private void StudentToCourseRemove_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsToCourseList.SelectedItem == null || studentsList.SelectedItem == null)
            {
                return;
            }

            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Student> students = ApplicatonContext.GetStudents();
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Student SelectedStudent = students.Find(item => item.Name == studentsList.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == StudentsToCourseList.SelectedItem.ToString());
            List<Student_to_Course> Students_To_Courses = ApplicatonContext.GetStudentsToCourses(SelectedStudent.Id);
            Student_to_Course currentStudent_To_Course = Students_To_Courses.Find(item => item.Course_id == SelectedCourse.Id && item.Student_id == SelectedStudent.Id);
            ApplicatonContext.RemoveStudentToCourse(currentStudent_To_Course.Id);
            StudentsToCourseBox.Items.Add(StudentsToCourseList.SelectedItem);
            StudentsToCourseList.Items.Remove(StudentsToCourseList.SelectedItem);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            AddLectureTextBox.Text = "";
            TeacherProfile.Visibility = Visibility.Hidden;
            lectures.Visibility = Visibility;
            addEssence.Click -= AddLecturesButton_Click;
            addEssence.Click += AddTestButton_Click;
            removeEssence.Click -= removeLecture_Click;
            removeEssence.Click += removeTest_Click;
            EditEssense.Click -= EditLecture_Click;
            EditEssense.Click += EditTest_Click;
            EditLectureSave.Click -= EditLectureSave_Click;
            EditLectureSave.Click += EditTestSave_Click;
            DisplayTests(CurrentTeacher.Id, LecturesList);
        }
        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddLectureTextBox.Text != "")
            {
                Test NewTest = new Test()
                {
                    Name = AddLectureTextBox.Text.Trim(),
                    Teacher_id = CurrentTeacher.Id
                };
                ApplicatonContext.AddTest(NewTest, CurrentTeacher.Id);
                if (ApplicatonContext.IsAddedTest)
                {
                    LecturesList.Items.Add(ApplicatonContext.GetTests(CurrentTeacher.Id)[ApplicatonContext.GetTests(CurrentTeacher.Id).Count - 1].Name);
                    AddLectureTextBox.Text = "";
                }
            }
        }
        private void testAddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (testAddQuestionBox.Text != "")
            {
                Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
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
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Test_Question> i = ApplicatonContext.GetTestQuestions(SelectedTest.Id);
            Test_Question currentTestQuestion = i.Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            ApplicatonContext.RemoveTestQuestion(currentTestQuestion.Id);
            testQuestionsListBox.Items.Remove(testQuestionsListBox.SelectedItem);
        }

        private void EditTestQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddTestAnswerTextBox.Text = "";
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            List<Test_Question> i = ApplicatonContext.GetTestQuestions(SelectedTest.Id);
            Test_Question currentTestQuestion = i.Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            EditEssense.Visibility = Visibility.Hidden;
            TestQuestionEditTextBox.Text = currentTestQuestion.Question_body;
            DisplayTestAnswers(currentTestQuestion.Id, TestAnswersList);
            testAnswersEdit.Visibility = Visibility;

        }

        private void EditTestAnswer_Click(object sender, RoutedEventArgs e)
        {
            testAnswersEdit.Visibility = Visibility.Hidden;
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
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
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
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
                Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
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
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
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
            TestQuestionSaveButton.Content = "ok";
            Test SelectedTest = ApplicatonContext.GetTests(CurrentTeacher.Id).Find(item => item.Name == LecturesList.SelectedItem.ToString());
            Test_Question currentTestQuestion = ApplicatonContext.GetTestQuestions(SelectedTest.Id).Find(item => item.Question_body == testQuestionsListBox.SelectedItem.ToString());
            ApplicatonContext.EditTestQuestion(currentTestQuestion.Id, new Test_Question()
            {
                Question_body = TestQuestionEditTextBox.Text,
                Test_id = SelectedTest.Id
            });
            DisplayTestQuestions(SelectedTest.Id, testQuestionsListBox);
        }

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

            /*
            List<Lecture> lectures = ApplicatonContext.GetLectures(CurrentTeacher.Id);
            List<Course> courses = ApplicatonContext.GetCourses(CurrentTeacher.Id);
            Lecture SelectedLecture = lectures.Find(item => item.Name == LecturesToCourseList.SelectedItem.ToString());
            Course SelectedCourse = courses.Find(item => item.Name == coursesList.SelectedItem.ToString());
            List<Lecture_to_Course> lecture_To_Courses = ApplicatonContext.GetLecturesToCourses(SelectedCourse.Id);
            Lecture_to_Course currentLecture_To_Course = lecture_To_Courses.Find(item => item.Course_id == SelectedCourse.Id && item.Lecture_id == SelectedLecture.Id);
            ApplicatonContext.RemoveLectureToCourse(currentLecture_To_Course.Id);
            LecturesToCourseBox.Items.Add(LecturesToCourseList.SelectedItem);
            LecturesToCourseList.Items.Remove(LecturesToCourseList.SelectedItem);*/


            //Course i = CoursesIdList.Find(item => item.Name == coursesList.SelectedItem.ToString());
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
