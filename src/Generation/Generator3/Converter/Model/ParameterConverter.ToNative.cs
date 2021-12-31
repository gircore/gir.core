using System;
using GirModel;

namespace Generator3.Converter
{
    public static partial class ParameterConverter
    {
        public static string? ToNative(this Parameter from, out string variableName)
        {
            #region Primitivevaluetype

            if (from.AnyType.Is<PrimitiveValueType>())
            {
                if (from.IsPointer)
                    throw new NotImplementedException($"{from.AnyType}: Pointed primitive value types can not yet be converted to native");

                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Primitive value type with direction != in not yet supported");

                //We don't need any conversion for native parameters
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region Enumeration

            if (from.AnyType.Is<Enumeration>())
            {
                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Enumeration with direction != in not yet supported");

                //We don't need any conversion for enumerations
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region Bitfield

            if (from.AnyType.Is<Bitfield>())
            {
                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Bitfield with direction != in not yet supported");

                //We don't need any conversion for bitfields
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region String
            if (from.AnyType.Is<GirModel.String>())
            {
                variableName = from.GetPublicName();
                return null;
            }
            #endregion

            #region String array

            if (from.AnyType.IsArray<GirModel.String>())
            {
                var arrayType = from.AnyType.AsT1;
                if (from.Transfer == Transfer.None && arrayType.Length == null)
                {
                    variableName = from.GetConvertedName();
                    return $"var {variableName} = new GLib.Internal.StringArrayNullTerminatedSafeHandle({from.GetPublicName()}).DangerousGetHandle();";
                }
                else
                {
                    //We don't need any conversion for string[]
                    variableName = from.GetPublicName();
                    return null;
                }
            }

            #endregion

            #region Interface

            if (from.AnyType.Is<GirModel.Interface>())
            {
                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: interface parameter with direction != in not yet supported");

                variableName = from.GetConvertedName();
                return $"var {variableName} = ({from.GetPublicName()} as GObject.Object).Handle;";
            }
            #endregion

            #region Interface array
            // Interface Array Conversions
            //ArrayTypeReference { Type: Interface } => $"{fromParam}.Select(iface => (iface as GObject.Object).Handle).ToArray()",

            if (from.AnyType.IsArray<GirModel.Interface>())
            {
                variableName = from.GetConvertedName();
                return $"var {variableName} = {from.GetPublicName()}.Select(iface => (iface as GObject.Object).Handle).ToArray();";
            }
            #endregion

            #region Class

            if (from.AnyType.Is<GirModel.Class>())
            {
                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: class parameter with direction != in not yet supported");

                if (!from.IsPointer)
                    throw new NotImplementedException($"{from.AnyType}: class parameter which is no pointer can not be converted to native");

                if (from.Nullable)
                    variableName = from.GetPublicName() + "?.Handle ?? IntPtr.Zero";
                else
                    variableName = from.GetPublicName() + ".Handle";

                return null;
            }

            #endregion

            #region Class array

            if (from.AnyType.IsArray<GirModel.Class>())
            {
                var arrayType = from.AnyType.AsT1;

                if (arrayType.IsPointer)
                    throw new NotImplementedException($"{from.AnyType}: Pointed class array can not yet be converted to native.");

                variableName = from.GetConvertedName();
                return $"var {variableName} = {from.GetPublicName()}.Select(cls => cls.Handle).ToArray();";
            }

            #endregion

            #region Record
            if (from.AnyType.Is<GirModel.Record>())
            {
                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: record parameter with direction != in not yet supported");

                if (!from.IsPointer)
                    throw new NotImplementedException($"{from.AnyType}: Not pointed record types can not yet be converted to native");

                if (from.Nullable)
                {
                    var record = (GirModel.Record) from.AnyType.AsT0;
                    variableName = from.GetPublicName() + "?.Handle ?? " + record.GetFullyQualifiedInternalNullHandle();
                }
                else
                {
                    variableName = from.GetPublicName() + ".Handle";
                }

                return null;
            }
            #endregion

            #region Record array
            if (from.AnyType.IsArray<GirModel.Record>())
            {
                var arrayType = from.AnyType.AsT1;

                if (!arrayType.IsPointer)
                    throw new NotImplementedException($"{from.AnyType}: Not pointed array record types can not yet be converted to native.");

                variableName = from.GetConvertedName();
                return $"var {variableName} = {from.GetPublicName()}.Select(record => record.Handle.DangerousGethandle()).ToArray();";
            }

            #endregion

            throw new NotImplementedException($"Can't convert from parameter {from.Name} ({from.AnyType}) to managed");
        }
    }
}
