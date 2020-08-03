using System;

namespace GObject
{
    public interface CanChange<T>
    {
        event EventHandler<ChangedArgs<T>> Changed;
    }
}