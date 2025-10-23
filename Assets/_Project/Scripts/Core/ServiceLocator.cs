using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TOW.Core
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            var type = typeof(T);
            if (!services.ContainsKey(type))
                services.Add(type, service);
        }

        public static T Get<T>()
        {
            var type = typeof(T);
            if (services.TryGetValue(type, out var service))
                return (T)service;

            throw new Exception($"Service of type {type} not registered.");
        }
    }
}