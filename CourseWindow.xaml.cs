using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppLab
{
    public partial class CourseWindow : Window
    {
        public Course CreatedCourse { get; private set; }

        private ObservableCollection<Student> AvailableStudents;
        private ObservableCollection<Teacher> AvailableTeachers;
        private ObservableCollection<Student> SelectedStudents = new();

        public CourseWindow(ObservableCollection<Student> students, ObservableCollection<Teacher> teachers, Course? courseToEdit = null)
        {
            InitializeComponent();

            AvailableStudents = new ObservableCollection<Student>(students);
            AvailableTeachers = teachers;

            AvailableStudentsBox.ItemsSource = AvailableStudents;
            SelectedStudentsBox.ItemsSource = SelectedStudents;
            TeacherComboBox.ItemsSource = AvailableTeachers;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteStudent_Executed, PasteStudent_CanExecute));

            if (courseToEdit != null)
            {
                Title = "Edit Course";
                ConfirmBtn.Content = "Save";
                CourseNameBox.Text = courseToEdit.CourseName;
                TeacherComboBox.SelectedItem = courseToEdit.AssignedTeacher;

                foreach (var student in courseToEdit.Students)
                {
                    SelectedStudents.Add(student);
                    AvailableStudents.Remove(student);
                }
            }
        }

        private void PasteStudent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsData("WpfAppLab.Student");
        }

        private void PasteStudent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.GetData("WpfAppLab.Student") is Student student && AvailableStudents.Contains(student))
            {
                AvailableStudents.Remove(student);
                SelectedStudents.Add(student);
            }
        }

        private void AvailableStudentsBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AvailableStudentsBox.SelectedItem is Student student)
            {
                AvailableStudents.Remove(student);
                SelectedStudents.Add(student);
            }
        }

        private void SelectedStudentsBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedStudentsBox.SelectedItem is Student student)
            {
                SelectedStudents.Remove(student);
                AvailableStudents.Add(student);
            }
        }

        private void RemoveFromSelected_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Student student)
            {
                SelectedStudents.Remove(student);
                AvailableStudents.Add(student);
            }
        }

        private void AddCourseWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CourseNameBox.Text) || TeacherComboBox.SelectedItem is not Teacher teacher)
            {
                MessageBox.Show("Please enter a course name and select a teacher.");
                return;
            }

            CreatedCourse = new Course(CourseNameBox.Text.Trim(), teacher, SelectedStudents.ToList());
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
