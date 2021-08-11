namespace GirLoader.Input
{
    internal class Project
    {
        public string Gir { get; }

        public Project(string girFile)
        {
            Gir = girFile;
        }
    }
}
