using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningReflectionCSharp.DIContainer
{
    public interface IDemoService
    {
        public string Name { get; set; }

        public void DisplayName();
    }

    public class DemoService : IDemoService
    {
        public string Name { get; set; }

        public void DisplayName()
        {
            Console.WriteLine($"My Name is : {Name}");
        }
    }
}
