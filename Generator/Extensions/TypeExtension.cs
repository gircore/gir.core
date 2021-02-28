using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        public static void AddClassStructs(this Type type, IEnumerable<Record> classStructs)
        {
            var foundClassStructs = FindClassStructs(classStructs, type);
            
            foreach (var classStruct in foundClassStructs)
            {
                var identifier = GetClassStructIdentifier(classStruct.Type);
                classStruct.ManagedName = $"{type.ManagedName}.Native.{identifier}";
                type.Metadata[identifier] = classStruct;
                type.Namespace.RemoveRecord(classStruct);
            }
        }
        
        private static string GetClassStructIdentifier(RecordType type) => type switch
        {
            RecordType.PublicClass => "ClassStruct",
            RecordType.PrivateClass => "PrivateClassStruct",
            _ => throw new Exception($"Unknown class struct type {type}")
        };
        
        private static IEnumerable<Record> FindClassStructs(IEnumerable<Record> classStructs, Type type)
            => classStructs.Where(x => x.GLibClassStructFor!.GetSymbol() == type);
    }
}
