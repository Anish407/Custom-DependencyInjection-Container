public class DIContainer
{
    public DIContainer(IList<ServiceDescriptor> serviceDescriptors)
    {
        ServiceDescriptors = serviceDescriptors;
    }

    public IList<ServiceDescriptor> ServiceDescriptors { get; }

    public T GetService<T>()
    {
        Type type = typeof(T);
        var descriptor = ServiceDescriptors.SingleOrDefault(x => x.Type == type);

        if (descriptor == null) throw new Exception($"{type.Name} is not registered");

        if(descriptor.Lifetime == ServiceLifetimes.Transient)
        {
           return (T)Activator.CreateInstance(type);
        }

        return (T)descriptor.Implementation;
    }
}
