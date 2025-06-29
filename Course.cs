using System.Collections.Generic;

namespace WpfAppLab
{
    public class Course
    {
        public string CourseName { get; set; }
        public Teacher AssignedTeacher { get; set; }
        public List<Student> Students { get; set; }

        public Course(string courseName, Teacher teacher, List<Student> students)
        {
            CourseName = courseName;
            AssignedTeacher = teacher;
            Students = students;
        }

        public override string ToString()
        {
            return $"{CourseName} - {AssignedTeacher?.Name ?? "No Teacher"} ({Students.Count} students)";
        }
    }
}
