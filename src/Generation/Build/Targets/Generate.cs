using System;
using System.IO;
using System.Linq;
using GirLoader;
using GirLoader.Output;

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
            var generator = new Generator3.Generator3();
            
            generator.GenerateEnumerations(project, repository.Namespace.Enumerations);
            generator.GenerateRecords(project, repository.Namespace.Records);
            generator.GenerateCallbacks(project, repository.Namespace.Callbacks);

            generator.GenerateConstants(project, repository.Namespace.Constants);
            generator.GenerateFunctions(project, repository.Namespace.Functions);
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
