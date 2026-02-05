using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Integration.Tests;

[TestClass, TestCategory("BindingTest")]
public class DiagnosticAnalyzerTest : Test
{
    public TestContext TestContext { get; set; }

    private const string RaiseGirCore1001 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1001
                                            {
                                                public RaiseGirCore1001(string a) { }
                                            }
                                            """;

    private const string RaiseGirCore1002 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1002
                                            {
                                                public RaiseGirCore1002() { }
                                            }
                                            """;

    private const string RaiseGirCore1003 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1003
                                            {
                                                public RaiseGirCore1003(string a) : base(null!) { }
                                            }
                                            """;

    private const string RaiseGirCore1004 = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class RaiseGirCore1004<T>;
                                            """;

    private const string RaiseGirCore1005 = """
                                            public partial class Wrapper<T>
                                            {
                                                [GObject.Subclass<GObject.Object>]
                                                public partial class RaiseGirCore1004Wrapped
                                                {
                                                    public RaiseGirCore1004Wrapped() : this()
                                                    {
                                                    }
                                                }
                                            }
                                            """;

    private const string NotRaiseGirCore1002 = """
                                               [GObject.Subclass<GObject.Object>]
                                               public partial class NotRaiseGirCore1002
                                               {
                                                   static NotRaiseGirCore1002() { }
                                               }
                                               """;

    private const string NotRaiseGirCore1004 = """
                                               [GObject.Subclass<GObject.Object>]
                                               public partial class NotRaiseGirCore1004;
                                               
                                               public class NotRaiseGirCore1004<T> : NotRaiseGirCore1004;
                                               """;

