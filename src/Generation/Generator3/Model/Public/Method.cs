using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Public
{
    public class Method
    {
        private readonly GirModel.Method _method;

        public string ClassName { get; }
        public string ManagedName => GetManagedName();
        public string NativeName => _method.Name;
        
        public GirModel.ReturnType ReturnType => _method.ReturnType;

        public InstanceParameter InstanceParameter { get; }
        public IEnumerable<Parameter> Parameters { get; }

        public Method(GirModel.Method method, string className)
        {
            ClassName = className;
            _method = method;

            InstanceParameter = method.InstanceParameter.CreatePublicModel();
            Parameters = method.Parameters.CreatePublicModels();
        }

        public bool IsFree() => _method.IsFree() || _method.IsUnref();
        public bool HasInOutRefParameter() => _method.Parameters.Any(param => param.Direction != GirModel.Direction.In);
        public bool HasCallbackReturnType() => _method.ReturnType.AnyType.TryPickT0(out var type, out _) && type is GirModel.Callback;
        public bool HasUnionParameter() => _method.Parameters.Any(param => param.AnyTypeReference.AnyType.TryPickT0(out var type, out _) && type is GirModel.Union);
        public bool HasUnionReturnType() => _method.ReturnType.AnyType.TryPickT0(out var type, out _) && type is GirModel.Union;

        public bool HasArrayClassParameter() => _method.Parameters.Any(param => param.AnyTypeReference.AnyType.TryPickT1(out var arrayType, out _)
                                                                                && arrayType.AnyTypeReference.AnyType.TryPickT0(out var type, out _)
                                                                                && type is GirModel.Class);

        public bool HasArrayClassReturnType() => _method.ReturnType.AnyType.TryPickT1(out var arrayType, out _)
                                                 && arrayType.AnyTypeReference.AnyType.TryPickT0(out var type, out _)
                                                 && type is GirModel.Class;

        private string GetManagedName()
        {
            if (!ReturnType.AnyType.Is<GirModel.Void>() && !Parameters.Any() && !_method.Name.ToLower().StartsWith("get"))
            {
                //This is a "getter" method. We prefix all getter methods with "Get" to avoid naming conflicts
                //with properties of the same name.
                return "Get" + _method.Name;
            }

            return _method.Name;
        }
    }
}
