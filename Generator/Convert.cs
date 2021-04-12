using System;
using Repository.Model;

namespace Generator
{
    internal static class Convert
    {
        internal static string ManagedToNative(string fromParam, Symbol symbol, TypeInformation typeInfo, Namespace currentNamespace)
        {
            // TODO: We need to support disguised structs (opaque types)
            var qualifiedType = symbol.Write(Target.Native, currentNamespace);
            
            return (symbol, typeInfo) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"Marshal.StructureToPtr<{qualifiedType}>({fromParam}, {fromParam}Ptr, false);",
                (Record r, {IsPointer: true, Array:{}}) => $"{fromParam}.MarshalToStructure<{qualifiedType}>();",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{qualifiedType}.From({fromParam});",
                (Class c, {IsPointer: true, Array: null}) => $"{fromParam}.Handle",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Can't create delegate for argument {fromParam}"),
                _ => $"({qualifiedType}){fromParam}" // Other -> Try a brute-force cast
            };
        }
        
        internal static string NativeToManaged(string fromParam, Symbol symbol, TypeInformation typeInfo, Namespace currentNamespace, Transfer transfer = Transfer.Unknown)
        {
            // TODO: We need to support disguised structs (opaque types)
            var qualifiedType = symbol.Write(Target.Managed, currentNamespace);
            
            return (symbol, typeInfo) switch
            {
                (Symbol s, _) when s.SymbolName == "string" => $"Marshal.PtrToStringAnsi({fromParam})",
                (Record r, {IsPointer: true, Array: null}) => $"Marshal.PtrToStructure<{qualifiedType}>({fromParam})",
                (Record r, {IsPointer: true, Array:{}}) => $"{fromParam}.MarshalToStructure<{qualifiedType}>()",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{qualifiedType}.From({fromParam})",
                (Class c, {IsPointer: true, Array: null}) => $"GObject.Object.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Can't create delegate for argument '{fromParam}'"),
                _ => $"({qualifiedType}){fromParam}" // Other -> Try a brute-force cast
            };
        }
    }
}
