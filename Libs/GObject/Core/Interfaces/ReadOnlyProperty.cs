namespace GObject.Core
{
    public interface ReadOnlyProperty<T> : CanChange<T>
    {
        T Value { get; }
    }
}