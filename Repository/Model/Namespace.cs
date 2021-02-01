using System.Collections.Generic;

namespace Repository.Model
{
    //TODO: Records should not be changeable. All lists will be altered during runtime
    public record Namespace
    {
        // Basic Info
        public string Name { get; set; }
        public string Version { get; set; }
        
        // Aliases
        public List<Alias> Aliases { get; internal set; } = new();
        
        // Symbols
        public List<Callback> Callbacks { get; internal set; } = new();
        public List<Class> Classes { get; internal set; } = new();
        public List<Enumeration> Enumerations { get; internal set; } = new();
        public List<Enumeration> Bitfields { get; internal set; } = new();
        public List<Interface> Interfaces { get; internal set; } = new();
        public List<Record> Records { get; internal set; } = new();
        
        // Miscellaneous
        public List<Method> Functions { get; internal set; } = new();
        
        public string ToCanonicalName() => $"{Name}-{Version}";
        
    }
}
