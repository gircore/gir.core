namespace Generator3.Generation
{
    public class CodeUnit
    {
        public string Project { get; }
        public string Name { get; }
        public string Source { get; }

        public CodeUnit(string project, string name, string source)
        {
            Project = project;
            Name = name;
            Source = source;
        }
    }
}
