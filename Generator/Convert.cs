using System;
using Repository.Model;

namespace Generator
{
    internal static class Convert
    {
        internal static string ManagedToNative(string fromParam, Symbol symbol, TypeInformation typeInfo, Namespace currentNamespace)
        {
            // TODO: We need to support disguised structs (opaque types)
            return (symbol, typeInfo) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"Marshal.StructureToPtr<{r.SymbolName}>({fromParam}, {fromParam}Ptr, false);",
                // (Record r, {IsPointer: true, Array:{}}) => $"{arg.ManagedName}.MarshalToStructure<{r.ManagedName}>();",
                // (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{c.ManagedName}.From({arg.ManagedName});",
                (Class c, {IsPointer: true, Array: null}) => $"{fromParam}.Handle",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Can't create delegate for argument {fromParam}"),
                _ => $"({symbol.Write(Target.Native, currentNamespace)}){fromParam}" // Other -> Try a brute-force cast
            };
        }
        
        internal static string NativeToManaged(string fromParam, Symbol symbol, TypeInformation typeInfo, Namespace currentNamespace, Transfer transfer = Transfer.Unknown)
        {
            // TODO: We need to support disguised structs (opaque types)
            return (symbol, typeInfo) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"Marshal.PtrToStructure<{r.SymbolName}>({fromParam})",
                (Record r, {IsPointer: true, Array:{}}) => $"{fromParam}.MarshalToStructure<{r.SymbolName}>()",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{c.SymbolName}.From({fromParam})",
                (Class c, {IsPointer: true, Array: null}) => $"GObject.Object.WrapHandle<{c.SymbolName}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Can't create delegate for argument '{fromParam}'"),
                _ => $"({symbol.Write(Target.Managed, currentNamespace)}){fromParam}" // Other -> Try a brute-force cast
            };
        }
    }
}
