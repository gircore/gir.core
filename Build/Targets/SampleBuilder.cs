using System;

namespace Build
{
    public interface ISampleBuilder
    {
        void BuildSamples();
    }
    
    public class SampleBuilder : ISampleBuilder
    {
        private readonly Settings _settings;

        public SampleBuilder(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void BuildSamples()
        {
            foreach(var project in Projects.SampleProjects)
                DotNet.Build(project, _settings.Configuration);
        }
    }
}
