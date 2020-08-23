using System;
using System.Collections.Generic;
using GObject;

namespace Gtk
{
    public partial class Notebook
    {
        private readonly Dictionary<Widget, Widget> data;

        #region Events
        public event EventHandler<PageChangedEventArgs>? PageAdded;
        public event EventHandler<PageChangedEventArgs>? PageRemoved;
        #endregion
        
        #region Properties
        public Property<bool> Scrollable { get; }
        public Property<int> Page { get; }
        public Property<bool> ShowTabs { get; }
        public Property<bool> ShowBorder { get; }
        #endregion

        public Notebook() : this(Sys.Notebook.@new()){ }
        internal Notebook(IntPtr handle) : base(handle) 
        {
            data = new Dictionary<Widget, Widget>();

            Page = PropertyOfInt("page");
            ShowTabs = PropertyOfBool("show-tabs");
            Scrollable = PropertyOfBool("scrollable");
            ShowBorder = PropertyOfBool("show-border");

            RegisterEvent("page-added", OnPageAdded);
            RegisterEvent("page-removed", OnPageRemoved);
        }

        public void InsertPage(string label, Widget child, int position)
        {
            var tabLabel = new Label(label);
            data.Add(child, tabLabel);

            Sys.Notebook.insert_page(Handle, GetHandle(child), GetHandle(tabLabel), position);
        }

        public void RemovePage(Widget child)
        {
            if(!data.ContainsKey(child))
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
            child = ((Widget?)(GObject.Object?)(IntPtr)values[1])!;
            pageNum = (uint)values[2];
        }

        private void OnPageRemoved(ref GObject.Sys.Value[] values)
        {
            GetChildAndPage(ref values, out var child, out var pageNum);
            OnPageRemoved(child, pageNum);
        }

        private void OnPageAdded(ref GObject.Sys.Value[] values)
        {
            GetChildAndPage(ref values, out var child, out var pageNum);
            OnPageAdded(child, pageNum);
        }

        protected void OnPageAdded(Widget child, uint pageNum) => PageAdded?.Invoke(this, new PageChangedEventArgs(child, pageNum));
        protected void OnPageRemoved(Widget child, uint pageNum) => PageRemoved?.Invoke(this, new PageChangedEventArgs(child, pageNum));

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
                data.Clear();
        }
    }
}