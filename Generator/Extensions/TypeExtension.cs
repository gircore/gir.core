using System;
using Repository;
using Repository.Model;
using String = Repository.Model.String;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        internal static string WriteType(this Type type, Target target, Namespace currentNamespace, bool useSafeHandle = true)
        {
            var symbol = type.SymbolReference.GetSymbol();
            var name = (symbol, type, target) switch
            {
                (Callback, _, Target.Native)
                    => symbol.Write(target, currentNamespace),
                
                (Callback c, _, Target.Managed)
                    => AddNamespace(currentNamespace, c.Namespace, c.GetMetadataString("ManagedName"), target),
                
                // Return values which return a string without transferring ownership to us can not be marshalled automatically
                // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
                (String, ReturnValue { Transfer: Transfer.None}, Target.Native) => "IntPtr",

                // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
                (String, TransferableType {TypeInformation: {Array:{ Length: null}}, Transfer: Transfer.None}, Target.Native) => "IntPtr",
                
                // Arrays of string can be marshalled automatically, no IntPtr needed
                (String, {TypeInformation: {Array:{}}}, Target.Native) => "string",
                
                // Arrays of byte can be marshalled automatically, no IntPtr needed
                (_, {TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "byte" => "byte",

                // Parameters of record arrays which do not transfer ownership can be marshalled directly
                // as struct[]
                (Record, Parameter {TypeInformation: {IsPointer: false, Array:{}}, Transfer: Transfer.None}, Target.Native) 
                    => symbol.Write(target, currentNamespace),
                
                // Use IntPtr[] for arrays of SafeHandles as those are not supported by the marshaller
                (Record, {TypeInformation: {IsPointer: true, Array:{}}}, Target.Native) => "IntPtr",

                // Use original symbol name for records (remapped to SafeHandles)
                (Record r, {TypeInformation: {IsPointer: true}}, Target.Native) when useSafeHandle
                    => AddNamespace(currentNamespace, r.Namespace, r.GetMetadataString("SafeHandleRefName"), target),
                
                (Record r, _, Target.Managed)
                    => AddNamespace(currentNamespace, r.Namespace, r.GetMetadataString("Name"), target),
                
                (Union u, _, Target.Managed)
                    => AddNamespace(currentNamespace, u.Namespace, u.GetMetadataString("Name"), target),

                // Pointers to primitive value types can be marshalled directly
                (PrimitiveValueType, {TypeInformation:{IsPointer: true}}, Target.Native) => symbol.Write(target, currentNamespace),
                
                // Use IntPtr for all types where a pointer is expected
                (_, {TypeInformation: {IsPointer: true}}, Target.Native) => "IntPtr",
                
                _ => symbol.Write(target, currentNamespace)
            };

            if (type.TypeInformation.Array is { })
                name += "[]";

            return name;
        }

        internal static string Bla(this Type type, Target target, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return target switch
            {
                Target.Managed => BlaManaged(type, currentNamespace, useSafeHandle),
                Target.Native => BlaNative(type, currentNamespace, useSafeHandle)
            };
        }

        internal static string BlaNative(Type type, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return type switch
            {
                // Pointers to primitive value types can be marshalled directly
                {TypeInformation:{IsPointer: true}, SymbolReference: {Symbol: {}s and PrimitiveValueType}} => s.Write(Target.Native, currentNamespace),
                
                // Use IntPtr for all types where a pointer is expected
                {TypeInformation: {IsPointer: true}} => "IntPtr",
                
                _ => type.SymbolReference.Symbol.Write(Target.Native, currentNamespace)
            };
        }

        internal static string BlaManaged(Type type, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return type switch
            {
                (Record r, _, Target.Managed)
                    => AddNamespace(currentNamespace, r.Namespace, r.GetMetadataString("Name"), target),
                
                (Union u, _, Target.Managed)
                    => AddNamespace(currentNamespace, u.Namespace, u.GetMetadataString("Name"), target),
                
                _ => type.SymbolReference.Symbol.Write(Target.Managed, currentNamespace)
            };
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
