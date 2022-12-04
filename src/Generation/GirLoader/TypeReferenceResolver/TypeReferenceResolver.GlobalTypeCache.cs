namespace GirLoader;

internal partial class TypeReferenceResolver
{
    private class GlobalTypeCache : TypeCache
    {
        public GlobalTypeCache()
        {
            Add(new Output.Void("none"));
            Add(new Output.Void("void"));

            Add(new Output.Boolean("gboolean"));

            Add(new Output.Float("gfloat"));
            Add(new Output.Float("float"));

            Add(new Output.Pointer("any"));
            Add(new Output.Pointer("gconstpointer"));
            Add(new Output.Pointer("va_list"));
            Add(new Output.GPointer());
            Add(new Output.VoidPointer());
            Add(new Output.Pointer("tm"));
            Add(new Output.Pointer("FILE")); //TODO: Automatic convert to some stream?

            // TODO: Should we use UIntPtr here? Non-CLR compliant
            Add(new Output.UnsignedPointer("guintptr"));

            Add(new Output.UnsignedShort("guint16"));
            Add(new Output.UnsignedShort("gunichar2")); //TOOO: UTF16 char?
            Add(new Output.UnsignedShort("gushort"));

            Add(new Output.Short("gint16"));
            Add(new Output.Short("gshort"));

            Add(new Output.Double("double"));
            Add(new Output.Double("gdouble"));
            Add(new Output.Double("long double"));

            Add(new Output.Integer("int"));
            Add(new Output.Integer("gint"));
            Add(new Output.Integer("gatomicrefcount"));
            Add(new Output.Integer("gint32"));
            Add(new Output.Integer("pid_t"));
            Add(new Output.Integer("grefcount"));

            Add(new Output.UnsignedInteger("unsigned int"));
            Add(new Output.UnsignedInteger("unsigned"));
            Add(new Output.UnsignedInteger("guint"));
            Add(new Output.UnsignedInteger("guint32"));
            Add(new Output.UnsignedInteger("gunichar"));
            Add(new Output.UnsignedInteger("uid_t"));

            Add(new Output.Byte("guchar"));
            Add(new Output.Byte("guint8"));

            Add(new Output.UnpointedSignedByte("gchar"));
            Add(new Output.UnpointedSignedByte("char"));
            Add(new Output.SignedByte("gint8"));

            Add(new Output.Long("glong"));
            Add(new Output.Long("gssize"));
            Add(new Output.Long("gint64"));
            Add(new Output.Long("goffset"));
            Add(new Output.Long("time_t"));

            Add(new Output.NativeUnsignedInteger("gsize"));
            Add(new Output.UnsignedLong("guint64"));
            Add(new Output.UnsignedLong("gulong"));

            Add(new Output.Utf8String());
            Add(new Output.PlatformString());
        }
    }
}
