using GObject;

namespace DiagnosticAnalyzerTestProject;

[Subclass<GObject.Object>]
public partial class SomeInitializedSubClassWithUserConstructor
{
    private readonly bool _someOption;

    public int InitializedCount { get; private set; }

    public SomeInitializedSubClassWithUserConstructor(bool someOption) : this()
    {
        _someOption = someOption;
    }

    partial void Initialize()
    {
        InitializedCount++; //Count the number of times we are initialized. Should be one.
    }
}
