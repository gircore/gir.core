using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Generator3;
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
            var repositoriesXmlModel = Projects.AllLibraries.Select(LoadXmlModel);
            var loader = new GirLoader.Loader(ResolveInclude);

            if (_settings.EnableDebugOutput)
                loader.EnableDebugOutput();

            if (_settings.EnableVerboseOutput)
                loader.EnableVerboseOutput();

            var outputRepositories = loader.Load(repositoriesXmlModel);
            GenerateRepositories(outputRepositories);
        }

        private void GenerateRepositories(IEnumerable<Repository> repositories)
        {
            if (_settings.EnableDebugOutput)
                Generator3.Configuration.EnableDebugOutput();

            if (_settings.EnableVerboseOutput)
                Generator3.Configuration.EnableVerboseOutput();

            if (_settings.GenerateAsynchronously)
                Parallel.ForEach(repositories, repository => repository.Namespace.Generate());
            else
                foreach (var repository in repositories)
                    repository.Namespace.Generate();
        }

        private GirLoader.Input.Repository LoadXmlModel(Project project)
        {
            return new FileInfo(project.GirFile).OpenRead().DeserializeGirInputModel();
        }

        private void FormatCode()
        {
            var project = "GLib-2.0";
            var p = Project.FromName(project);
            DotNet.Format(p.Folder);
        }

        private const string CacheDir = "../../../ext/gir-files";

        public static GirLoader.Input.Repository? ResolveInclude(GirLoader.Output.Include include)
        {
            var fileName = $"{include.Name}-{include.Version}.gir";

            var path = Path.Combine(CacheDir, fileName);
            return File.Exists(path)
                ? new FileInfo(path).OpenRead().DeserializeGirInputModel()
                : null;
        }
    }
}
