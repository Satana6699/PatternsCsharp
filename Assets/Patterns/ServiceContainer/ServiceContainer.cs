using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Infrastructure.Services
{
    public class ServiceContainer
    {
        private static ServiceContainer _instance;
        public static ServiceContainer Container => _instance ??= new ServiceContainer();

        private static Dictionary<Type, IService> _services;

        private ServiceContainer()
        {
            _services = new Dictionary<Type, IService>();
        }

        public TService Register<TService>(TService implementation) where TService : class, IService
        {
            var type = typeof(TService);
            if (_services.ContainsKey(type))
                return Get<TService>();
            _services.Add(type, implementation);
            return Get<TService>();
        }

        public TService Get<TService>() where TService : class, IService
        {
            return _services[typeof(TService)] as TService;
        }
    }
}