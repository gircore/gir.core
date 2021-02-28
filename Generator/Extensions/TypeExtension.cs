using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;

namespace Generator
{
    internal static class TypeExtension
    {
        public static void AddClassStructs(this Symbol symbol, IEnumerable<Record> classStructs)
        {
            var foundClassStructs = FindClassStructs(classStructs, symbol);
            
            foreach (var classStruct in foundClassStructs)
            {
                var identifier = GetClassStructIdentifier(classStruct.Type);
                classStruct.ManagedName = $"{symbol.ManagedName}.Native.{identifier}";
                symbol.Metadata[identifier] = classStruct;
                symbol.Namespace.RemoveRecord(classStruct);
            }
        }
        
        private static string GetClassStructIdentifier(RecordType type) => type switch
        {
            RecordType.PublicClass => "ClassStruct",
            RecordType.PrivateClass => "PrivateClassStruct",
            _ => throw new Exception($"Unknown class struct type {type}")
        };
        
        private static IEnumerable<Record> FindClassStructs(IEnumerable<Record> classStructs, Symbol symbol)
            => classStructs.Where(x => x.GLibClassStructFor!.GetSymbol() == symbol);
    }
}
