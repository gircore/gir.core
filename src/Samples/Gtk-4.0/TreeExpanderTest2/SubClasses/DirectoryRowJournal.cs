
/// <summary>
/// Рядок для табличного списку журналу довідника
/// </summary>
[GObject.Subclass<GObject.Object>]
public partial class DirectoryRowJournal 
{
    public static DirectoryRowJournal New() => NewWithProperties([]);

    /// <summary>
    /// Колекція полів
    /// </summary>
    public Dictionary<string, string?> Fields { get; set; } = [];
}