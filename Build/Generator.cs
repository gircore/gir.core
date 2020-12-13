using System;
using Generator;

namespace Build
{
    public interface IGenerator
    {
        void Generate(Project project, Type type);
    }

    public class Generator : IGenerator
    {
        private readonly Settings _settings;
        private const string EnvXmlDocumentation = "GirComments";

        public Generator(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
        
        public void Generate(Project project, Type type)
        {
            SetEnvironmentVariableToGenerateXmlDocumentation();
            RunGenerator(project, type);
        }

        private void SetEnvironmentVariableToGenerateXmlDocumentation()
        {
            if (_settings.GenerateXmlDocumentation)
                Environment.SetEnvironmentVariable(EnvXmlDocumentation, true.ToString());
            else
                Environment.SetEnvironmentVariable(EnvXmlDocumentation, null);
        }

        private void RunGenerator(Project project, Type type)
        {
            var generator = CreateGenerator(project, type);
            generator.GenerateComments = _settings.GenerateComments;
            generator.Generate();
        }

        private global::Generator.IGenerator CreateGenerator(Project project, Type type)
        {
            project.Gir = $"../gir-files/{project.Gir}";

            if (Activator.CreateInstance(type, project) is global::Generator.IGenerator generator)
                return generator;

            throw new Exception($"{type.Name} is not a {nameof(global::Generator.IGenerator)}");
        }
    }
}
