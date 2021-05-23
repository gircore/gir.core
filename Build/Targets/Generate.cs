using System;
using System.Linq;

namespace Build
{
    public class Generate : ExecuteableTarget
    {
        private readonly Settings _settings;
        private const string EnvXmlDocumentation = "GirComments";

        public string Description => "Generates the source code files.";

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
            try
            {
                var generator = new Generator.Generator
                {
                    OutputDir = Projects.ProjectPath,
                    UseAsync = _settings.GenerateAsynchronously,
                    GenerateDocComments = _settings.GenerateComments
                };

                generator.Write(Projects.AllLibraries.Select(x => new GirLoader.GirFile(x.GirFile)));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Log.Error("An error occurred while writing files. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
            }
        }
    }
}
