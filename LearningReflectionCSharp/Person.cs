﻿namespace ReflectionSample.PersonData
{
    public class Person : ITalk
    {
        public string Name { get; set; }
        public int age;
        private string _aPrivateField = "initial private field value";

        public Person()
        {
            Console.WriteLine("A person is being created.");
        }

        public Person(string name)
        {
            Console.WriteLine($"A person with name {name} is being created.");
            Name = name;
        }

        private Person(string name, int age)
        {
            Console.WriteLine($"A person with name {name} and age {age} " +
                $"is being created using a private constructor.");
            Name = name;
            this.age = age;
        }

        public void Talk(string sentence)
        {
            // talk...
            Console.WriteLine($"Talking...: {sentence}");
        }

        protected void Yell(string sentence)
        {
            // yell...
            Console.WriteLine($"YELLING! {sentence}");
        }

        public override string ToString()
        {
            return $"{Name} {age} {_aPrivateField}";
        }
    }

}