using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using GirModel;

namespace Generator3.Model.Public
{
    public class Constructor
    {
        private readonly GirModel.Constructor _constructor;

        private string? _publicName;
        private string? _internalName;

        public Class Class { get; }
        public string PublicName => _publicName ??= _constructor.GetPublicName();
        public string InternalName => _internalName ??= _constructor.GetInternalName();
        public bool HidesParentConstructor { get; }

        public GirModel.ReturnType ReturnType => _constructor.ReturnType;
        public IEnumerable<Parameter> Parameters { get; }

        public Constructor(GirModel.Constructor constructor, GirModel.Class @class)
        {
            _constructor = constructor;
            Class = @class;
            Parameters = constructor.Parameters.CreatePublicModels().ToList();

            HidesParentConstructor = @class.Parent is not null && HidesConstructorFrom(@class.Parent, PublicName);
        }

        private bool HidesConstructorFrom(GirModel.Class cls, string publicName)
        {
            var matchingConstructor = cls.Constructors.FirstOrDefault(c => c.GetPublicName() == publicName);

            if (matchingConstructor is null)
                return cls.Parent is not null && HidesConstructorFrom(cls.Parent, publicName);

            GirModel.Parameter[] parameters = Parameters.Select(x => x.Model).ToArray();
            GirModel.Parameter[] foundParameters = matchingConstructor.Parameters.ToArray();

            if (parameters.Length != foundParameters.Length)
                return cls.Parent is not null && HidesConstructorFrom(cls.Parent, publicName);

            for (var i = 0; i < parameters.Length; i++)
            {
                if (!parameters[i].AnyType.Equals(foundParameters[i].AnyType))
                    return cls.Parent is not null && HidesConstructorFrom(cls.Parent, publicName);
            }

            return true;
        }
    }
}
