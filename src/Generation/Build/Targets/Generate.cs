using System;
using System.IO;
using System.Linq;
using GirLoader;
using GirLoader.Output;
using Generator3;

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
            //FormatCode();
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
            var project = "GLib-2.0";
            var loader = new GirLoader.Loader();
            var repository = loader.Load(new [] { LoadInputRepository(Project.FromName(project)) }).First();

            GenerateRepository(project, repository);
        }

        private void GenerateRepository(string project, Repository repository)
        {
            repository.Namespace.Enumerations.Generate(project);
            repository.Namespace.Bitfields.Generate(project);
            repository.Namespace.Records.Generate(project);
            repository.Namespace.Callbacks.Generate(project);
            repository.Namespace.Constants.Generate(project);
            repository.Namespace.Functions.Generate(project);
        }
        
        private GirLoader.Input.Repository LoadInputRepository(Project project)
        {
            return new FileInfo(project.GirFile).OpenRead().DeserializeGirInputModel();
        }

        private void FormatCode()
        {
            var project = "GLib-2.0";
            var p = Project.FromName(project);
            DotNet.Format(p.Folder);
        }
    }
}
