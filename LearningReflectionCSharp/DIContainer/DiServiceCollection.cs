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
        object implementation = Activator.CreateInstance<TImplementation>();
        AddRegistration<TService, TImplementation>(ServiceLifetimes.Singleton, implementation);
    }

    public void AddSingleton<T>()
    {
        object implementation = Activator.CreateInstance<T>();
        AddRegistration<T>(ServiceLifetimes.Singleton, implementation);
    }

    public void AddSingleton<T>(T service)
    {
        object implementation = Activator.CreateInstance<T>();
        AddRegistration<T>(ServiceLifetimes.Singleton, service);
    }

    T CreateInstance<T>()
    {
        return Activator.CreateInstance<T>();
    }

    public DIContainer Buid()
    {
        return new DIContainer(serviceDescriptors);
    }

    private void AddRegistration<T>(ServiceLifetimes lifetime, object implementation = null)
    {
        string serviceName = typeof(T).Name;
        InsertDescriptorIfNotExists<T>(serviceName, lifetime, implementation: implementation);
    }

    private void AddRegistration<T, TImplementation>(ServiceLifetimes lifetime, object implementation = null)
    {
        string serviceName = typeof(T).Name;
        InsertDescriptorIfNotExists<T, TImplementation>(serviceName, lifetime, implementation);
    }

    private void AddDescriptor<T>(string serviceName, ServiceLifetimes lifetime, object implementation)
    {
        serviceDescriptors.Add(serviceName, new List<ServiceDescriptor> { new ServiceDescriptor(implementation, lifetime) });
    }

    private void AddDescriptor<T, TImplementation>(string serviceName,
        ServiceLifetimes lifetime,
        Type ImplementationType,
        object implementation = null)
    {
        serviceDescriptors.Add(serviceName, new List<ServiceDescriptor> { new ServiceDescriptor(ImplementationType, lifetime, implementation) });
    }


    //<TService, TImplementation>
    private void InsertDescriptorIfNotExists<T, TImplementation>(string serviceName, ServiceLifetimes lifetime,
        object implementation = null
        )
    {
        ICollection<ServiceDescriptor> descriptors;
        if (CheckIfServiceRegistrationExists(serviceName, out descriptors))
        {
            descriptors.Add(new ServiceDescriptor(typeof(TImplementation), lifetime, implementation));
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
        if (CheckIfServiceRegistrationExists(serviceName, out descriptors))
        {
            descriptors.Add(new ServiceDescriptor(implementation, lifetime));
        }
        else
            AddDescriptor<T>(serviceName, lifetime, implementation);
    }

    private bool CheckIfServiceRegistrationExists(string serviceName, out ICollection<ServiceDescriptor> descriptors)
    {
        return serviceDescriptors.TryGetValue(serviceName, out descriptors);
    }
}
