using System;
using System.Collections.Generic;

using Repository.Analysis;

namespace Generator.Services
{
    public abstract class Service
    {
        // Service resources (add here if relevant to all services)
        public string CurrentNamespace { get; set; }
    }
    
    public class ServiceManager
    {
        private readonly Dictionary<Type, Service> serviceDict = new();

        private readonly TypeDictionaryView typeDict;
        private readonly string nspace;

        public ServiceManager(string nspace)
        {
            this.nspace = nspace;
        }

        public void Add<T>(T service)
            where T: Service
        {
            // Set Service Resources
            service.CurrentNamespace = nspace;
            
            serviceDict.Add(typeof(T), service);
        }

        public T Get<T>()
            where T : Service
        { 
            return (T)serviceDict[typeof(T)]; 
        }
    }
}
