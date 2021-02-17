using System.IO;
using System.Reflection;
using Scriban;

#nullable enable

namespace Generator.Services
{
    public class TemplateReaderService
    {
        public Template ReadGenericTemplate(string resource)
        {
            var objTemplate = ReadTemplate(resource);
            return Template.Parse(objTemplate);
        }

        private static string ReadTemplate(string resource)
        {
            Stream? stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"Generator.Templates.{resource}");

            if (stream == null)
                throw new IOException($"Cannot get template resource file '{resource}'");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
