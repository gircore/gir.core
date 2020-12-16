using System;

namespace Build
{
    public interface IBuilder
    {
        void Build(string project);
    }

    public interface ITester
    {
        void Test(string project);
    }

    public class DotNetExecutor : IBuilder, ITester
    {
        private readonly Settings _settings;

        public DotNetExecutor(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Build(string project)
        {
            DotNet.Build(project, _settings.Configuration);
        }
        
        public void Test(string project)
        {
            DotNet.Test(project, _settings.Configuration);
        }
    }
}
