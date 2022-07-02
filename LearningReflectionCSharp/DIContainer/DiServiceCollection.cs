using LearningReflectionCSharp.NewFolder;

public class DiServiceCollection
{
    private Dictionary<string, ICollection<ServiceDescriptor>> serviceDescriptors = new Dictionary<string, ICollection<ServiceDescriptor>>();


    public void AddTransient<T>()
    {
        AddRegistration<T>(ServiceLifetimes.Transient);
    }

    public void AddSingleton<T>()
    {
        AddRegistration<T>(ServiceLifetimes.Singleton);
    }

    public void AddSingleton<T>(T service)
    {
        AddRegistration<T>(ServiceLifetimes.Singleton, service);
    }

    T CreateInstance<T>()
    {
        return Activator.CreateInstance<T>();
    }

    private void AddRegistration<T>(ServiceLifetimes lifetime, object implementation = null)
    {
        string serviceName = typeof(T).Name;
        InsertDescriptorIfNotExists<T>(serviceName, lifetime, implementation);
    }

    private void AddDescriptor<T>(string serviceName, ServiceLifetimes lifetime, object implementation)
    {
        serviceDescriptors.Add(serviceName, new List<ServiceDescriptor> { new ServiceDescriptor(implementation, lifetime) });
    }

    private void InsertDescriptorIfNotExists<T>(string serviceName, ServiceLifetimes lifetime, object implementation = null)
    {
        implementation = implementation ?? CreateInstance<T>();
        if (serviceDescriptors.TryGetValue(serviceName, out ICollection<ServiceDescriptor> descriptors))
            descriptors.Add(new ServiceDescriptor(implementation, lifetime));
        else
            AddDescriptor<T>(serviceName, lifetime, implementation);
    }

    public DIContainer Buid()
    {
        return new DIContainer(serviceDescriptors);
    }
}
