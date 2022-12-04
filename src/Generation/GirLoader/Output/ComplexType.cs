namespace GirLoader.Output;

public abstract partial class ComplexType : Type
{
    public Repository Repository { get; }
    public string Name { get; }

    protected ComplexType(Repository repository, string? cType, string name) : base(cType)
    {
        Repository = repository;
        Name = name;
    }
}
