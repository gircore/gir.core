using Bullseye;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Build.Test
{
    [TestClass]
    public class RunnerTest
    {
        #region Helper

        private static Runner GetRunner(out IProjectCleaner projectCleaner, out ILibraryGenerator libraryGenerator, out ILibraryBuilder libraryBuilder, out ILibraryPacker libraryPacker, out ISampleBuilder sampleBuilder, out ITester tester, out IIntegrationBuilder integrationBuilder)
        {
            projectCleaner = Mock.Of<IProjectCleaner>();
            libraryGenerator = Mock.Of<ILibraryGenerator>();
            sampleBuilder = Mock.Of<ISampleBuilder>();
            libraryBuilder = Mock.Of<ILibraryBuilder>();
            libraryPacker = Mock.Of<ILibraryPacker>();
            tester = Mock.Of<ITester>();
            integrationBuilder = Mock.Of<IIntegrationBuilder>();

            return new Runner(projectCleaner, libraryGenerator, libraryBuilder, libraryPacker, sampleBuilder, tester, integrationBuilder);
        }

        private static void RunTarget(Runner runner, string target)
        {
            runner.Run(new[] {target}, null!);
        }

        #endregion

        [TestMethod]
        public void InvokingCleanTargetExecutesProjectCleaner()
        {
            Runner runner = GetRunner(
                out IProjectCleaner projectCleaner, 
                out ILibraryGenerator _, 
                out ILibraryBuilder _, 
                out ILibraryPacker _,
                out ISampleBuilder _, 
                out ITester _,
                out IIntegrationBuilder _
            );

            RunTarget(runner, "clean");

            Mock.Get(projectCleaner).Verify((x) => x.CleanProjects(), Times.Once);
        }

        [TestMethod]
        public void InvokingGenerateTargetExecutesLibraryGenerator()
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator generator, 
                out ILibraryBuilder _, 
                out ILibraryPacker _,
                out ISampleBuilder _, 
                out ITester _,
                out IIntegrationBuilder _
            );

            RunTarget(runner, "generate");

            Mock.Get(generator).Verify((x) => x.GenerateLibraries(), Times.Once);
        }
        
        [TestMethod]
        public void InvokingIntegrationTargetExecutesIntegrationBuilder()
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator _, 
                out ILibraryBuilder _, 
                out ILibraryPacker _,
                out ISampleBuilder _, 
                out ITester _,
                out IIntegrationBuilder integrationBuilder
            );

            RunTarget(runner, "integration");

            Mock.Get(integrationBuilder).Verify((x) => x.BuildIntegration(), Times.Once);
        }

        [TestMethod]
        [DataRow("build")]
        [DataRow("default")] //Default target is equal to build target
        public void InvokingBuildTargetExecutesLibraryGeneratorAndLibraryBuilder(string target)
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator generator, 
                out ILibraryBuilder builder, 
                out ILibraryPacker _,
                out ISampleBuilder _, 
                out ITester _,
                out IIntegrationBuilder _
            );

            RunTarget(runner, target);

            Mock.Get(generator).Verify((x) => x.GenerateLibraries(), Times.Once);
            Mock.Get(builder).Verify((x) => x.BuildLibraries(), Times.Once);
        }

        [TestMethod]
        public void InvokingSamplesTargetExecutesLibraryBuilderIntegrationBuilderSampleBuilder()
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator _, 
                out ILibraryBuilder builder, 
                out ILibraryPacker _,
                out ISampleBuilder sampleBuilder, 
                out ITester _,
                out IIntegrationBuilder integrationBuilder
            );

            RunTarget(runner, "samples");

            Mock.Get(integrationBuilder).Verify(x => x.BuildIntegration(), Times.Once);
            Mock.Get(builder).Verify((x) => x.BuildLibraries(), Times.Once);
            Mock.Get(sampleBuilder).Verify((x) => x.BuildSamples(), Times.Once);
        }
        
        [TestMethod]
        public void InvokingTestTargetExecutesLibraryBuilderAndTester()
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator _, 
                out ILibraryBuilder builder, 
                out ILibraryPacker _,
                out ISampleBuilder _, 
                out ITester tester,
                out IIntegrationBuilder _
            );

            RunTarget(runner, "test");

            Mock.Get(builder).Verify((x) => x.BuildLibraries(), Times.Once);
            Mock.Get(tester).Verify((x) => x.Test(), Times.Once);
        }
        
        [TestMethod]
        public void InvokingPackTargetExecutesLibraryBuilderAndPacker()
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator _, 
                out ILibraryBuilder builder, 
                out ILibraryPacker packer,
                out ISampleBuilder _, 
                out ITester _,
                out IIntegrationBuilder _
            );

            RunTarget(runner, "pack");

            Mock.Get(builder).Verify((x) => x.BuildLibraries(), Times.Once);
            Mock.Get(packer).Verify((x) => x.PackLibraries(), Times.Once);
        }
    }
}
