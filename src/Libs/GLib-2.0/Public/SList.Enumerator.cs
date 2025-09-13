using System;
using System.Collections;
using System.Collections.Generic;

namespace GLib;

public partial class SList
{
    public unsafe struct Enumerator : IEnumerator<SListElement>
    {
        private SList SList { get; }
        private Internal.SListData* CurrentSList { get; set; }

        public Enumerator(SList slist)
        {
            SList = slist;
            Reset();
        }

        public SListElement Current { get; private set; }
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if ((IntPtr) CurrentSList == IntPtr.Zero)
                return false;

            Current = new SListElement
            {
                Data = CurrentSList->Data
            };

            CurrentSList = (Internal.SListData*) CurrentSList->Next;
            return true;
        }

        public void Reset()
        {
            CurrentSList = (Internal.SListData*) SList.Handle.DangerousGetHandle();
        }

        public void Dispose() { }
    }
}
