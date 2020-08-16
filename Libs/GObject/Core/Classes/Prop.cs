using System;

namespace GObject
{
    public class Prop
    {
        public string Name { get; }
        public Sys.Value Value { get; }

        private Prop(string name, object value)
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
        
        public static Prop With(string name, object value) => new Prop(name, value);
    }
}