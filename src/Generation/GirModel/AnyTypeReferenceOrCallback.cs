using System.Diagnostics.CodeAnalysis;

namespace GirModel;

public union AnyTypeReferenceOrCallback(AnyTypeReference, Callback)
{
    public bool IsAnyTypeReference => this switch
    {
        AnyTypeReference => true,
        _ => false
    };

    public bool IsCallback => !IsAnyTypeReference;

    public bool TryPickAnyTypeReference([NotNullWhen(true)] out AnyTypeReference? anyTypeReference)
    {
        if (IsAnyTypeReference)
        {
            anyTypeReference = (AnyTypeReference) Value;
            return true;
        }

        anyTypeReference = null;
        return false;
    }

    public bool TryPickCallback([NotNullWhen(true)] out Callback? callback)
    {
        callback = this switch
        {
            Callback c => c,
            _ => null
        };
        return IsCallback;
    }

    public AnyTypeReference AsAnyTypeReference => (AnyTypeReference) Value;
    public Callback AsCallback => (Callback) Value;
}
