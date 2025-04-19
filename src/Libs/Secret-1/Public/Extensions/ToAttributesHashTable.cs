using System.Collections.Generic;
using System.Runtime.Versioning;

namespace Secret;

[UnsupportedOSPlatform("OSX")]
[UnsupportedOSPlatform("Windows")]
public static partial class Extensions
{
    public static GLib.HashTable ToAttributesHashTable(this Dictionary<string, string> attributes)
    {
        var hashTable = new GLib.HashTable(GLib.Internal.HashTable.New(
            GLib.Functions.StrHash,
            GLib.Functions.StrEqual));

        foreach (KeyValuePair<string, string> pair in attributes)
        {
            GLib.HashTable.Insert(hashTable,
                GLib.String.New(pair.Key).Handle.GetStr(),
                GLib.String.New(pair.Value).Handle.GetStr());
        }

        return hashTable;
    }
}

