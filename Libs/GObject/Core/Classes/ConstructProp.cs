using System;

namespace GObject
{
    public class ConstructProp
    {
        public string Name { get; }
        public Sys.Value Value { get; }

        private ConstructProp(string name, object value)
        {
            Name = name;

            Value = value switch
            {
                int i => new Sys.Value(i),
                Object obj => new Sys.Value(obj.Handle),
                string str => new Sys.Value(str),
                _ => throw new NotSupportedException()
            };
        }
        
        public static ConstructProp With(string name, object value) => new ConstructProp(name, value);
    }
}