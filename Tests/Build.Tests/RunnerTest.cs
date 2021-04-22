using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Build
{
    [TestClass, TestCategory("UnitTest")]
    public class RunnerTest
    {
        #region Helper

        private static ExecuteableTarget GetActionTarget(string name)
        {
            var targetMock = new Mock<ExecuteableTarget>();
            targetMock.Setup(x => x.Name).Returns(name);

            return targetMock.Object;
        }

        private static Target GetTarget(string name, params string[] dependencies)
        {
            var targetMock = new Mock<Target>();
            targetMock.Setup(x => x.Name).Returns(name);
            targetMock.Setup(x => x.DependsOn).Returns(dependencies);

            return targetMock.Object;
        }

        private static void RunTarget(Runner runner, string target)
        {
            runner.Run(new[] { target }, null!);
        }

        #endregion

        [TestMethod]
        public void RunningActionTargetExecutesAction()
        {
            const string Name = "testName";
            
            ExecuteableTarget target = GetActionTarget(Name);
            Runner runner = new (target);
            RunTarget(runner, Name);
            
            Mock.Get(target).Verify(x => x.Execute(), Times.Once);
        }
        
        [TestMethod]
        public void RunningTargetExecutesDependencies()
        {
            const string TargetName = "testName";
            const string DependencyName = "dependencyName";
            
            Target target = GetTarget(TargetName, dependencies: DependencyName);
            ExecuteableTarget depdendency = GetActionTarget(DependencyName);

            Runner runner = new (target, depdendency);
            
            RunTarget(runner, TargetName);
            Mock.Get(depdendency).Verify(x => x.Execute(), Times.Once);
        }
    }
}
