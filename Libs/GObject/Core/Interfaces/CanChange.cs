using System;

namespace GObject.Core
{
    public interface CanChange<T>
    {
        event EventHandler<ChangedArgs<T>> Changed;
    }
}