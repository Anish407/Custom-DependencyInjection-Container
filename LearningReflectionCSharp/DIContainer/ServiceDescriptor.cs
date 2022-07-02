public class ServiceDescriptor
{
    public Type Type { get; }
    public Type ImplementationType { get; }

    public Object Implementation { get; }
    public ServiceLifetimes Lifetime { get; }

    public ServiceDescriptor(object implementation, ServiceLifetimes serviceLifetimes)
    {
        Type = implementation.GetType();
        Lifetime = serviceLifetimes;
        Implementation = implementation;
    }
    public ServiceDescriptor(Type implementationType, ServiceLifetimes serviceLifetimes, object implementation)
    {
        Lifetime = serviceLifetimes;
        ImplementationType = implementationType;
        Implementation = implementation;
    }
}