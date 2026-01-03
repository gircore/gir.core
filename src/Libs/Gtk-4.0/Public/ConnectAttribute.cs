using System;

namespace Gtk;

[AttributeUsage(AttributeTargets.Field)]
public class ConnectAttribute(string? objectId = null) : Attribute
{
    public string? ObjectId { get; } = objectId;
}
