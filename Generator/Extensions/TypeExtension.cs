using System;
using Repository;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        internal static string WriteType(this Type type, Target target,  Namespace currentNamespace, bool useSafeHandle = true)
        {
            var symbol = type.SymbolReference.GetSymbol();
            var name = (symbol, type, target) switch
            {
                //Return values which return a string without transfering ownership to us can not be marshalled automatically
                //as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
                (_, ReturnValue { Transfer: Transfer.None}, _) when symbol.SymbolName == "string" => "IntPtr",

                //Arrays of string can be marshalled automatically, no IntPtr needed
                (_, {TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "string" => "string",
                
                //Arrays of byte can be marshalled automatically, no IntPtr needed
                (_, {TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "byte" => "byte",

                //Parameters of record arrays which do not transfer ownership can be marshalled directly
                //as struct[]
                (Record, Parameter {TypeInformation: {IsPointer: false, Array:{}}, Transfer: Transfer.None}, Target.Native) 
                    => symbol.Write(target, currentNamespace),
                
                //Use IntPtr[] for arrays of SafeHandles as those are not supported by the marshaller
                (Record, {TypeInformation: {IsPointer: true, Array:{}}}, Target.Native) => "IntPtr",

                //Use original symbol name for records (remapped to SafeHandles)
                (Record r, {TypeInformation: {IsPointer: true}}, Target.Native) when useSafeHandle
                    => WriteType(currentNamespace, r.Namespace, r.GetMetadataString("SafeHandleRefName"), target),

                //Pointers to primitive value types can be marshalled directly
                (PrimitiveValueType, {TypeInformation:{IsPointer: true}}, Target.Native) => symbol.Write(target, currentNamespace),
                
                //Use IntPtr for all types where a pointer is expected
                (_, {TypeInformation: {IsPointer: true}}, Target.Native) => "IntPtr",
                
                _ => symbol.Write(target, currentNamespace)
            };

            if (type.TypeInformation.Array is { })
                name += "[]";

            return name;
        }

        private static string WriteType(Namespace currentNamespace, Namespace? targetNamespace, string str, Target target)
        {
            if (!currentNamespace.IsForeignTo(targetNamespace))
                return str;

            if (targetNamespace is null)
                throw new Exception("Targetnamespace is missing");
            
            return targetNamespace.GetName(target) + "." + str;
        }
    }
}
