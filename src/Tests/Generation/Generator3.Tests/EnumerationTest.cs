using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Generator3.Generation.Test
{
    [TestClass, TestCategory("UnitTest")]
    public class EnumerationTest
    {
        [TestMethod]
        public void NameIsTransfered()
        {
            var enumeration = Substitute.For<GirModel.Enumeration>();
            enumeration.Name.Returns("MyName");

            var renderer = Substitute.For<Enumeration.Renderer>();

            var generator = new Enumeration.Generator(renderer);
            generator.Generate(enumeration);
            
            renderer.Received().Render(Arg.Is<Enumeration.Data>(data => 
                data.Name == enumeration.Name
            ));
        }
        
        [TestMethod]
        public void NamespaceIsTransfered()
        {
            var enumeration = Substitute.For<GirModel.Enumeration>();
            enumeration.NamespaceName.Returns("MyNamespace");

            var renderer = Substitute.For<Enumeration.Renderer>();

            var generator = new Enumeration.Generator(renderer);
            generator.Generate(enumeration);
            
            renderer.Received().Render(Arg.Is<Enumeration.Data>(data => 
                data.NamespaceName == enumeration.NamespaceName
            ));
        }

        [TestMethod]
        public void MembersAreTransfered()
        {
            var member = Substitute.For<GirModel.Member>();
            member.Name.Returns("MyMember");
            member.Value.Returns(3l);
            
            var enumeration = Substitute.For<GirModel.Enumeration>();
            enumeration.Members.Returns(new []{ member });

            var renderer = Substitute.For<Enumeration.Renderer>();
            var generator = new Enumeration.Generator(renderer);
            generator.Generate(enumeration);
            
            renderer.Received().Render(Arg.Is<Enumeration.Data>(data => 
                data.Members.First().Name == member.Name &&
                data.Members.First().Value == member.Value
            ));
        }
    }
}
