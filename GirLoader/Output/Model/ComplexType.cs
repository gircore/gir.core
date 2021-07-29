namespace GirLoader.Output.Model
{
    public abstract class ComplexType : Type
    {
        public TypeName OriginalName { get; }

        protected ComplexType(Repository? repository, CType? cType, TypeName name, TypeName originalName) : base(repository, cType, name)
        {
            OriginalName = originalName;
        }
    }
}
