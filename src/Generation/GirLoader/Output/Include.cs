using System;

namespace GirLoader.Output;

public record Include(string Name, string Version)
{
    public Repository? ResolvedRepository { get; private set; }

    public void Resolve(Repository repository)
    {
        ResolvedRepository = repository;
    }

    public Repository GetResolvedRepository()
    {
        if (ResolvedRepository is null)
            throw new Exception($"{nameof(Include)} {Name} is not resolved");

        return ResolvedRepository;
    }

    public string ToCanonicalName()
        => $"{Name}-{Version}";
};
