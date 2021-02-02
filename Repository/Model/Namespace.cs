using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    public class Namespace
    {
        #region Properties
        
        // Basic Info
        public string Name { get; }
        public string Version { get; }
        
        // Aliases
        private readonly List<Alias> _aliases = new();
        public IEnumerable<Alias> Aliases => _aliases;
        
        // Symbols
        private readonly List<Callback> _callbacks = new();
        public IEnumerable<Callback> Callbacks => _callbacks;

        private readonly List<Class> _classes = new();
        public IEnumerable<Class> Classes => _classes;

        private readonly List<Enumeration> _enumerations = new();
        public IEnumerable<Enumeration> Enumerations => _enumerations;

        private readonly List<Enumeration> _bitfields = new();
        public IEnumerable<Enumeration> Bitfields => _bitfields;

        private readonly List<Interface> _interfaces = new();
        public IEnumerable<Interface> Interfaces => _interfaces;

        private readonly List<Record> _records = new();
        public IEnumerable<Record> Records => _records;
        
        // Miscellaneous
        private List<Method> _functions = new();
        public IEnumerable<Method> Functions => _functions;

        #endregion
        
        public Namespace(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public void AddAlias(Alias alias)
            => _aliases.Add(alias);

        public void AddCallback(Callback callback)
            => _callbacks.Add(callback);

        public void AddClass(Class @class)
            => _classes.Add(@class);

        public void AddEnumeration(Enumeration enumeration)
            => _enumerations.Add(enumeration);

        public void AddBitfield(Enumeration enumeration)
            => _bitfields.Add(enumeration);

        public void AddInterface(Interface @interface)
            => _interfaces.Add(@interface);

        public void AddRecord(Record @record)
            => _records.Add(@record);

        public void AddFunction(Method method)
            => _functions.Add(method);
        
        public string ToCanonicalName() => $"{Name}-{Version}";
        
    }
}
