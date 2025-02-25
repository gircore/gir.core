using GObject;

namespace DiagnosticAnalyzerTestProject;

public partial class SomeClassContainingNestedSubSubSubClass
{
    [Subclass<SomeSubSubClass>]
    public partial class SomeNestedSubSubSubClass;
}
