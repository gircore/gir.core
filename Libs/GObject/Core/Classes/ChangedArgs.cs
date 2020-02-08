using System;

namespace GObject.Core
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