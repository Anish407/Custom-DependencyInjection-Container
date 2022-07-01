// See https://aka.ms/new-console-template for more information
using ReflectionSample.PersonData;
using System.Reflection;

var currentAssembly= Assembly.GetExecutingAssembly();
var typesFromCurrentAssembly=currentAssembly.GetTypes();

DisplyTypes(typesFromCurrentAssembly);

var OnetypeFromCurrentAssembly= currentAssembly.GetType("ReflectionSample.Employee");
var person= currentAssembly.GetType("ReflectionSample.PersonData.Person");

var typeFromClassName = currentAssembly.GetType($"{typeof(Person).Namespace}.{nameof(Person)}");


var externalAssembly = Assembly.Load("System.Text.Json");
DisplyTypes(externalAssembly.GetTypes());
Console.WriteLine(typeFromClassName.Name);
Console.ReadLine();

static void DisplyTypes(Type[] typesFromCurrentAssembly)
{
    Console.WriteLine("...............................");
    foreach (var type in typesFromCurrentAssembly)
    {
        Console.WriteLine(type.Name);
    }
    Console.WriteLine("...............................");
}