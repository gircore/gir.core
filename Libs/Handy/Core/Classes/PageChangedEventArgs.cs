using System;

namespace Handy.Core
{
    public class PageChangedEventArgs : EventArgs
    {
        public uint Index { get; }
        public PageChangedEventArgs(uint index)
        {
            this.Index = index;
        }
    }
}