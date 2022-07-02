public class ServiceDescriptor
{
    public Type Type { get; }
    public Type ImplementationType { get; }

    public Object Implementation { get; set; }
    public ServiceLifetimes Lifetime { get; }

    public ServiceDescriptor(Type serviceType, ServiceLifetimes serviceLifetimes)
    {
        Type = serviceType;
        Lifetime = serviceLifetimes;
    }

    public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetimes serviceLifetimes)
    {
        Type = serviceType;
        Lifetime = serviceLifetimes;
        ImplementationType = implementationType;
    }
}