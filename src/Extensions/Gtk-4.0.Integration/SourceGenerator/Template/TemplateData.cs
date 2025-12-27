using System.Collections.Generic;

namespace Gtk.Integration.SourceGenerator;

internal sealed record TemplateData(
    string NameGenericArguments,
    string Namespace,
    bool IsGlobalNamespace,
    string Accessibility,
    string FileName,
    string RessourceName,
    Stack<TypeData> UpperNestedClasses
);
