namespace GirLoader.Output;

public partial class Constant
{
    private readonly Repository _repository;

    public string Name { get; }
    public string Value { get; }
    public AnyTypeReference AnyTypeReference { get; }
    public bool Introspectable { get; }

    public Constant(Repository repository, string name, AnyTypeReference anyTypeReference, string value, bool introspectable)
    {
        _repository = repository;
        Name = name;
        AnyTypeReference = anyTypeReference;
        Value = value;
        Introspectable = introspectable;
    }
}
