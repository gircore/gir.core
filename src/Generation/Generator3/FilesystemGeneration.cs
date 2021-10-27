using System.Collections.Generic;
using GirModel;
using Generator3.Publication.Filesystem;

public static class FilesystemGeneration
{
    public static void Generate(this IEnumerable<Enumeration> enumerations, string project)
    {
        var generator = new Generator3.Generation.Enumeration.Generator(
            renderer: new Generator3.Renderer.EnumerationUnit(),
            publisher: new EnumFilePublisher()
        );
        
        foreach(var enumeration in enumerations)
            generator.Generate(project, enumeration);
    }
    
    public static void Generate(this IEnumerable<Bitfield> bitfields, string project)
    {
        var generator = new Generator3.Generation.Bitfield.Generator(
            renderer: new Generator3.Renderer.BitfieldUnit(),
            publisher: new EnumFilePublisher()
        );
        
        foreach(var bitfield in bitfields)
            generator.Generate(project, bitfield);
    }
    
    public static void Generate(this IEnumerable<Record> records, string project)
    {
        var nativeFunctionsGenerator = new Generator3.Generation.Record.NativeFunctionsGenerator(
            renderer: new Generator3.Renderer.NativeRecordFunctionsUnit(),
            publisher: new NativeRecordFilePublisher()
        );
        
        var nativeStructGenerator = new Generator3.Generation.Record.NativeStructGenerator (
            renderer: new Generator3.Renderer.NativeRecordStuctUnit(),
            publisher: new NativeRecordFilePublisher()
        );

        foreach (var bitfield in records)
        {
            nativeFunctionsGenerator.Generate(project, bitfield);
            nativeStructGenerator.Generate(project, bitfield);
        }
    }
    
    public static void Generate(this IEnumerable<Callback> callbacks, string project)
    {
        var generator = new Generator3.Generation.Callback.NativeGenerator(
            renderer: new Generator3.Renderer.NativeCallbackUnit(),
            publisher: new NativeDelegateFilePublisher()
        );
        
        foreach(var callback in callbacks)
            generator.Generate(project, callback);
    }
    
    public static void Generate(this IEnumerable<Constant> constants, string project)
    {
        var generator = new Generator3.Generation.Constants.Generator(
            renderer: new Generator3.Renderer.ConstantsUnit(),
            publisher: new ClassFilePublisher()
        );
        
        generator.Generate(project, constants);
    }
    
    public static void Generate(this IEnumerable<Method> functions, string project)
    {
        var generator = new Generator3.Generation.Functions.NativeGenerator(
            renderer: new Generator3.Renderer.NativeFunctionsUnit(),
            publisher: new NativeClassFilePublisher()
        );
        
        generator.Generate(project, functions);
    }
}
