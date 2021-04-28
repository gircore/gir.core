namespace Build
{
    public abstract class Test : ExecuteableTarget
    {
        private readonly Settings _settings;
        public abstract string[] DependsOn { get; }
        public abstract string Description { get; }

        protected Test(Settings settings)
        {
            _settings = settings;
        }
        
        public void Execute()
        {
            DotNet.Test(Projects.SolutionDirectory, _settings.Configuration, $"TestCategory={((Target)this).Name}");
        }
    }
}
