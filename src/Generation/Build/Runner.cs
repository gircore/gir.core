using System.Collections.Generic;
using Bullseye;

namespace Build
{
    public class Runner
    {
        private Targets Targets { get; } = new();

        public Runner(params Target[] targets)
        {
            InitializeTargets(targets);
        }

        private void InitializeTargets(IEnumerable<Target> targets)
        {
            foreach (dynamic target in targets)
            {
                AddTarget(target);
            }
        }

        private void AddTarget(Target target)
        {
            Targets.Add(
                name: target.Name,
                dependsOn: target.DependsOn,
                description: target.Description
            );
        }

        private void AddTarget(ExecuteableTarget target)
        {
            Targets.Add(
                name: target.Name,
                dependsOn: target.DependsOn,
                action: target.Execute,
                description: target.Description
            );
        }

        public void Run(IEnumerable<string> targets, Options options)
        {
            Targets.RunWithoutExiting(
                targets: targets,
                options: options,
                messageOnly: _ => true
            );
        }
    }
}
