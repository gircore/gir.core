namespace GirLoader.Output
{
    public abstract class ComplexType : Type
    {
        public Repository Repository { get; }
        public string Name { get; }

        protected ComplexType(Repository repository, string? cType, string name) : base(cType)
        {
            Repository = repository;
            Name = name;
        }
    }
}
