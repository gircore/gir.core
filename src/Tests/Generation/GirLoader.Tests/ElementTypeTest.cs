using System;
using System.IO;
using System.Linq;
using System.Text;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirLoader.Test;

[TestClass, TestCategory("UnitTest")]
public class ElementTypeTest
{
    private static Output.Repository LoadRepository(string namespaceContentXml)
    {
        var xml = $"""
                   <?xml version="1.0"?>
                   <repository xmlns="http://www.gtk.org/introspection/core/1.0"
                               xmlns:c="http://www.gtk.org/introspection/c/1.0"
                               xmlns:glib="http://www.gtk.org/introspection/glib/1.0">
                     <namespace name="Test" version="1.0">
                       <record name="TestRecord" c:type="TestRecord">
                       </record>
                       {namespaceContentXml}
                     </namespace>
                   </repository>
                   """;

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
        var inputRepository = stream.DeserializeGirInputModel();

        return new Loader(DummyResolver.Resolve).Load(new[] { inputRepository }).First();
    }

    [TestMethod]
    public void ElementTypeOfListReturnValueShouldBeLoaded()
    {
        var repository = LoadRepository("""
                                        <function name="get_list" c:identifier="test_get_list">
                                          <return-value transfer-ownership="full">
                                            <type name="GLib.List" c:type="GList*">
                                              <type name="TestRecord"/>
                                            </type>
                                          </return-value>
                                        </function>
                                        """);

        var returnValue = repository.Namespace.Functions.First().ReturnValue;

        var typeReference = (Output.ResolveableTypeReference) returnValue.TypeReference;
        typeReference.ElementTypeReferences.Should().HaveCount(1);
        typeReference.ElementTypeReferences[0].Type.Should().BeOfType<Output.Record>();

        GirModel.ReturnType returnType = returnValue;
        var elementType = returnType.ElementTypes.Should().ContainSingle().Which;
        elementType.Is<GirModel.Record>(out var record).Should().BeTrue();
        record!.Name.Should().Be("TestRecord");
    }

    [TestMethod]
    public void KeyAndValueElementTypesOfHashTableReturnValueShouldBeLoadedInDocumentOrder()
    {
        var repository = LoadRepository("""
                                        <function name="get_hash_table" c:identifier="test_get_hash_table">
                                          <return-value transfer-ownership="full">
                                            <type name="GLib.HashTable" c:type="GHashTable*">
                                              <type name="utf8"/>
                                              <type name="TestRecord"/>
                                            </type>
                                          </return-value>
                                        </function>
                                        """);

        GirModel.ReturnType returnType = repository.Namespace.Functions.First().ReturnValue;

        returnType.ElementTypes.Should().HaveCount(2);
        returnType.ElementTypes[0].Is<GirModel.String>().Should().BeTrue();
        returnType.ElementTypes[1].Is<GirModel.Record>().Should().BeTrue();
    }

    [TestMethod]
    public void ElementTypeOfListParameterShouldBeLoaded()
    {
        var repository = LoadRepository("""
                                        <function name="take_list" c:identifier="test_take_list">
                                          <return-value transfer-ownership="none">
                                            <type name="none" c:type="void"/>
                                          </return-value>
                                          <parameters>
                                            <parameter name="list" transfer-ownership="full">
                                              <type name="GLib.SList" c:type="GSList*">
                                                <type name="TestRecord"/>
                                              </type>
                                            </parameter>
                                          </parameters>
                                        </function>
                                        """);

        GirModel.Parameter parameter = repository.Namespace.Functions.First().ParameterList.SingleParameters.First();

        var elementType = parameter.ElementTypes.Should().ContainSingle().Which;
        elementType.Is<GirModel.Record>(out var record).Should().BeTrue();
        record!.Name.Should().Be("TestRecord");
    }

    [TestMethod]
    public void ElementTypeOfListFieldShouldBeLoaded()
    {
        var repository = LoadRepository("""
                                        <record name="FieldRecord" c:type="FieldRecord">
                                          <field name="data">
                                            <type name="GLib.SList" c:type="GSList*">
                                              <type name="TestRecord"/>
                                            </type>
                                          </field>
                                        </record>
                                        """);

        GirModel.Field field = repository.Namespace.Records
            .First(x => x.Name == "FieldRecord")
            .Fields.First();

        var elementType = field.ElementTypes.Should().ContainSingle().Which;
        elementType.Is<GirModel.Record>(out var record).Should().BeTrue();
        record!.Name.Should().Be("TestRecord");
    }

    [TestMethod]
    public void ReturnValueWithoutElementTypeShouldHaveNoElementTypes()
    {
        var repository = LoadRepository("""
                                        <function name="get_int" c:identifier="test_get_int">
                                          <return-value transfer-ownership="none">
                                            <type name="gint" c:type="int"/>
                                          </return-value>
                                        </function>
                                        """);

        GirModel.ReturnType returnType = repository.Namespace.Functions.First().ReturnValue;

        returnType.ElementTypes.Should().BeEmpty();
    }

    [TestMethod]
    public void ArrayElementTypeOfListReturnValueShouldBeLoaded()
    {
        //Occurs in the wild: g_dtls_client_connection_get_accepted_cas returns
        //a GLib.List of GLib.ByteArray elements.
        var repository = LoadRepository("""
                                        <function name="get_list_of_byte_arrays" c:identifier="test_get_list_of_byte_arrays">
                                          <return-value transfer-ownership="full">
                                            <type name="GLib.List" c:type="GList*">
                                              <array name="GLib.ByteArray">
                                                <type name="guint8" c:type="guint8"/>
                                              </array>
                                            </type>
                                          </return-value>
                                        </function>
                                        """);

        GirModel.ReturnType returnType = repository.Namespace.Functions.First().ReturnValue;

        var elementType = returnType.ElementTypes.Should().ContainSingle().Which;
        elementType.IsGLibByteArray().Should().BeTrue();
    }

    [TestMethod]
    public void ElementTypeOfArrayReturnValueShouldBeLoaded()
    {
        var repository = LoadRepository("""
                                        <function name="get_strings" c:identifier="test_get_strings">
                                          <return-value transfer-ownership="full">
                                            <array c:type="char**">
                                              <type name="utf8" c:type="char*"/>
                                            </array>
                                          </return-value>
                                        </function>
                                        """);

        GirModel.ReturnType returnType = repository.Namespace.Functions.First().ReturnValue;

        var elementType = returnType.ElementTypes.Should().ContainSingle().Which;
        elementType.Is<GirModel.String>().Should().BeTrue();
    }

    [TestMethod]
    public void UnresolvableElementTypeShouldThrow()
    {
        var repository = LoadRepository("""
                                        <function name="get_list" c:identifier="test_get_list">
                                          <return-value transfer-ownership="full">
                                            <type name="GLib.List" c:type="GList*">
                                              <type name="Unknown.Thing"/>
                                            </type>
                                          </return-value>
                                        </function>
                                        """);

        GirModel.ReturnType returnType = repository.Namespace.Functions.First().ReturnValue;

        var act = () => returnType.ElementTypes;
        act.Should().Throw<InvalidOperationException>();
    }
}
