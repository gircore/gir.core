using Repository.Model;

namespace Repository.Analysis
{
    public enum ReferenceType
    {
        Internal,
        External
    }

    public interface ITypeReference
    {
        IType? Type { get;  }
        bool IsForeign { get;  }
        bool IsArray { get; }
        string Name { get; }
    }

    internal interface IResolveable : ITypeReference
    {
        void ResolveAs(IType type, ReferenceType referenceType);
    }
    
    public class TypeReference : ITypeReference, IResolveable
    {
        public IType? Type { get; private set; }
        public bool IsForeign { get; private set; }
        public bool IsArray { get; }
        public string Name { get; }

        public TypeReference(string name, bool isArray)
        {
            Name = name;
            IsArray = isArray;
        }

        public void ResolveAs(IType type, ReferenceType referenceType)
        {
            Type = type;
            IsForeign = (referenceType == ReferenceType.External);
        }
    }
}
