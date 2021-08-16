using System;

namespace Build
{
    public class Integration : ExecuteableTarget
    {
        private readonly Settings _settings;

        public string Description => "Builds the integration library.";

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
