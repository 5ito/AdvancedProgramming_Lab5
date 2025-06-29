using System;

namespace WpfAppLab
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }

        public Student(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            return obj is Student s && s.Name == Name;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
