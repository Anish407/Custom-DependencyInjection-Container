using LearningReflectionCSharp.NewFolder;

public class DiServiceCollection
{
    private IList<ServiceDescriptor> serviceDescriptors = new List<ServiceDescriptor>();
    public DiServiceCollection()
    {
    }

    public void AddTransient<T>()
    {
        serviceDescriptors.Add(new ServiceDescriptor(CreateInstance<T>(), ServiceLifetimes.Transient));
    }

    internal void AddSingleton<T>()
    {
        serviceDescriptors.Add(new ServiceDescriptor(CreateInstance<T>(), ServiceLifetimes.Singleton));
    }

    public void AddSingleton<T>(T service)
    {
        serviceDescriptors.Add(new ServiceDescriptor(service, ServiceLifetimes.Singleton));
    }

    T CreateInstance<T>()
    {
        return Activator.CreateInstance<T>();
    }

    public DIContainer Buid()
    {
        return new DIContainer(serviceDescriptors);
    }
}

public enum ServiceLifetimes
{
    Singleton = 1,
    Transient
}

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