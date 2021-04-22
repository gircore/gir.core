using System;

namespace Build
{
    public interface ExecuteableTarget : Target
    {
        void Execute();
    }

    public interface Target
    {
        string Name => this.GetType().Name;

        string Description { get; }
        string[] DependsOn => Array.Empty<string>();
    }
}
