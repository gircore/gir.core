using Bullseye;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Build.Test
{
    [TestClass]
    public class RunnerTest
    {
        #region Helper

        private static Runner GetRunner(out IProjectCleaner projectCleaner, out ILibraryGenerator libraryGenerator,
            out ILibraryBuilder libraryBuilder, out ISampleBuilder sampleBuilder, out ITester tester)
        {
            projectCleaner = Mock.Of<IProjectCleaner>();
            libraryGenerator = Mock.Of<ILibraryGenerator>();
            sampleBuilder = Mock.Of<ISampleBuilder>();
            libraryBuilder = Mock.Of<ILibraryBuilder>();
            tester = Mock.Of<ITester>();

            return new Runner(projectCleaner, libraryGenerator, libraryBuilder, sampleBuilder, tester);
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
                out ISampleBuilder _, 
                out ITester _
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
                out ISampleBuilder _, 
                out ITester _
            );

            RunTarget(runner, "generate");

            Mock.Get(generator).Verify((x) => x.GenerateLibraries(), Times.Once);
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
                out ISampleBuilder _, 
                out ITester _
            );

            RunTarget(runner, target);

            Mock.Get(generator).Verify((x) => x.GenerateLibraries(), Times.Once);
            Mock.Get(builder).Verify((x) => x.BuildLibraries(), Times.Once);
        }

        [TestMethod]
        public void InvokingSamplesTargetExecutesLibraryBuilderAndSampleBuilder()
        {
            Runner runner = GetRunner(
                out IProjectCleaner _, 
                out ILibraryGenerator _, 
                out ILibraryBuilder builder, 
                out ISampleBuilder sampleBuilder, 
                out ITester _
            );

            RunTarget(runner, "samples");

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
                out ISampleBuilder _, 
                out ITester tester
            );

            RunTarget(runner, "test");

            Mock.Get(builder).Verify((x) => x.BuildLibraries(), Times.Once);
            Mock.Get(tester).Verify((x) => x.Test(), Times.Once);
        }
    }
}
