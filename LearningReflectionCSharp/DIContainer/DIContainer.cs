public class DIContainer
{
    public DIContainer(Dictionary<string, ICollection<ServiceDescriptor>> serviceDescriptors)
    {
        ServiceDescriptors = serviceDescriptors;
    }

    public Dictionary<string, ICollection<ServiceDescriptor>> ServiceDescriptors { get; }

    public object GetService(Type type)
    {
        if(!ServiceDescriptors.TryGetValue(type.Name, out var serviceDescriptors))
        {
            throw new Exception($"Service {type.Name} has not been registered..");
        }

        var descriptor = serviceDescriptors.FirstOrDefault();

        if (descriptor == null) throw new Exception($"{type.Name} is not registered");

        //if (descriptor.Implementation != null)
        //{
        //    return descriptor.Implementation;
        //}

        Type implementationType;
        // if we have registered an interface like <Tservice,TImplementation> then gets its implementation type
        // from the service descriptor
        if (type.IsInterface)
        {
            implementationType = descriptor.ImplementationType;
        }
        else
        {
            if (type.IsInterface) throw new Exception($"Cannot Instantiate interfaces {type.IsInterface}");
            implementationType = type;
        }

        var constructorInfo = implementationType.GetConstructors().First();

        object implementation = null;

        var parameters = constructorInfo.GetParameters().Select(x =>
        // recursive call to resolve nested dependencies
        GetService(x.ParameterType)
        ).ToArray();
        implementation = Activator.CreateInstance(implementationType, parameters);

        if (descriptor.Lifetime == ServiceLifetimes.Singleton)
        {
            if (descriptor.Implementation == null)
            {
                descriptor.Implementation = implementation;
            }
            return descriptor.Implementation;
        }

        return implementation;
    }

    public T GetService<T>()
    {
        try
        {
            Type type = typeof(T);

            return (T)GetService(type);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }

    }
}
