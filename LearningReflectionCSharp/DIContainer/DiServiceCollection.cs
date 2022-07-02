using LearningReflectionCSharp.NewFolder;

public class DiServiceCollection
{
    private Dictionary<string, ICollection<ServiceDescriptor>> serviceDescriptors = new Dictionary<string, ICollection<ServiceDescriptor>>();


    public void AddTransient<T>()
    {
        AddRegistration<T>(ServiceLifetimes.Transient);
    }

    public void AddTransient<TService, TImplementation>() where TImplementation : TService
    {
        AddRegistration<TService, TImplementation>(ServiceLifetimes.Transient);
    }


    public void AddSingleton<TService, TImplementation>() where TImplementation : TService
    {
        AddRegistration<TService, TImplementation>(ServiceLifetimes.Singleton);
    }

    public void AddSingleton<T>()
    {
        AddRegistration<T>(ServiceLifetimes.Singleton);
    }

    public void AddSingleton<T>(T service)
    {
        AddRegistration<T>(ServiceLifetimes.Singleton);
    }

    public DIContainer Buid()
    {
        return new DIContainer(serviceDescriptors);
    }

    private void AddRegistration<T>(ServiceLifetimes lifetime)
    {
        // add save type and create the instance later inside  GetService()
        string serviceName = typeof(T).Name;
        InsertDescriptorIfNotExists<T>(serviceName, lifetime);
    }

    // Use when we register an interface and its implementation
    private void AddRegistration<T, TImplementation>(ServiceLifetimes lifetime)
    {
        // if we register with an implementation, then save the implementation type in the service descriptor
        string serviceName = typeof(T).Name;
        InsertDescriptorIfNotExists<T, TImplementation>(serviceName, lifetime);
    }

    private void AddDescriptor<T>(string serviceName, ServiceLifetimes lifetime)
    {
        serviceDescriptors.Add(serviceName, new List<ServiceDescriptor> { new ServiceDescriptor(typeof(T), lifetime) });
    }

    private void AddDescriptor<T, TImplementation>(string serviceName,
        ServiceLifetimes lifetime,
        Type ImplementationType,
        object implementation = null)
    {
        serviceDescriptors.Add(serviceName, new List<ServiceDescriptor> { new ServiceDescriptor(typeof(T), ImplementationType, lifetime) });
    }


    //<TService, TImplementation>
    private void InsertDescriptorIfNotExists<T, TImplementation>(string serviceName, ServiceLifetimes lifetime,
        object implementation = null
        )
    {
        ICollection<ServiceDescriptor> descriptors;
        if (CheckIfServiceRegistrationExists(serviceName, out descriptors))
        {
            descriptors.Add(new ServiceDescriptor(typeof(TImplementation), lifetime));
        }
        else
            AddDescriptor<T, TImplementation>(serviceName, lifetime, typeof(TImplementation), implementation);

    }

    //<TService>
    private void InsertDescriptorIfNotExists<T>(string serviceName, ServiceLifetimes lifetime,
       object implementation = null
       )
    {
        // implementation = implementation ?? CreateInstance<T>();

        ICollection<ServiceDescriptor> descriptors;

        // Check if service has been registered, if yes then add it to its descriptors
        if (CheckIfServiceRegistrationExists(serviceName, out descriptors))
        {
            descriptors.Add(new ServiceDescriptor(typeof(T), lifetime));
        }
        else
            AddDescriptor<T>(serviceName, lifetime);
    }

    private bool CheckIfServiceRegistrationExists(string serviceName, out ICollection<ServiceDescriptor> descriptors)
    {
        return serviceDescriptors.TryGetValue(serviceName, out descriptors);
    }
}
