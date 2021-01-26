using System;

namespace Build
{
    public class Integration : ITarget
    {
        private readonly Settings _settings;

        public Integration(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach (var project in Projects.IntegrationProjects)
                DotNet.Build(project, _settings.Configuration);
        }
    }
}
