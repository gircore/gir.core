using System.Diagnostics.CodeAnalysis;
using GObject;

namespace DiagnosticAnalyzerTestProject;

[Subclass<GObject.Object>]
public partial class SomeInitializedSubClass
{
    public int InitializedCount { get; private set; }

    public string Text { get; set; } //null by default

    [MemberNotNull(nameof(Text))]
    partial void Initialize()
    {
        InitializedCount++; //Count the number of times we are initialized. Should be one.
        Text = string.Empty; //ensure that property is not null. No warning is raised.
    }
}
