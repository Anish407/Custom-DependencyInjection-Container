

using LearningReflectionCSharp.NewFolder;

var services = new DiServiceCollection();
//services.AddSingleton<RandomGuidGenerator>();
//services.AddSingleton(new RandomGuidGenerator());

services.AddTransient<RandomGuidGenerator>();

var container = services.Buid();

var service1 = container.GetService<RandomGuidGenerator>();
var service2 = container.GetService<RandomGuidGenerator>();

Console.WriteLine(service1.NewGuid);
Console.WriteLine(service2.NewGuid);

Console.ReadKey();