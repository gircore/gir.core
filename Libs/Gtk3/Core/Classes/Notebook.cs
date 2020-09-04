using System;
using System.Collections.Generic;
using GObject;

namespace Gtk
{
    public partial class Notebook
    {
        private readonly Dictionary<Widget, Widget> data = new Dictionary<Widget, Widget>();

        public Notebook() : this(Sys.Notebook.@new()) { }

        public void InsertPage(string label, Widget child, int position)
        {
            var tabLabel = new Label(label);
            data.Add(child, tabLabel);

            Sys.Notebook.insert_page(Handle, GetHandle(child), GetHandle(tabLabel), position);
        }

        public void RemovePage(Widget child)
        {
            if (!data.ContainsKey(child))
                throw new Exception("Not inside this notebook");

            data.Remove(child);
            var index = GetPageNum(child);
            RemovePage(index);
        }

        protected void RemovePage(int page) => Sys.Notebook.remove_page(Handle, page);

        public int GetPageNum(Widget child) => Sys.Notebook.page_num(Handle, GetHandle(child));

        public int GetPageCount() => Sys.Notebook.get_n_pages(Handle);

        private static void GetChildAndPage(ref GObject.Sys.Value[] values, out Widget child, out uint pageNum)
        {
            //TODO
            //child = ((Widget?)(GObject.Object?)(IntPtr)values[1])!;
            //pageNum = (uint)values[2];
            child = null!;
            pageNum = 0;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                data.Clear();
        }
    }
}
