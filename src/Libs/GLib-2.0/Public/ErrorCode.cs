using System.Diagnostics;

namespace GLib;

/// <summary>
/// An ErrorCode that consists of a DomainQuark and  an actual error code.
/// </summary>
[DebuggerDisplay("ErrorCode: ErrorDomain={GetErrorDomain()}, ErrorCode={Code}")]
public readonly ref struct ErrorCode
{
    public required Quark Domain { get; init; }
    public required int Code { get; init; }

    public override string ToString()
    {
        return $"{nameof(ErrorCode)}: Domain={GetErrorDomain()}, Code={Code}";
    }

    private string GetErrorDomain()
    {
        return Functions.QuarkToString(Domain).Replace("-quark", string.Empty);
    }
}
