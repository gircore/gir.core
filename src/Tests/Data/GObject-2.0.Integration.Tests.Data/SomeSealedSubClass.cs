namespace DiagnosticAnalyzerTestProject;

//This class does not have a test.
//Its presence should not trigger any error from a warning (CS0628).

[GObject.Subclass<GObject.Object>]
public sealed partial class SomeSealedSubClass;
