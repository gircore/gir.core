using System;
using System.Reflection;

namespace CompositeTemplate;

public static class Ressource
{
    public static GLib.Bytes FromAssembly(string resource)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

        if (stream == null)
            throw new Exception("Cannot get resource file '" + resource + "'");

        var size = (int) stream.Length;
        var buffer = new byte[size];
        stream.ReadExactly(buffer, 0, size);
        stream.Close();

        return GLib.Bytes.New(buffer);
    }
}
