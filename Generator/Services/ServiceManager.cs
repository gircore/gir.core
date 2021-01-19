using System;
using System.Collections.Generic;
using Generator.Analysis;

namespace Generator.Services
{
    public abstract class Service
    {
        // Service resources (add here if relevant to all services)
        public TypeDictionary TypeDict { get; set; }
    }
    
    public class ServiceManager
    {
        private readonly Dictionary<Type, Service> serviceDict = new();

        private readonly TypeDictionary typeDict;

        public ServiceManager(TypeDictionary typeDict)
        {
            this.typeDict = typeDict;
        }

        public void Add<T>(T service)
            where T: Service
        {
            // Set Service Resources
            service.TypeDict = typeDict;
            
            serviceDict.Add(typeof(T), service);
        }

        public T Get<T>()
            where T : Service
        { 
            return (T)serviceDict[typeof(T)]; 
        }
    }
}
