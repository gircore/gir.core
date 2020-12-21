using System;

namespace Build
{
    public interface ITester
    {
        void Test();
    }
    
    public class Tester : ITester
    {
        private readonly Settings _settings;

        public Tester(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Test()
        {
            foreach(var project in Projects.TestProjects)
                DotNet.Test(project, _settings.Configuration);
        }
    }
}
