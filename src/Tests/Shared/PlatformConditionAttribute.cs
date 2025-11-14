using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[Flags]
public enum Platform
{
    Windows32 = 1 << 0,
    Windows64 = 1 << 1,
    Windows = Windows32 | Windows64,
    Linux32 = 1 << 2,
    Linux64 = 1 << 3,
    Linux = Linux32 | Linux64,
    OSX32 = 1 << 4,
    OSX64 = 1 << 5,
    OSX = OSX32 | OSX64,
    Unix32 = Linux32 | OSX32,
    Unix64 = Linux64 | OSX64,
    Unix = Unix32 | Unix64,
}

/// <summary>
/// This attribute is used to execute a test class or a test
/// method only if the platform matches, with an optional message.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public sealed class PlatformConditionAttribute : ConditionBaseAttribute
{
    private readonly Platform _platform;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformConditionAttribute" /> class.
    /// </summary>
    /// <param name="platform">The platform that this test supports.</param>
    public PlatformConditionAttribute(Platform platform) : base(ConditionMode.Include)
    {
        _platform = platform;
        IgnoreMessage = $"Test is supported only on {platform}";
    }

    public bool ShouldRun
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    return _platform.HasFlag(Platform.Windows64);
                }

                return _platform.HasFlag(Platform.Windows32);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    return _platform.HasFlag(Platform.Linux64);
                }

                return _platform.HasFlag(Platform.Linux32);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    return _platform.HasFlag(Platform.OSX64);
                }

                return _platform.HasFlag(Platform.OSX32);
            }

            return false;
        }
    }

    public new string? IgnoreMessage { get; }

    public override string GroupName => nameof(PlatformConditionAttribute);
    public override bool IsConditionMet => ShouldRun;
}
