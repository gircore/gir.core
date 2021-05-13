namespace Repository.Model
{
    public record Include(string Name, string Version)
    {
        public Repository? ResolvedRepository { get; private set; }

        public void Resolve(Repository repository)
        {
            ResolvedRepository = repository;
        }
        
        public string ToCanonicalName()
            => $"{Name}-{Version}";
    };
}
