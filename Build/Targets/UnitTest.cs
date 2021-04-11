using System;

namespace Build
{
    public class UnitTest : ITarget
    {
        private readonly Settings _settings;

        public UnitTest(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            DotNet.Test(Projects.SolutionDirectory, _settings.Configuration, $"TestCategory={nameof(UnitTest)}");
        }
    }
}
