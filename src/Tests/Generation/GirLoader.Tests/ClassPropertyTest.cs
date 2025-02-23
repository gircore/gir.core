using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class ClassPropertyTest
{
    private static Input.Repository GetRepositoryWithStubClass()
    {
        var repository = InputRepositoryHelper.CreateRepository("ns", "1.0");
        repository.Namespace!.Classes.Add(new()
        {
            Name = "ClassName",
            GetTypeFunction = "get_type"
        });

        return repository;
    }

    private static Output.Property GetLoadedProperty(string? ctype, string? name)
    {
        var repository = GetRepositoryWithStubClass();
        repository.Namespace!.Classes.First().Properties.Add(new()
        {
            Name = "TestProperty",
            Type = new()
            {
                CType = ctype,
                Name = name
            }
        });

        return new Loader(DummyResolver.Resolve).Load(new[] { repository }).First()
            .Namespace.Classes.First()
            .Properties.First();
    }

    [TestMethod]
    public void PropertyNameShouldBeLoaded()
    {
        var propertyName = "MyPropName";

        var repository = GetRepositoryWithStubClass();
        repository.Namespace!.Classes.First().Properties.Add(new()
        {
            Name = propertyName,
            Type = new()
            {
                CType = "int"
            }
        });

        var property = new Loader(DummyResolver.Resolve).Load(new[] { repository }).First()
            .Namespace.Classes.First()
            .Properties.First();

        property.Name.Should().Be(propertyName);
    }

    [DataTestMethod]
    [DataRow("gboolean", false, typeof(Output.Boolean))]
    [DataRow("gfloat", false, typeof(Output.Float))]
    [DataRow("float", false, typeof(Output.Float))]
    [DataRow("guint16", false, typeof(Output.UnsignedShort))]
    [DataRow("gunichar2", false, typeof(Output.UnsignedShort))]
    [DataRow("gushort", false, typeof(Output.UnsignedShort))]
    [DataRow("gint16", false, typeof(Output.Short))]
    [DataRow("gshort", false, typeof(Output.Short))]
    [DataRow("gdouble", false, typeof(Output.Double))]
    [DataRow("double", false, typeof(Output.Double))]
    [DataRow("long double", false, typeof(Output.Double))]
    [DataRow("int", false, typeof(Output.Integer))]
    [DataRow("gint", false, typeof(Output.Integer))]
    [DataRow("gatomicrefcount", false, typeof(Output.Integer))]
    [DataRow("gint32", false, typeof(Output.Integer))]
    [DataRow("pid_t", false, typeof(Output.Integer))]
    [DataRow("grefcount", false, typeof(Output.Integer))]
    [DataRow("unsigned int", false, typeof(Output.UnsignedInteger))]
    [DataRow("unsigned", false, typeof(Output.UnsignedInteger))]
    [DataRow("guint", false, typeof(Output.UnsignedInteger))]
    [DataRow("guint32", false, typeof(Output.UnsignedInteger))]
    [DataRow("gunichar", false, typeof(Output.UnsignedInteger))]
    [DataRow("uid_t", false, typeof(Output.UnsignedInteger))]
    [DataRow("guchar", false, typeof(Output.Byte))]
    [DataRow("guint8", false, typeof(Output.Byte))]
    [DataRow("gchar", false, typeof(Output.SignedByte))]
    [DataRow("char", false, typeof(Output.SignedByte))]
    [DataRow("gint8", false, typeof(Output.SignedByte))]
    [DataRow("glong", false, typeof(Output.CLong))]
    [DataRow("gssize", false, typeof(Output.NativeInteger))]
    [DataRow("gint64", false, typeof(Output.Long))]
    [DataRow("goffset", false, typeof(Output.Long))]
    [DataRow("time_t", false, typeof(Output.Long))]
    [DataRow("gsize", false, typeof(Output.NativeUnsignedInteger))]
    [DataRow("guint64", false, typeof(Output.UnsignedLong))]
    [DataRow("gulong", false, typeof(Output.UnsignedCLong))]
    [DataRow("gboolean*", true, typeof(Output.Boolean))]
    [DataRow("gfloat*", true, typeof(Output.Float))]
    [DataRow("float*", true, typeof(Output.Float))]
    [DataRow("guint16*", true, typeof(Output.UnsignedShort))]
    [DataRow("gunichar2*", true, typeof(Output.UnsignedShort))]
    [DataRow("gushort*", true, typeof(Output.UnsignedShort))]
    [DataRow("gint16*", true, typeof(Output.Short))]
    [DataRow("gshort*", true, typeof(Output.Short))]
    [DataRow("gdouble*", true, typeof(Output.Double))]
    [DataRow("double*", true, typeof(Output.Double))]
    [DataRow("long double*", true, typeof(Output.Double))]
    [DataRow("int*", true, typeof(Output.Integer))]
    [DataRow("gint*", true, typeof(Output.Integer))]
    [DataRow("gatomicrefcount*", true, typeof(Output.Integer))]
    [DataRow("gint32*", true, typeof(Output.Integer))]
    [DataRow("pid_t*", true, typeof(Output.Integer))]
    [DataRow("grefcount*", true, typeof(Output.Integer))]
    [DataRow("unsigned int*", true, typeof(Output.UnsignedInteger))]
    [DataRow("unsigned*", true, typeof(Output.UnsignedInteger))]
    [DataRow("guint*", true, typeof(Output.UnsignedInteger))]
    [DataRow("guint32*", true, typeof(Output.UnsignedInteger))]
    [DataRow("gunichar*", true, typeof(Output.UnsignedInteger))]
    [DataRow("uid_t*", true, typeof(Output.UnsignedInteger))]
    [DataRow("guchar*", true, typeof(Output.Byte))]
    [DataRow("guint8*", true, typeof(Output.Byte))]
    //[DataRow("gchar*", true, typeof(Output.Model.String))] -> Ambigous between UTF8 and platform strings
    //[DataRow("char*", true, typeof(Output.Model.String))] -> Ambigous between UTF8 and platform strings
    [DataRow("gint8*", true, typeof(Output.SignedByte))]
    [DataRow("glong*", true, typeof(Output.CLong))]
    [DataRow("gssize*", true, typeof(Output.NativeInteger))]
    [DataRow("gint64*", true, typeof(Output.Long))]
    [DataRow("goffset*", true, typeof(Output.Long))]
    [DataRow("time_t*", true, typeof(Output.Long))]
    [DataRow("gsize*", true, typeof(Output.NativeUnsignedInteger))]
    [DataRow("guint64*", true, typeof(Output.UnsignedLong))]
    [DataRow("gulong*", true, typeof(Output.UnsignedCLong))]
    public void PropertyTypeShouldMatchCType(string ctype, bool isPointer, System.Type resultType)
    {
        var property = GetLoadedProperty(ctype, null);

        property.TypeReference.CTypeReference!.IsPointer.Should().Be(isPointer);

        var type = property.TypeReference.Type;
        type.Should().BeAssignableTo(resultType);
        type.Should().BeAssignableTo<Output.PrimitiveValueType>();
    }

    [DataTestMethod]
    [DataRow("gchar*", "utf8", typeof(Output.Utf8String))]
    [DataRow("gchar*", "filename", typeof(Output.PlatformString))]
    [DataRow("char*", "utf8", typeof(Output.Utf8String))]
    [DataRow("char*", "filename", typeof(Output.PlatformString))]
    public void PropertyShouldBeString(string ctype, string name, System.Type type)
    {
        var property = GetLoadedProperty(ctype, name);
        property.TypeReference.Type.Should().BeOfType(type);
        property.TypeReference.Type.Should().BeAssignableTo<Output.String>();
    }
}
