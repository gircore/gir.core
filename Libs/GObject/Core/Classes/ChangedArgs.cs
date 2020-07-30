using System;

namespace GObject
{
    public class ChangedArgs<T> : EventArgs
    {
        public T Value { get; }

        public ChangedArgs(T value)
        {
            Value = value;
        }
    }
}