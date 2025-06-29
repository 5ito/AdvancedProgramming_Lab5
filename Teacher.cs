namespace WpfAppLab
{
    public class Teacher
    {
        public string Name { get; set; }

        public Teacher(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
