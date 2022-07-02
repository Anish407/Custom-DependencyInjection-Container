public class DIContainer
{
    public DIContainer(Dictionary<string, ICollection<ServiceDescriptor>> serviceDescriptors)
    {
        ServiceDescriptors = serviceDescriptors;
    }

    public Dictionary<string, ICollection<ServiceDescriptor>> ServiceDescriptors { get; }

    public T GetService<T>()
    {
        Type type = typeof(T);
        var descriptor = ServiceDescriptors[type.Name]?.FirstOrDefault();

        if (descriptor == null) throw new Exception($"{type.Name} is not registered");

        if(descriptor.Lifetime == ServiceLifetimes.Transient)
        {
           return (T)Activator.CreateInstance(type);
        }

        return (T)descriptor.Implementation;
    }
}
