using System;
using System.Collections.Generic;
using GObject.Core;

namespace Gtk.Core
{
    public class GNotebook : GContainer
    {
        private Dictionary<GWidget, GWidget> data;

        public event EventHandler<EventArgs>? PageAdded;
        public event EventHandler<EventArgs>? PageRemoved;

        public Property<bool> Scrollable { get; }
        public Property<int> Page { get; }
        public Property<bool> ShowTabs { get; }
        public Property<bool> ShowBorder { get; }

        public GNotebook() : this(Gtk.Notebook.@new()){ }
        internal GNotebook(IntPtr handle) : base(handle) 
        {
            data = new Dictionary<GWidget, GWidget>();

            Page = Property<int>("page",
                get: GetInt,
                set: Set
            );

            ShowTabs = Property<bool>("show-tabs",
                get: GetBool,
                set: Set
            );

            Scrollable = Property<bool>("scrollable",
                get: GetBool,
                set: Set
            );

            ShowBorder = Property<bool>("show-border",
                get: GetBool,
                set: Set
            );

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

        protected void OnPageAdded() => PageAdded?.Invoke(this, EventArgs.Empty);
        protected void OnPageRemoved() => PageRemoved?.Invoke(this, EventArgs.Empty);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
                data.Clear();
        }
    }
}