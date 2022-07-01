using ReflectionSample.PersonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample
{
    public interface ITalk
    {
        void Talk(string sentence);
    }

    public class EmployeeMarkerAttribute : Attribute
    {
    }

    [EmployeeMarker]
    public class Employee : Person
    {
        public string Company { get; set; }
    }

    public class Alien : ITalk
    {
        public void Talk(string sentence)
        {
            // talk...
            Console.WriteLine($"Alien talking...: {sentence}");
        }
    }

}