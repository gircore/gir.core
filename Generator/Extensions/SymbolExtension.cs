using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class SymbolExtension
    {
        public static bool IsIntPtr(this Symbol symbol, Target target) => target switch
        {
            Target.Managed => symbol.ManagedName.StartsWith("IntPtr"),
            Target.Native => symbol.NativeName.StartsWith("IntPtr"),
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };

        public static bool IsForeignTo(this Symbol symbol, Namespace ns)
            => symbol.Namespace is not null && ns != symbol.Namespace;

        public static bool IsReferenceType(this Symbol symbol) => symbol switch
        {
            Class => true,
            Interface => true,
            Record { Type: RecordType.Ref } => true,
            Record {Type: RecordType.Value } => true,
            _ => false
        };

        public static void AddClassStructs(this Symbol symbol, IEnumerable<Record> classStructs)
        {
            var foundClassStructs = FindClassStructs(classStructs, symbol);

            foreach (var classStruct in foundClassStructs)
            {
                var identifier = GetClassStructIdentifier(classStruct.Type);
                classStruct.ManagedName = $"{symbol.ManagedName}.Native.{identifier}";
                symbol.Metadata[identifier] = classStruct;

                if (symbol.Namespace is null)
                    throw new Exception($"Can not add class structs to symbol {symbol.Name} because the symbol is missing its namespace");

                symbol.Namespace?.RemoveRecord(classStruct);
            }
        }

        private static string GetClassStructIdentifier(RecordType type) => type switch
        {
            RecordType.PublicClass => "ClassStruct",
            RecordType.PrivateClass => "PrivateClassStruct",
            _ => throw new Exception($"Unknown class struct type {type}")
        };

        private static IEnumerable<Record> FindClassStructs(IEnumerable<Record> classStructs, Symbol symbol)
            => classStructs.Where(x => x.GLibClassStructFor?.GetSymbol() == symbol);

        public static string WriteNativeSummary(this Symbol symbol)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Native name: {symbol.NativeName}.");
            builder.AppendLine($"/// </summary>");
            return builder.ToString();
        }
    }
}
