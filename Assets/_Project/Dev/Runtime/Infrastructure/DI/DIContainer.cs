using System;
using System.Collections.Generic;
using _Project.Dev.Runtime.Utilities.ConfigsManagment;
using Unity.VisualScripting;

namespace _Project.Dev.Runtime.Infrastructure.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();
            
        private readonly List<Type> _requests = new();
        
        private readonly DIContainer _parent;
        
        public DIContainer() : this(null)
        {
        }

        public DIContainer(DIContainer parent) => _parent = parent;

        public void RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if(IsAlreadyRegistered<T>())
                throw new InvalidOperationException($"Already registered type {typeof(T).Name}");   
            
            
            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);
        }

        public bool IsAlreadyRegistered<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsAlreadyRegistered<T>();
            
            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"The requested type {typeof(T)} has already been registered.");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstanceFrom(this);
                
                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"No registration for type {typeof(T)}");
        }
    }
}