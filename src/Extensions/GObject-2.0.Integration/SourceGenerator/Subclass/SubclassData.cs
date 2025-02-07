using System.Collections.Generic;

namespace GObject.Integration.SourceGenerator;

internal sealed record SubclassData(
    string Name,
    string NameGenericArguments,
    string Parent,
    string ParentHandle,
    string Namespace,
    string Accessibility,
    string FileName,
    Stack<TypeData> UpperNestedClasses
);
