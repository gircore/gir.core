namespace Generator
{
    public class Project
    {
        #region Properties
        public string Folder { get; }
        public string Gir { get; set; }

        #endregion

        public Project(string folder, string gir)
        {
            Folder = folder;
            Gir = gir;
        }

        public override string ToString() => Gir;
    }
}