    [TestMethod]
    [DataRow(RaiseGirCore1001, "GirCore1001", true)]
    [DataRow(RaiseGirCore1002, "GirCore1002", true)]
    [DataRow(RaiseGirCore1003, "GirCore1003", true)]
    [DataRow(RaiseGirCore1004, "GirCore1004", true)]
    [DataRow(RaiseGirCore1005, "GirCore1005", true)]
    [DataRow(NotRaiseGirCore1002, "GirCore1002", false)]
    [DataRow(NotRaiseGirCore1004, "GirCore1004", false)]
    [DataRow(RaiseGirCore1005, "GirCore1004", false)]
    [DataRow(RaiseGirCore1004, "GirCore1005", false)]
    public async Task ShouldRaiseExpectedDiagnosticIds(string code, string diagnosticId, bool diagnosticIdExpected)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(ShouldRaiseExpectedDiagnosticIds),
            syntaxTrees: [CSharpSyntaxTree.ParseText(code)],
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
            references: [
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(GObject.Object).Assembly.Location)
            ]
        );

        var driver = CSharpGeneratorDriver.Create(new SourceGenerator.Generator());
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out _, TestContext.CancellationToken);

        var diagnostics = await outputCompilation
            .WithAnalyzers([new SourceAnalyzer.Analyzer()])
            .GetAllDiagnosticsAsync();

        if (diagnosticIdExpected)
            diagnostics.ContainsDiagnostic(diagnosticId);
        else
            diagnostics.ContainsNoDiagnostic(diagnosticId);
    }
    
    private const string GObjectInitallyOwned = """
                                            [GObject.Subclass<GObject.Object>]
                                            public partial class Owned;
                                            """;

    private const string GObjectInitiallyOwnedExpected = """
         #nullable enable
         public unsafe partial class Owned : global::GObject.Object, GObject.GTypeProvider, GObject.InstanceFactory
         {
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<Owned, global::GObject.Object>(&ClassInit, &InstanceInit);
              
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              public static new GObject.Type GetGType() => GType;

              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
              {
                  return new Owned(new global::GObject.Internal.ObjectHandle(handle, ownsHandle));
              }
              
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              public Owned(global::GObject.Internal.ObjectHandle handle) : base(handle) 
              {
                  CompositeTemplateInitialize();
                  Initialize();
              }
              
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              public Owned(params GObject.ConstructArgument[] constructArguments) : this(global::GObject.Internal.ObjectHandle.For<Owned>(constructArguments)) 
              {
                  //Do not call 'Initialize();' here. It will be called by 'this(...)'.
              }
              
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              [System.Runtime.InteropServices.UnmanagedCallersOnly]
              private static void ClassInit(System.IntPtr cls, System.IntPtr clsData)
              {
                  var classDefinition = (GObject.Internal.ObjectClassUnmanaged*) cls;
                  classDefinition->Dispose = &Dispose;
             
                  CompositeTemplateClassInit(cls, clsData);
              }
             
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              [System.Runtime.InteropServices.UnmanagedCallersOnly]
              private static void InstanceInit(System.IntPtr instance, System.IntPtr cls)
              {
                  CompositeTemplateInstanceInit(instance, cls);
                  
                  //After C initialization create a C# instance to ensure
                  //dotnet initialization code runs. Instance is added to
                  //instance cache automatically.
                  _ = new Owned(new global::GObject.Internal.ObjectHandle(instance, false));
              }
             
              [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
              [System.Runtime.InteropServices.UnmanagedCallersOnly]
              private static void Dispose(System.IntPtr instance)
              {
                  CompositeTemplateDispose(instance);
             
                  //Call into parents dispose method
                  var parentType = GObject.Internal.Functions.TypeParent(GType);
                  var parentTypeClass = (GObject.Internal.ObjectClassUnmanaged*) GObject.Internal.TypeClass.Peek(parentType).DangerousGetHandle();
                  parentTypeClass->Dispose(instance);
              }
              
              /// <summary>
              /// This method is called by all generated constructors.
              /// Implement this partial method to initialize all members.
              /// Decorating this method with "MemberNotNullAttribute" for
              /// the appropriate members can remove nullable warnings.
              /// </summary>
              partial void Initialize();
              
              /// <summary>
              /// This method is called during GObject class initialization. It is
              /// meant to set up Gtk composite templates.
              /// </summary>
              /// <remarks>
              /// The content of this method can be generated by Gtk-4.0.Integration 
              /// nuget package if a class is decorated with the [Gtk.Template] attribute.
              /// </remarks>
              static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsdata);
             
              /// <summary>
              /// This method is called during GObject instance initialization. It is
              /// meant to set up Gtk composite templates.
              /// </summary>
              /// <remarks>
              /// The content of this method can be generated by Gtk-4.0.Integration 
              /// nuget package if a class is decorated with the [Gtk.Template] attribute.
              /// </remarks>
              static partial void CompositeTemplateInstanceInit(System.IntPtr instance, System.IntPtr cls);
             
              /// <summary>
              /// This method is called during GObject instance disposal. It is
              /// meant to dispose Gtk composite templates.
              /// </summary>
              /// <remarks>
              /// The content of this method can be generated by Gtk-4.0.Integration 
              /// nuget package if a class is decorated with the [TGtk.emplate] attribute.
              /// </remarks>
              static partial void CompositeTemplateDispose(System.IntPtr instance);
              
              /// <summary>
              /// This method is called during the dotnet constructor call. It is
              /// meant to initialize composite templates.
              /// </summary>
              /// <remarks>
              /// The content of this method can be generated by Gtk-4.0.Integration 
              /// nuget package if a member is decorated with the [Gtk.Connect] attribute.
              /// </remarks>
              partial void CompositeTemplateInitialize();
         }

         """;
            
    private const string GObjectInitallyUnowned = """
                                                [GObject.Subclass<GObject.InitiallyUnowned>]
                                                public partial class Unowned;
                                                """;

    private const string GObjectInitiallyUnownedExpected = """
        #nullable enable
        public unsafe partial class Unowned : global::GObject.InitiallyUnowned, GObject.GTypeProvider, GObject.InstanceFactory
        {
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<Unowned, global::GObject.InitiallyUnowned>(&ClassInit, &InstanceInit);
             
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             public static new GObject.Type GetGType() => GType;

             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
             {
                 return new Unowned(new global::GObject.Internal.InitiallyUnownedHandle(handle, ownsHandle));
             }
             
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             public Unowned(global::GObject.Internal.InitiallyUnownedHandle handle) : base(handle) 
             {
                 CompositeTemplateInitialize();
                 Initialize();
             }
             
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             public Unowned(params GObject.ConstructArgument[] constructArguments) : this(global::GObject.Internal.InitiallyUnownedHandle.For<Unowned>(constructArguments)) 
             {
                 //Do not call 'Initialize();' here. It will be called by 'this(...)'.
             }
             
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             [System.Runtime.InteropServices.UnmanagedCallersOnly]
             private static void ClassInit(System.IntPtr cls, System.IntPtr clsData)
             {
                 var classDefinition = (GObject.Internal.ObjectClassUnmanaged*) cls;
                 classDefinition->Dispose = &Dispose;
            
                 CompositeTemplateClassInit(cls, clsData);
             }
            
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             [System.Runtime.InteropServices.UnmanagedCallersOnly]
             private static void InstanceInit(System.IntPtr instance, System.IntPtr cls)
             {
                 CompositeTemplateInstanceInit(instance, cls);
                 
                 //After C initialization create a C# instance to ensure
                 //dotnet initialization code runs. Instance is added to
                 //instance cache automatically.
                 _ = new Unowned(new global::GObject.Internal.InitiallyUnownedHandle(instance, true));
             }
            
             [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
             [System.Runtime.InteropServices.UnmanagedCallersOnly]
             private static void Dispose(System.IntPtr instance)
             {
                 CompositeTemplateDispose(instance);
            
                 //Call into parents dispose method
                 var parentType = GObject.Internal.Functions.TypeParent(GType);
                 var parentTypeClass = (GObject.Internal.ObjectClassUnmanaged*) GObject.Internal.TypeClass.Peek(parentType).DangerousGetHandle();
                 parentTypeClass->Dispose(instance);
             }
             
             /// <summary>
             /// This method is called by all generated constructors.
             /// Implement this partial method to initialize all members.
             /// Decorating this method with "MemberNotNullAttribute" for
             /// the appropriate members can remove nullable warnings.
             /// </summary>
             partial void Initialize();
             
             /// <summary>
             /// This method is called during GObject class initialization. It is
             /// meant to set up Gtk composite templates.
             /// </summary>
             /// <remarks>
             /// The content of this method can be generated by Gtk-4.0.Integration 
             /// nuget package if a class is decorated with the [Gtk.Template] attribute.
             /// </remarks>
             static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsdata);
            
             /// <summary>
             /// This method is called during GObject instance initialization. It is
             /// meant to set up Gtk composite templates.
             /// </summary>
             /// <remarks>
             /// The content of this method can be generated by Gtk-4.0.Integration 
             /// nuget package if a class is decorated with the [Gtk.Template] attribute.
             /// </remarks>
             static partial void CompositeTemplateInstanceInit(System.IntPtr instance, System.IntPtr cls);
            
             /// <summary>
             /// This method is called during GObject instance disposal. It is
             /// meant to dispose Gtk composite templates.
             /// </summary>
             /// <remarks>
             /// The content of this method can be generated by Gtk-4.0.Integration 
             /// nuget package if a class is decorated with the [TGtk.emplate] attribute.
             /// </remarks>
             static partial void CompositeTemplateDispose(System.IntPtr instance);
             
             /// <summary>
             /// This method is called during the dotnet constructor call. It is
             /// meant to initialize composite templates.
             /// </summary>
             /// <remarks>
             /// The content of this method can be generated by Gtk-4.0.Integration 
             /// nuget package if a member is decorated with the [Gtk.Connect] attribute.
             /// </remarks>
             partial void CompositeTemplateInitialize();
        }

        """;
    
        [TestMethod]
    [DataRow(GObjectInitallyOwned, "Owned.Subclass.g.cs", GObjectInitiallyOwnedExpected)]
    [DataRow(GObjectInitallyUnowned, "Unowned.Subclass.g.cs", GObjectInitiallyUnownedExpected)]
    public void ShouldGenerateCorrectCode(string code, string file, string expeced)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(ShouldGenerateCorrectCode),
            syntaxTrees: [CSharpSyntaxTree.ParseText(code)],
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
            references: [
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(GObject.Object).Assembly.Location)
            ]
        );

        var driver = CSharpGeneratorDriver.Create(new SourceGenerator.Generator());
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out _, TestContext.CancellationToken);

        outputCompilation
            .SyntaxTrees
            .First(x => x.FilePath.EndsWith(file))
            .ToString()
            .Should().Be(expeced);
    }
}
