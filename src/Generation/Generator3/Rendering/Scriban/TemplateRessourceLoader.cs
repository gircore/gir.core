using System.IO;
using System.Reflection;
using Scriban;

namespace Generator3.Rendering.Scriban
{
    internal static class TemplateRessourceLoader
    {
        public static Template Load(string resource)
        {
            var objTemplate = Read(resource);
            return Template.Parse(objTemplate);
        }

        public static string Read(string resource)
        {
            Stream? stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"Generator3.Rendering.Scriban.Templates.{resource}");

            if (stream == null)
                throw new IOException($"Cannot get template resource file '{resource}'");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
