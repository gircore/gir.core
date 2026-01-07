namespace GObject.Integration.SourceGenerator;

internal sealed record SubclassData(
    TypeData TypeData,
    string Parent,
    string ParentHandle,
    bool InitiallyUnowned
);
