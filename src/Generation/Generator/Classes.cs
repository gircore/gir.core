using System.Collections.Generic;
using Generator.Generator;

namespace Generator;

public static class Classes
{
    public static void Generate(IEnumerable<GirModel.Class> classes, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Class>>()
        {
            //Fundamental Generators
            new Generator.Internal.FundamentalClassMethods(publisher),
            new Generator.Internal.FundamentalClassStruct(publisher),
            new Generator.Public.FundamentalClassMethods(publisher),
            new Generator.Public.FundamentalClassFramework(publisher),

            //Standard generators
            new Generator.Internal.ClassMethods(publisher),
            new Generator.Internal.ClassStruct(publisher),
            new Generator.Public.ClassConstructors(publisher),
            new Generator.Public.ClassMethods(publisher),
            new Generator.Public.ClassInterfaceMethods(publisher),
            new Generator.Public.ClassProperties(publisher),
            new Generator.Public.ClassFramework(publisher),
            new Generator.Public.ClassSignals(publisher),
        };

        foreach (var cls in classes)
            foreach (var generator in generators)
                generator.Generate(cls);
    }
}
