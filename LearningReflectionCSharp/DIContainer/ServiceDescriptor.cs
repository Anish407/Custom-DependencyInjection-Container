public class ServiceDescriptor
{
    public Type Type { get; }
    public Object Implementation { get; }
    public ServiceLifetimes Lifetime { get; }

    public ServiceDescriptor(object implementation, ServiceLifetimes serviceLifetimes)
    {
        Type = implementation.GetType();
        Lifetime = serviceLifetimes;
        Implementation = implementation;

    }
}