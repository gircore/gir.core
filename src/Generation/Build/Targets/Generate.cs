using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var repositoriesXmlModel = Projects.AllLibraries.Select(LoadXmlModel);
            var loader = new GirLoader.Loader(ResolveInclude);
            var outputRepositories = loader.Load(repositoriesXmlModel);
            GenerateRepositories(outputRepositories);
        }

        private void GenerateRepositories(IEnumerable<Repository> repositories)
        {
            if (_settings.GenerateAsynchronously)
                Parallel.ForEach(repositories, GenerateRepository);
            else
                foreach (var repository in repositories)
                    GenerateRepository(repository);
        }

        private void GenerateRepository(Repository repository)
        {
            if (repository.Namespace.SharedLibrary is null)
                throw new Exception($"Shared library is not set for project {repository.Namespace.ToCanonicalName()}");

            Framework.Generate(repository.Namespace.ToCanonicalName(), repository.Namespace.SharedLibrary, repository.Namespace.Name);

            repository.Namespace.Enumerations.Generate();
            repository.Namespace.Bitfields.Generate();
            repository.Namespace.Records.Generate();
            repository.Namespace.Unions.Generate();
            repository.Namespace.Callbacks.Generate();
            repository.Namespace.Constants.Generate();
            repository.Namespace.Functions.Generate();
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
