using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppLab
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> Students { get; set; } = new();
        public ObservableCollection<Teacher> Teachers { get; set; } = new();
        public ObservableCollection<Course> Courses { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            StudentList.ItemsSource = Students;
            TeacherList.ItemsSource = Teachers;
            CourseList.ItemsSource = Courses;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CopyStudent_Executed, CopyStudent_CanExecute));
        }

        private void CopyStudent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = StudentList.SelectedItem is Student;
        }

        private void CopyStudent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (StudentList.SelectedItem is Student student)
            {
                Clipboard.SetData("WpfAppLab.Student", student);
            }
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            string firstName = StudentFirstNameBox.Text.Trim();
            string lastName = StudentLastNameBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                Students.Add(new Student($"{firstName} {lastName}"));
                StudentFirstNameBox.Clear();
                StudentLastNameBox.Clear();
            }
        }

        private void RemoveStudentItem_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Student student)
                Students.Remove(student);
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TeacherFirstNameBox.Text.Trim();
            string lastName = TeacherLastNameBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                Teachers.Add(new Teacher($"{firstName} {lastName}"));
                TeacherFirstNameBox.Clear();
                TeacherLastNameBox.Clear();
            }
        }

        private void RemoveTeacherItem_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Teacher teacher)
                Teachers.Remove(teacher);
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            var courseWindow = new CourseWindow(Students, Teachers);
            if (courseWindow.ShowDialog() == true && courseWindow.CreatedCourse != null)
            {
                Courses.Add(courseWindow.CreatedCourse);
            }
        }

        private void RemoveCourseItem_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Course course)
                Courses.Remove(course);
        }

        private void CourseList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CourseList.SelectedItem is Course selectedCourse)
            {
                var editWindow = new CourseWindow(Students, Teachers, selectedCourse);
                if (editWindow.ShowDialog() == true && editWindow.CreatedCourse != null)
                {
                    int index = Courses.IndexOf(selectedCourse);
                    Courses[index] = editWindow.CreatedCourse;
                }
            }
        }
    }
}
