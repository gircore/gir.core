namespace GObject
{
    public interface Property<T> : CanChange<T>
    {
        T Value { get; set; }
    }
}