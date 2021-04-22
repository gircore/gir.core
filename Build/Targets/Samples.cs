using System;

namespace Build
{
    public class Samples : ExecuteableTarget
    {
        private readonly Settings _settings;

        public string Description => "Builds the sample applications.";
        public string[] DependsOn => new[] { nameof(Build), nameof(Integration) };

        public Samples(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach (var project in Projects.SampleProjects)
                DotNet.Build(project, _settings.Configuration);
        }
    }
}
