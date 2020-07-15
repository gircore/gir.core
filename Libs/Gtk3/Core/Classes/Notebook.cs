using System;
using System.Collections.Generic;
using GObject.Core;

namespace Gtk.Core
{
    public class GNotebook : GContainer
    {
        private Dictionary<GWidget, GWidget> data;

        public event EventHandler<PageChangedEventArgs>? PageAdded;
        public event EventHandler<PageChangedEventArgs>? PageRemoved;

        public Property<bool> Scrollable { get; }
        public Property<int> Page { get; }
        public Property<bool> ShowTabs { get; }
        public Property<bool> ShowBorder { get; }

        public GNotebook() : this(Gtk.Notebook.@new()){ }
        internal GNotebook(IntPtr handle) : base(handle) 
        {
            data = new Dictionary<GWidget, GWidget>();

            Page = PropertyOfInt("page");
            ShowTabs = PropertyOfBool("show-tabs");
            Scrollable = PropertyOfBool("scrollable");
            ShowBorder = PropertyOfBool("show-border");

            RegisterEvent("page-added", OnPageAdded);
            RegisterEvent("page-removed", OnPageRemoved);
        }

        public void InsertPage(string label, GWidget child, int position)
        {
            var tabLabel = new GLabel(label);
            data.Add(child, tabLabel);

            Gtk.Notebook.insert_page(this, child, tabLabel, position);
        }

        public void RemovePage(GWidget child)
        {
            if(!data.ContainsKey(child))
                throw new Exception("Not inside this notebook");

            data.Remove(child);
            var index = GetPageNum(child);
            RemovePage(index);
        }

        protected void RemovePage(int page) => Gtk.Notebook.remove_page(this, page);

        public int GetPageNum(GWidget child) => Gtk.Notebook.page_num(this, child);

        public int GetPageCount() => Gtk.Notebook.get_n_pages(this);

        private void GetChildAndPage(ref GObject.Value[] values, out GWidget child, out uint pageNum)
        {
            child = ((GWidget?)(GObject.Core.GObject?)(IntPtr)values[1])!;
            pageNum = (uint)values[2];
        }

        private void OnPageRemoved(ref GObject.Value[] values)
        {
            GetChildAndPage(ref values, out var child, out var pageNum);
            OnPageRemoved(child, pageNum);
        }

        private void OnPageAdded(ref GObject.Value[] values)
        {
            GetChildAndPage(ref values, out var child, out var pageNum);
            OnPageAdded(child, pageNum);
        }

        protected void OnPageAdded(GWidget child, uint pageNum) => PageAdded?.Invoke(this, new PageChangedEventArgs(child, pageNum));
        protected void OnPageRemoved(GWidget child, uint pageNum) => PageRemoved?.Invoke(this, new PageChangedEventArgs(child, pageNum));

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
                data.Clear();
        }
    }
}