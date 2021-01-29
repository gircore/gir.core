using NuGet.Versioning;

namespace Build
{
    public class Settings
    {
        #region Properties

        public Configuration Configuration { get; init; }
        public bool GenerateComments { get; init; }
        public bool GenerateXmlDocumentation { get; init; }
        public SemanticVersion? Version { get; init; }

        #endregion

        #region Constructors

        public Settings()
        {
            Configuration = Configuration.Debug;
        }

        #endregion
    }
}
