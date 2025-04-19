using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace Secret;

[UnsupportedOSPlatform("OSX")]
[UnsupportedOSPlatform("Windows")]
public static partial class Extensions
{
    public static GLib.HashTable ToSchemaHashTable(this Dictionary<string, SchemaAttributeType> attributes)
    {
        var hashTable = new GLib.HashTable(GLib.Internal.HashTable.New(
            GLib.Functions.StrHash,
            GLib.Functions.StrEqual));

        foreach (KeyValuePair<string, SchemaAttributeType> pair in attributes)
        {
            GLib.HashTable.Insert(
                hashTable,
                GLib.String.New(pair.Key).Handle.GetStr(),
                (IntPtr) pair.Value);
        }

        return hashTable;
    }
}

