
/// <summary>
/// Ієрархічний рядок
/// </summary>
[GObject.Subclass<GObject.Object>]
public partial class DirectoryHierarchicalRow 
{
    public static DirectoryHierarchicalRow New() => NewWithProperties([]);

    /// <summary>
    /// Цей елемент для завантаження даних
    /// </summary>
    public bool IsLoading { get; set; }

    /// <summary>
    /// Сховище
    /// </summary>
    public Gio.ListStore? Store { get; set; } = null;

    /// <summary>
    /// Колекція полів
    /// </summary>
    public Dictionary<string, string?> Fields { get; set; } = [];
}
