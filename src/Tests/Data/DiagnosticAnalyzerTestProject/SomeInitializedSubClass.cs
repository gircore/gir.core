using System.Diagnostics.CodeAnalysis;
using GObject;

namespace DiagnosticAnalyzerTestProject;

[Subclass<GObject.Object>]
public partial class SomeInitializedSubClass
{
    public string Text { get; set; } //null by default

    [MemberNotNull(nameof(Text))]
    partial void Initialize()
    {
        Text = string.Empty; //ensure that property is not null. No warning is raised.
    }
}
