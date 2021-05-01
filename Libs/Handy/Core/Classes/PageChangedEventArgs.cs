using System;

namespace Handy
{
    public class PageChangedEventArgs : EventArgs
    {
        public uint Index { get; }
        public PageChangedEventArgs(uint index)
        {
            Index = index;
        }
    }
}
