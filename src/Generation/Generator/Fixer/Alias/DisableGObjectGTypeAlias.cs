namespace Generator.Fixer.Record;

/// <summary>
/// The alias is added manually in GLib (as GObject.Type) as GLib needs a GType.
/// </summary>
internal class DisableGObjectGTypeAlias : Fixer<GirModel.Alias>
{
    public void Fixup(GirModel.Alias alias)
    {
        if (alias is { Name: "Type", Namespace.Name: "GObject" })
        {
            Model.Alias.Disable(alias);
        }
    }
}
