using System;

namespace Build
{
    public class IntegrationTest : ITarget
    {
        private readonly Settings _settings;

        public IntegrationTest(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            DotNet.Test(Projects.SolutionDirectory, _settings.Configuration, $"TestCategory={nameof(IntegrationTest)}");
        }
    }
}
