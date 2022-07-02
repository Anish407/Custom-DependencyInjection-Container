

using LearningReflectionCSharp.DIContainer;
using LearningReflectionCSharp.NewFolder;

var services = new DiServiceCollection();


//services.AddTransient<RandomGuidGenerator>();
services.AddTransient<IDemoService, DemoService>();

//services.AddSingleton<IDemoService, DemoService>();
//services.AddSingleton<RandomGuidGenerator>();
//services.AddSingleton(new RandomGuidGenerator());

var container = services.Buid();

//var service1 = container.GetService<RandomGuidGenerator>();
//var service2 = container.GetService<RandomGuidGenerator>();

//Console.WriteLine(service1.NewGuid);
//Console.WriteLine(service2.NewGuid);

var service1= container.GetService<IDemoService>();
service1.Name = "Test";
var service2= container.GetService<IDemoService>();

service1.DisplayName();
service2.DisplayName();


Console.ReadKey();