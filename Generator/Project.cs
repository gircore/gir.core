namespace GirLoader.Input.Model
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
