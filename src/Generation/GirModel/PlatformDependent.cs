namespace GirModel;

/// <summary>
/// Optional interface which allows types to express that they are platform dependent.
/// </summary>
public interface PlatformDependent
{
    bool SupportsLinux { get; }
    bool SupportsMacos { get; }
    bool SupportsWindows { get; }
}
