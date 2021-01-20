using System;
using System.Collections.Generic;
using Generator.Analysis;

namespace Generator.Services
{
    public abstract class Service
    {
        // Service resources (add here if relevant to all services)
        public TypeDictionaryView TypeDict { get; set; }
        public string CurrentNamespace { get; set; }
    }
    
    public class ServiceManager
    {
        private readonly Dictionary<Type, Service> serviceDict = new();

        private readonly TypeDictionaryView typeDict;
        private readonly string nspace;

        public ServiceManager(TypeDictionaryView typeDict, string nspace)
        {
            this.typeDict = typeDict;
            this.nspace = nspace;
        }

        public void Add<T>(T service)
            where T: Service
        {
            // Set Service Resources
            service.TypeDict = typeDict;
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
