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

        if (descriptor.ImplementationType != null)
        {
           return (T) descriptor.Implementation ?? (T)Activator.CreateInstance(descriptor.ImplementationType);
        }

        if (descriptor.Lifetime == ServiceLifetimes.Transient)
        {
            return (T)Activator.CreateInstance(type);
        }

        if (descriptor.Lifetime == ServiceLifetimes.Singleton)
        {
            return (T)descriptor.Implementation;
        }

        return (T)descriptor.Implementation;
    }
}
