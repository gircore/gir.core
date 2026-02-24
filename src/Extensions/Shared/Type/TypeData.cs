using System.Collections.Generic;

internal sealed record TypeData(
    string Filename,
    string Namespace,
    bool IsGlobalNamespace,
    TypeProperties Properties,
    Stack<TypeProperties> UpperNestedTypes
);
