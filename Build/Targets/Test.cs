using System;

namespace Build
{
    public class Test : ITarget
    {
        private readonly Settings _settings;

        public Test(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach (var project in Projects.TestProjects)
                DotNet.Test(project, _settings.Configuration);
        }
    }
}
