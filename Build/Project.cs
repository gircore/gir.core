internal class Project
{
    public string Name { get; }
    public string Gir { get; }
    public string DllImport { get; }
    public bool AddAlias { get; }

    private Project(string name, string gir, string dllImport, bool addAlias)
    {
        Name = name;
        Gir = gir;
        DllImport = dllImport;
        AddAlias = addAlias;
    }

    public static implicit operator Project((string Name, string Gir, string DllImport, bool AddAlias) tuple) => new Project(
        tuple.Name,
        tuple.Gir,
        tuple.DllImport,
        tuple.AddAlias
    );
}