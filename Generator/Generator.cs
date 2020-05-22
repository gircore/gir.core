using System;
using System.IO;
using Gir;

namespace Generator
{
    public class Generator
    {
        private readonly string girFile;
        private readonly string outputDir;
        private GRepository repository;

        public Generator(string girFile, string outputDir)
        {
            this.girFile = girFile ?? throw new System.ArgumentNullException(nameof(girFile));
            this.outputDir = outputDir ?? throw new System.ArgumentNullException(nameof(outputDir));

            var reader = new GirReader();
            repository = reader.ReadRepository(girFile);

            Directory.CreateDirectory(outputDir);
        }

        public void GenerateClasses()
        {
            if(repository.Namespace is null)
            {
                Console.WriteLine($"Could not create classes for {girFile}. Namespace is missing.");
                return;
            }

            foreach(var cls in repository.Namespace.Classes)
            {
                try
                {
                    if(cls.Type is null)
                    {
                        Console.WriteLine($"Can not create {cls.Name}, type is missing.");
                        continue;
                    }

                    Write(cls.Type, "");
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine($"Could not create class {cls.Name}: {ex.Message}");
                }
            }
        }

        private void Write(string name, string content)
        {
            var fileName = name + ".cs";
            var path = Path.Combine(outputDir, fileName);

            File.WriteAllText(path, content);
        }
    }
}