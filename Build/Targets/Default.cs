namespace Build
{
    public class Default : Target
    {
        public string Description => "Depends on 'build'.";
        public string[] DependsOn => new[] { nameof(Build) };
    }
}
