using System;
using System.Linq;
using Generator = Generator.Generator;

namespace Build
{
    public class Generate : ITarget
    {
        private readonly Settings _settings;
        private const string EnvXmlDocumentation = "GirComments";

        public Generate(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            SetEnvironmentVariableToGenerateXmlDocumentation();
            RunGenerator();
        }

        private void SetEnvironmentVariableToGenerateXmlDocumentation()
        {
            if (_settings.GenerateXmlDocumentation)
                Environment.SetEnvironmentVariable(EnvXmlDocumentation, true.ToString());
            else
                Environment.SetEnvironmentVariable(EnvXmlDocumentation, null);
        }

        private void RunGenerator()
        {
            global::Generator.Generator.Write(
                projects: Projects.AllLibraries.Select(x => x.GirFile),
                outputDir: Projects.ProjectPath
            );
        }
    }
}
