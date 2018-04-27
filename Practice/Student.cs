using System;

namespace MultiThreadingPractice
{
    [Serializable]
    public class Student
    {
        private string name;
        private int age;
        private Department department;

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public Department Department { get => department; set => department = value; }
    }

    public class Department
    {
        private string name;

        public string Name { get => name; set => name = value; }
    }
}