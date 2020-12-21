using System;

namespace Build
{
    public interface IIntegrationBuilder
    {
        void BuildIntegration();
    }

    public class IntegrationBuilder : IIntegrationBuilder
    {
        private readonly Settings _settings;

        public IntegrationBuilder(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void BuildIntegration()
        {
            foreach (var project in Projects.IntegrationProjects)
                DotNet.Build(project, _settings.Configuration);
        }
    }
}
