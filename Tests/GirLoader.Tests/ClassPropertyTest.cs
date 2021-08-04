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

        [DataTestMethod]
        [DataRow("gdouble", typeof(Output.Model.Double))]
        [DataRow("double", typeof(Output.Model.Double))]
        [DataRow("int", typeof(Output.Model.Integer))]
        [DataRow("gint", typeof(Output.Model.Integer))]
        public void ResolvedTypeShouldBePrimitive(string ctype, System.Type resultType)
        {
            var propertyName = "MyPropName";

            var repository = GetRepositoryWithStubClass();
            repository.Namespace.Classes.First().Properties.Add(new()
            {
                Name = propertyName,
                Type = new()
                {
                    CType = ctype
                }
            });

            var property = Loader.Load(new[] { repository }).First()
                .Namespace.Classes.First()
                .Properties.First();

            property.Name.Value.Should().Be(propertyName);
            property.TypeReference.Type.Should().BeOfType(resultType);
        }
    }
}
