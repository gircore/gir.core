using System;
using Generator;

namespace Build
{
    public interface ILibraryBuilder
    {
        void BuildLibraries();
    }

    public interface ISampleBuilder
    {
        void BuildSamples();
    }

    public interface ITester
    {
        void Test();
    }

    public class DotNetExecutor : ILibraryBuilder, ITester, ISampleBuilder
    {
        private readonly Settings _settings;

        public DotNetExecutor(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void BuildLibraries()
        {
            foreach((Project project, Type _) in Projects.LibraryProjects)
                DotNet.Build(project.Folder, _settings.Configuration);
        }
        
        public void Test()
        {
            foreach(var project in Projects.TestProjects)
                DotNet.Test(project, _settings.Configuration);
        }

        public void BuildSamples()
        {
            foreach(var project in Projects.SampleProjects)
                DotNet.Build(project, _settings.Configuration);
        }
    }
}
