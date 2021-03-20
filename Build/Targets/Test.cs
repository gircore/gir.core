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
            DotNet.Test(Projects.SOLUTION, _settings.Configuration);
        }
    }
}
