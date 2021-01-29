namespace Generator
{
    public class Project
    {
        #region Properties

        public string Folder { get; }
        public string Gir { get; set; }

        #endregion

        #region Constructors

        public Project(string folder, string gir)
        {
            Folder = folder;
            Gir = gir;
        }

        #endregion

        #region Methods

        public override string ToString() => Gir;

        #endregion
    }
}
