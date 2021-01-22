using System;

namespace Build
{
    public class Samples : ITarget
    {
        private readonly Settings _settings;

        public Samples(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach(var project in Projects.SampleProjects)
                DotNet.Build(project, _settings.Configuration);
        }
    }
}
