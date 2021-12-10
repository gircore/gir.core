namespace GirLoader.Output
{
    public abstract class ComplexType : Type
    {
        public Repository Repository { get; }
        public TypeName OriginalName { get; }

        protected ComplexType(Repository repository, string? cType, TypeName originalName) : base(cType)
        {
            Repository = repository;
            OriginalName = originalName;
        }
    }
}
