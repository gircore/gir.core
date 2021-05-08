using System;
using Repository;
using Repository.Model;
using String = Repository.Model.String;

namespace Generator
{
    internal static class AnyTypeExtension
    {
        internal static string WriteType(this AnyType anyType, Target target, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return target switch
            {
                Target.Managed => WriteManagedType(anyType, currentNamespace, useSafeHandle),
                Target.Native => WriteNativeType(anyType, currentNamespace, useSafeHandle)
            };
        }

        private static string WriteNativeType(AnyType anyType, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return anyType switch
            {
                { SymbolReference: { Symbol: Callback c } } => c.Write(Target.Native, currentNamespace),

                // Return values which return a string without transferring ownership to us can not be marshalled automatically
                // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
                ReturnValue { Transfer: Transfer.None, SymbolReference: { Symbol: String } } => "IntPtr",

                // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
                TransferableAnyType { TypeInformation: { Array: { Length: null } }, SymbolReference: { Symbol: String }, Transfer: Transfer.None } => "IntPtr",

                // Arrays of string can be marshalled automatically, no IntPtr needed
                { TypeInformation: { Array: { } }, SymbolReference: { Symbol: String } } => "string[]",

                // Arrays of byte can be marshalled automatically, no IntPtr needed
                { TypeInformation: { Array: { } }, SymbolReference: { Symbol: { } s } } when s.SymbolName == "byte" => "byte[]",

                // Parameters of record arrays which do not transfer ownership can be marshalled directly
                // as struct[]
                Parameter { TypeInformation: { IsPointer: false, Array: { } }, Transfer: Transfer.None, SymbolReference: { Symbol: Record r } }
                    => r.Write(Target.Native, currentNamespace) + "[]",

                // Arrays of Opaque Structs, GObjects, and GInterfaces cannot be marshalled natively
                // Instead marshal them as variable width pointer arrays (LPArray)
                { TypeInformation: { IsPointer: true, Array: { } }, SymbolReference: { Symbol: Record } } => "IntPtr[]",    // SafeHandles
                { TypeInformation: { IsPointer: true, Array: { } }, SymbolReference: { Symbol: Class } } => "IntPtr[]",     // GObjects
                { TypeInformation: { IsPointer: true, Array: { } }, SymbolReference: { Symbol: Interface } } => "IntPtr[]", // GInterfaces

                // Use original symbol name for records (remapped to SafeHandles)
                { TypeInformation: { IsPointer: true }, SymbolReference: { Symbol: Record r } } when useSafeHandle
                    => AddNamespace(currentNamespace, r.Namespace, r.GetMetadataString("SafeHandleRefName"), Target.Native),

                // Pointers to primitive value types can be marshalled directly
                { TypeInformation: { IsPointer: true }, SymbolReference: { Symbol: PrimitiveValueType s } } => s.Write(Target.Native, currentNamespace),

                // Use IntPtr for all types where a pointer is expected
                { TypeInformation: { IsPointer: true, Array: { Length: not null } } } => "IntPtr[]",
                { TypeInformation: { IsPointer: true } } => "IntPtr",

                { TypeInformation: { Array: { } } } => anyType.SymbolReference.Symbol.Write(Target.Native, currentNamespace) + "[]",
                _ => anyType.SymbolReference.Symbol.Write(Target.Native, currentNamespace)
            };
        }

        private static string WriteManagedType(AnyType anyType, Namespace currentNamespace, bool useSafeHandle = true)
        {
            var result = anyType switch
            {
                { SymbolReference: { Symbol: Callback c } } => AddNamespace(currentNamespace, c.Namespace, c.GetMetadataString("ManagedName"), Target.Managed),
                { SymbolReference: { Symbol: Record r } } => AddNamespace(currentNamespace, r.Namespace, r.GetMetadataString("Name"), Target.Managed),
                { SymbolReference: { Symbol: Union u } } => AddNamespace(currentNamespace, u.Namespace, u.GetMetadataString("Name"), Target.Managed),

                _ => anyType.SymbolReference.Symbol.Write(Target.Managed, currentNamespace)
            };

            if (anyType.TypeInformation.Array is { })
                result += "[]";

            return result;
        }

        private static string AddNamespace(Namespace currentNamespace, Namespace? targetNamespace, string str, Target target)
        {
            if (!currentNamespace.IsForeignTo(targetNamespace))
                return str;

            if (targetNamespace is null)
                throw new Exception("Target namespace is missing");

            return targetNamespace.GetName(target) + "." + str;
        }
    }
}
