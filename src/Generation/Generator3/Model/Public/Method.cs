using System.Collections.Generic;
using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class Method
    {
        private readonly GirModel.Method _method;

        private string? _publicName;
        private string? _internalName;

        public string ClassName { get; }
        public string PublicName => _publicName ??= _method.GetPublicName();
        public string InternalName => _internalName ??= _method.GetInternalName();

        public GirModel.ReturnType ReturnType => _method.ReturnType;
        public IEnumerable<Parameter> Parameters { get; }

        public Method(GirModel.Method method, string className)
        {
            ClassName = className;
            _method = method;
            
            Parameters = method.Parameters.CreatePublicModels();
        }

        public bool IsFree() => _method.IsFree() || _method.IsUnref();
    }
}
