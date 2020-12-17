using Bullseye;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Build.Test
{
    [TestClass]
    public class RunnerTest
    {
        #region Helper

        private static Runner GetRunner(ICleaner? cleaner = null, IGenerator? generator = null, IBuilder? builder = null, ITester? tester = null )
        {
            if (cleaner is null)
                cleaner = Mock.Of<ICleaner>();

            if (generator is null)
                generator = Mock.Of<IGenerator>();

            if (builder is null)
                builder = Mock.Of<IBuilder>();

            if (tester is null)
                tester = Mock.Of<ITester>();

            return new Runner(cleaner, generator, builder, tester);
        }
        
        #endregion
        
        [TestMethod]
        public void InvokingCleanTargetExecutesCleaner()
        {
            var cleaner = new Mock<ICleaner>();
            Runner runner = GetRunner(cleaner.Object);
            
            runner.Run(new[] { "clean" }, null!);
            
            cleaner.Verify((x)=> x.Clean(), Times.Once);
        }
    }
}
