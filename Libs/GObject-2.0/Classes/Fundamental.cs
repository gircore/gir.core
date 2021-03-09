using System;

namespace GObject
{
    public abstract class Fundamental<T> where T : Fundamental<T>, new()
    {
        internal protected IntPtr Handle { get; init; }

        public static T From(IntPtr ptr)
        {
            //TODO: Fundamental types must be properly refed / disposed
            //They define separate ref / unref functions in the gir
            return new T() { Handle = ptr };
        }
    }
}
