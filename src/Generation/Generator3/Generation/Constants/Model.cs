using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Public;

namespace Generator3.Generation.Constants
{
    public class Model
    {
        private readonly IEnumerable<GirModel.Constant> _constants;

        public string NamespaceName => _constants.First().Namespace.GetPublicName();
        public IEnumerable<Constant> Constants { get; }

        public Model(IEnumerable<GirModel.Constant> constants)
        {
            _constants = constants;

            Constants = constants
                .Where(IsValid)
                .Select(constant => new Constant(constant))
                .ToList();
        }

        private bool IsValid(GirModel.Constant constant)
        {
            switch (constant.Type)
            {
                case GirModel.PrimitiveType:
                    return IsValidPrimitiveType(constant);
                case GirModel.Bitfield:
                    return true;
                default:
                    Log.Warning($"Excluding {constant.Namespace} constant '{constant.Name}'. Can't assign value '{constant.Value}' to unsupported type '{constant.Type.GetName()}'.");
                    return false;
            }
        }

        private bool IsValidPrimitiveType(GirModel.Constant constant)
        {
            var canParse = constant.Type switch
            {
                GirModel.Bool => bool.TryParse(constant.Value, out _),
                GirModel.Byte => byte.TryParse(constant.Value, out _),
                GirModel.Double => double.TryParse(constant.Value, out _),
                GirModel.Integer => int.TryParse(constant.Value, out _),
                GirModel.Long => long.TryParse(constant.Value, out _),
                GirModel.NativeUnsignedInteger => nuint.TryParse(constant.Value, out _),
                GirModel.Short => short.TryParse(constant.Value, out _),
                GirModel.SignedByte => sbyte.TryParse(constant.Value, out _),
                GirModel.String => true,
                GirModel.UnsignedInteger => uint.TryParse(constant.Value, out _),
                GirModel.UnsignedLong => ulong.TryParse(constant.Value, out _),
                GirModel.UnsignedShort => ushort.TryParse(constant.Value, out _),
                _ => false
            };

            if (canParse)
                return true;

            Log.Warning($"Excluding {constant.Namespace} constant '{constant.Name}'. Can't convert value '{constant.Value}' to type '{constant.Type.GetName()}'.");

            return false;
        }
    }
}
