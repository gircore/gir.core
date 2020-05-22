namespace Generator
{
    public class Parameter
    {
        string Name { get; }
        string Type { get; }

        public Parameter(string name, string type)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
        }
    }
}