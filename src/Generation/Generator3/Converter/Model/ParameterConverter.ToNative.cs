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
                    throw new NotImplementedException($"{from.AnyType}: Pointed primitive value types can not yet be converted to managed");

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

            #region Class

            if (from.AnyType.Is<GirModel.Class>())
            {
                if (from.Direction != Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: class parameter with direction != in not yet supported");

                if (from.AnyType.AsT0 is Class { IsFundamental: true } )
                    throw new NotImplementedException($"Can't convert fundamental class parameter {{from.Name}} ({from.AnyType} to managed");

                //We use the classes "Handle" property
                variableName = from.GetPublicName() + ".Handle";
                return null;
            }

            #endregion

            throw new NotImplementedException($"Can't convert from parameter {from.Name} ({from.AnyType}) to managed");
        }
    }
}
