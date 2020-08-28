namespace GObject
{
    public interface IProperty<T> : CanChange<T>
    {
        T Value { get; set; }
    }
}