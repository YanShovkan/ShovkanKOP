using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Appanov
{
    class Human
    {
        public Human(string name, int age, string pos, int sal, string num)
        {
            Name = name;
            Age = age;
            Position = pos;
            Salary = sal;
            Number = num;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string Number { get; set; }
    }
}
