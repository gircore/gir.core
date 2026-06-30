using GObject;

namespace DiagnosticAnalyzerTestProject;

[Subclass<GObject.Object>]
public abstract partial class SomeAbstractSubClass
{
    public int InitializedCount { get; private set; }

    partial void Initialize()
    {
        InitializedCount++;
    }
}

[Subclass<SomeAbstractSubClass>]
public partial class SomeConcreteSubClass;
