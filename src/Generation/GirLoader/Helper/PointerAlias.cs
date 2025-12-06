namespace GirLoader;

public class PointerAlias : Input.Alias
{
    public PointerAlias(string name, string type)
    {
        Name = name;
        Type = type;
        For = new Input.Type { Name = "any", CType = "any" };
    }
}
