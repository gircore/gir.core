namespace GObject.Integration.SourceGenerator;

internal sealed record SubclassData(
    TypeData TypeData,
    string? QualifiedName,
    string Parent,
    string ParentHandle,
    bool IsInitiallyUnowned
);
