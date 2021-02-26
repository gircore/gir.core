using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;

namespace Generator
{
    internal static class ClassExtension
    {
        public static void AddClassStructs(this IComplexSymbol cls, IEnumerable<Record> classStructs)
        {
            var foundClassStructs = FindClassStructs(classStructs, cls);
            
            foreach (var classStruct in foundClassStructs)
            {
                var identifier = GetClassStructIdentifier(classStruct.Type);
                classStruct.ManagedName = $"{cls.ManagedName}.Native.{identifier}";
                cls.Metadata[identifier] = classStruct;
                cls.Namespace.RemoveRecord(classStruct);
            }
        }
        
        private static string GetClassStructIdentifier(RecordType type) => type switch
        {
            RecordType.PublicClass => "ClassStruct",
            RecordType.PrivateClass => "PrivateClassStruct",
            _ => throw new Exception($"Unknown class struct type {type}")
        };
        
        private static IEnumerable<Record> FindClassStructs(IEnumerable<Record> classStructs, IComplexSymbol cls)
            => classStructs.Where(x => x.GLibClassStructFor!.GetSymbol() == cls);
    }
}
