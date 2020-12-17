namespace Build
{
    public class Settings
    {
        #region Properties

        public Configuration Configuration { get; init; }
        public bool GenerateComments { get; init; }
        public bool GenerateXmlDocumentation { get; init; }
        
        #endregion
        
        #region Constructors

        public Settings()
        {
            Configuration = Configuration.Debug;
        }
        
        #endregion
    }
}
