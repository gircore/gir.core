using System;

namespace Build
{
    public class SystemTest : ITarget
    {
        private readonly Settings _settings;

        public SystemTest(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            DotNet.Test(Projects.SolutionDirectory, _settings.Configuration, $"TestCategory={nameof(SystemTest)}");
        }
    }
}
