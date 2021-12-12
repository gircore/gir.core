namespace GirLoader.Output
{
    public abstract class ComplexType : Type
    {
        public Repository Repository { get; }
        public string OriginalName { get; }

        protected ComplexType(Repository repository, string? cType, string originalName) : base(cType)
        {
            Repository = repository;
            OriginalName = originalName;
        }
    }
}
