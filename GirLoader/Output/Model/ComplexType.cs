namespace GirLoader.Output.Model
{
    public abstract class ComplexType : Type
    {
        public Repository Repository { get; }
        public TypeName OriginalName { get; }

        protected ComplexType(Repository repository, CType? cType, TypeName name, TypeName originalName) : base(cType, name)
        {
            Repository = repository;
            OriginalName = originalName;
        }
    }
}
