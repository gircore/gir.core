using System.Collections.Generic;
using GirModel;

public static class Generation
{
    public static void GenerateEnumerations(this Generator3.Generator3 generator, string project, IEnumerable<Enumeration> enumerations)
    {
        foreach(var enumeration in enumerations)
            generator.GenerateEnumeration(project, enumeration);
    }
    
    public static void GenerateRecords(this Generator3.Generator3 generator, string project, IEnumerable<Record> records)
    {
        foreach(var record in records)
            generator.GenerateRecord(project, record);
    }
    
    public static void GenerateCallbacks(this Generator3.Generator3 generator, string project, IEnumerable<Callback> callbacks)
    {
        foreach(var callback in callbacks)
            generator.GenerateCallback(project, callback);
    }
}
