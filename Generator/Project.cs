namespace Generator
{
    public class Project
    {
        public string Folder { get; }
        public string Gir { get; }

        public Project(string folder, string gir)
        {
            Folder = folder;
            Gir = gir;
        }
    }
}
