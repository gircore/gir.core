namespace GObject
{
    public interface ReadOnlyProperty<T> : CanChange<T>
    {
        T Value { get; }
    }
}