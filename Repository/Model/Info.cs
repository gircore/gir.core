namespace Repository.Model
{
    public record Info(string Name, string Version)
    {
        public string ToCanonicalName()
            => $"{Name}-{Version}";
    };
}
