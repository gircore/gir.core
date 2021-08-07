using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test
{
    [TestClass, TestCategory("UnitTest")]
    public class ClassPropertyTest
    {
        private static Input.Model.Repository GetRepositoryWithStubClass()
        {
            var repository = Helper.GetInputRepository("ns", "1.0");
            repository.Namespace.Classes.Add(new()
            {
                Name = "ClassName",
                GetTypeFunction = "get_type"
            });

            return repository;
        }

        private static Output.Model.Property GetLoadedProperty(string? ctype, string? name)
        {
            var repository = GetRepositoryWithStubClass();
            repository.Namespace.Classes.First().Properties.Add(new()
            {
                Name = "TestProperty",
                Type = new()
                {
                    CType = ctype,
                    Name = name
                }
            });

            return Loader.Load(new[] { repository }).First()
                .Namespace.Classes.First()
                .Properties.First();
        }

        [TestMethod]
        public void PropertyNameShouldBeLoaded()
        {
            var propertyName = "MyPropName";

            var repository = GetRepositoryWithStubClass();
            repository.Namespace.Classes.First().Properties.Add(new()
            {
                Name = propertyName,
                Type = new()
                {
                    CType = "int"
                }
            });

            var property = Loader.Load(new[] { repository }).First()
                .Namespace.Classes.First()
                .Properties.First();

            property.Name.Value.Should().Be(propertyName);
        }

        [DataTestMethod]
        [DataRow("gboolean", false, typeof(Output.Model.Boolean))]
        [DataRow("gfloat", false, typeof(Output.Model.Float))]
        [DataRow("float", false, typeof(Output.Model.Float))]
        [DataRow("guint16", false, typeof(Output.Model.UnsignedShort))]
        [DataRow("gunichar2", false, typeof(Output.Model.UnsignedShort))]
        [DataRow("gushort", false, typeof(Output.Model.UnsignedShort))]
        [DataRow("gint16", false, typeof(Output.Model.Short))]
        [DataRow("gshort", false, typeof(Output.Model.Short))]
        [DataRow("gdouble", false, typeof(Output.Model.Double))]
        [DataRow("double", false, typeof(Output.Model.Double))]
        [DataRow("long double", false, typeof(Output.Model.Double))]
        [DataRow("int", false, typeof(Output.Model.Integer))]
        [DataRow("gint", false, typeof(Output.Model.Integer))]
        [DataRow("gatomicrefcount", false, typeof(Output.Model.Integer))]
        [DataRow("gint32", false, typeof(Output.Model.Integer))]
        [DataRow("pid_t", false, typeof(Output.Model.Integer))]
        [DataRow("grefcount", false, typeof(Output.Model.Integer))]
        [DataRow("unsigned int", false, typeof(Output.Model.UnsignedInteger))]
        [DataRow("unsigned", false, typeof(Output.Model.UnsignedInteger))]
        [DataRow("guint", false, typeof(Output.Model.UnsignedInteger))]
        [DataRow("guint32", false, typeof(Output.Model.UnsignedInteger))]
        [DataRow("gunichar", false, typeof(Output.Model.UnsignedInteger))]
        [DataRow("uid_t", false, typeof(Output.Model.UnsignedInteger))]
        [DataRow("guchar", false, typeof(Output.Model.Byte))]
        [DataRow("guint8", false, typeof(Output.Model.Byte))]
        [DataRow("gchar", false, typeof(Output.Model.SignedByte))]
        [DataRow("char", false, typeof(Output.Model.SignedByte))]
        [DataRow("gint8", false, typeof(Output.Model.SignedByte))]
        [DataRow("glong", false, typeof(Output.Model.Long))]
        [DataRow("gssize", false, typeof(Output.Model.Long))]
        [DataRow("gint64", false, typeof(Output.Model.Long))]
        [DataRow("goffset", false, typeof(Output.Model.Long))]
        [DataRow("time_t", false, typeof(Output.Model.Long))]
        [DataRow("gsize", false, typeof(Output.Model.NativeUnsignedInteger))]
        [DataRow("guint64", false, typeof(Output.Model.UnsignedLong))]
        [DataRow("gulong", false, typeof(Output.Model.UnsignedLong))]
        [DataRow("gboolean*", true, typeof(Output.Model.Boolean))]
        [DataRow("gfloat*", true, typeof(Output.Model.Float))]
        [DataRow("float*", true, typeof(Output.Model.Float))]
        [DataRow("guint16*", true, typeof(Output.Model.UnsignedShort))]
        [DataRow("gunichar2*", true, typeof(Output.Model.UnsignedShort))]
        [DataRow("gushort*", true, typeof(Output.Model.UnsignedShort))]
        [DataRow("gint16*", true, typeof(Output.Model.Short))]
        [DataRow("gshort*", true, typeof(Output.Model.Short))]
        [DataRow("gdouble*", true, typeof(Output.Model.Double))]
        [DataRow("double*", true, typeof(Output.Model.Double))]
        [DataRow("long double*", true, typeof(Output.Model.Double))]
        [DataRow("int*", true, typeof(Output.Model.Integer))]
        [DataRow("gint*", true, typeof(Output.Model.Integer))]
        [DataRow("gatomicrefcount*", true, typeof(Output.Model.Integer))]
        [DataRow("gint32*", true, typeof(Output.Model.Integer))]
        [DataRow("pid_t*", true, typeof(Output.Model.Integer))]
        [DataRow("grefcount*", true, typeof(Output.Model.Integer))]
        [DataRow("unsigned int*", true, typeof(Output.Model.UnsignedInteger))]
        [DataRow("unsigned*", true, typeof(Output.Model.UnsignedInteger))]
        [DataRow("guint*", true, typeof(Output.Model.UnsignedInteger))]
        [DataRow("guint32*", true, typeof(Output.Model.UnsignedInteger))]
        [DataRow("gunichar*", true, typeof(Output.Model.UnsignedInteger))]
        [DataRow("uid_t*", true, typeof(Output.Model.UnsignedInteger))]
        [DataRow("guchar*", true, typeof(Output.Model.Byte))]
        [DataRow("guint8*", true, typeof(Output.Model.Byte))]
        //[DataRow("gchar*", true, typeof(Output.Model.String))] -> Ambigous between UTF8 and platform strings
        //[DataRow("char*", true, typeof(Output.Model.String))] -> Ambigous between UTF8 and platform strings
        [DataRow("gint8*", true, typeof(Output.Model.SignedByte))]
        [DataRow("glong*", true, typeof(Output.Model.Long))]
        [DataRow("gssize*", true, typeof(Output.Model.Long))]
        [DataRow("gint64*", true, typeof(Output.Model.Long))]
        [DataRow("goffset*", true, typeof(Output.Model.Long))]
        [DataRow("time_t*", true, typeof(Output.Model.Long))]
        [DataRow("gsize*", true, typeof(Output.Model.NativeUnsignedInteger))]
        [DataRow("guint64*", true, typeof(Output.Model.UnsignedLong))]
        [DataRow("gulong*", true, typeof(Output.Model.UnsignedLong))]
        public void PropertyTypeShouldMatchCType(string ctype, bool isPointer, System.Type resultType)
        {
            var property = GetLoadedProperty(ctype, null);

            property.TypeReference.CTypeReference!.IsPointer.Should().Be(isPointer);

            var type = property.TypeReference.Type;
            type.Should().BeAssignableTo(resultType);
            type.Should().BeAssignableTo<Output.Model.PrimitiveValueType>();
        }

        [DataTestMethod]
        [DataRow("gchar*", "utf8", typeof(Output.Model.Utf8String))]
        [DataRow("gchar*", "filename", typeof(Output.Model.PlatformString))]
        [DataRow("char*", "utf8", typeof(Output.Model.Utf8String))]
        [DataRow("char*", "filename", typeof(Output.Model.PlatformString))]
        public void PropertyShouldBeString(string ctype, string name, System.Type type)
        {
            var property = GetLoadedProperty(ctype, name);
            property.TypeReference.Type.Should().BeOfType(type);
            property.TypeReference.Type.Should().BeAssignableTo<Output.Model.String>();
        }
    }
}
