namespace Gir
{
    public interface IType
    {
        GType? Type { get; set; }

        GArray? Array { get; set; }
    }
}