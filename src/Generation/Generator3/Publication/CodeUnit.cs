namespace Generator3.Publication
{
    public class CodeUnit
    {
        public string Name { get; }
        public string Project { get; }
        public string Source { get; }
        
        public CodeUnit(string project, string name, string source)
        {
            Name = name;
            Project = project;
            Source = source;
        }
    }
}
