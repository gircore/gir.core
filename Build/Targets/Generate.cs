using System;
using Generator;

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
            foreach (var (project, type) in Projects.LibraryProjects)
            {
                var generator = CreateGenerator(project, type);
                generator.GenerateComments = _settings.GenerateComments;
                generator.Generate();
            }
        }

        private static global::Generator.IGenerator CreateGenerator(Project project, Type type)
        {
            project.Gir = $"../gir-files/{project.Gir}";

            if (Activator.CreateInstance(type, project) is global::Generator.IGenerator generator)
                return generator;

            throw new Exception($"{type.Name} is not a {nameof(global::Generator.IGenerator)}");
        }
    }
}
