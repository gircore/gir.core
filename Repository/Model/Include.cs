namespace Repository.Model
{
    public record Include(string Name, string Version)
    {
        public string ToCanonicalName()
            => $"{Name}-{Version}";
    };
}
