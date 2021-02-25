using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;

namespace Generator
{
    internal static class ClassExtension
    {
        private const string PublicClassStruct = "ClassStruct";
        private const string PrivateClassStruct = "PrivateClassStruct";
        
        public static void AddClassStructs(this Class cls, IEnumerable<Record> classStructs)
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
            RecordType.PublicClass => PublicClassStruct,
            RecordType.PrivateClass => PrivateClassStruct,
            _ => throw new Exception($"Unknown class struct type {type}")
        };
        
        private static IEnumerable<Record> FindClassStructs(IEnumerable<Record> classStructs, Class cls)
            => classStructs.Where(x => x.GLibClassStructFor!.GetSymbol() == cls);
    }
}
