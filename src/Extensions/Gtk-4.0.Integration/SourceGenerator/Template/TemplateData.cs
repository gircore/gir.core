using System.Collections.Generic;

namespace Gtk.Integration.SourceGenerator;

internal sealed record TemplateData(
    TypeData TypeData,
    string ResourceName,
    string Loader,
    HashSet<TemplateData.Connect> Connections
)
{
    internal sealed record Connect(
        string ObjectId,
        string Type,
        string MemberName
    );
}
