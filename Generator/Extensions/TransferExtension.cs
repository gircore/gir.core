using System;
using GirLoader.Output.Model;

namespace Generator
{
    public static class TransferExtension
    {
        public static bool IsOwnedRef(this Transfer transfer) => transfer switch
        {
            Transfer.None => false,
            Transfer.Full => true,
            Transfer.Container => true,
            _ => throw new Exception("Unknown transfer type")
        };
    }
}
