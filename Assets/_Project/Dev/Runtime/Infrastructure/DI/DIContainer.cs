using System;
using System.Collections.Generic;

namespace _Project.Dev.Runtime.Infrastructure.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        public void RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);
        }

        public T Resolve<T>()      
        {
            if (_container.TryGetValue(typeof(T), out Registration registration))
                return (T)registration.CreateInstanceFrom(this);

            throw new InvalidOperationException($"No registration for type {typeof(T)}");
        }
    }
}