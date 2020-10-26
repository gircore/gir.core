using GObject;

namespace Gtk
{
    public partial class Notebook
    {
        #region Properties

        public static readonly Property<bool> ScrollableProperty = Property<bool>.Register<Notebook>(
            Native.ScrollableProperty,
            nameof(Scrollable),
            (o) => o.Scrollable,
            (o, v) => o.Scrollable = v
        );

        public bool Scrollable
        {
            get => GetProperty(ScrollableProperty);
            set => SetProperty(ScrollableProperty, value);
        }

        public static readonly Property<int> PageProperty = Property<int>.Register<Notebook>(
            Native.PageProperty,
            nameof(Page),
            (o) => o.Page,
            (o, v) => o.Page = v
        );

        public int Page
        {
            get => GetProperty(PageProperty);
            set => SetProperty(PageProperty, value);
        }

        public static readonly Property<bool> ShowTabsProperty = Property<bool>.Register<Notebook>(
            Native.ShowTabsProperty,
            nameof(ShowTabs),
            (o) => o.ShowTabs,
            (o, v) => o.ShowTabs = v
        );

        public bool ShowTabs
        {
            get => GetProperty(ShowTabsProperty);
            set => SetProperty(ShowTabsProperty, value);
        }

        public static readonly Property<bool> ShowBorderProperty = Property<bool>.Register<Notebook>(
            Native.ShowBorderProperty,
            nameof(ShowBorder),
            (o) => o.ShowBorder,
            (o, v) => o.ShowBorder = v
        );

        public bool ShowBorder
        {
            get => GetProperty(ShowBorderProperty);
            set => SetProperty(ShowBorderProperty, value);
        }

        #endregion

        #region Contructors

        public Notebook() : this(Native.@new()) { }

        #endregion

        #region Methods

        public void InsertPage(string label, Widget child, int position)
        {
            var tabLabel = new Label(label);
            Native.insert_page(Handle, GetHandle(child), GetHandle(tabLabel), position);
        }

        public void RemovePage(Widget child)
        {
            var index = GetPageNum(child);
            RemovePage(index);
        }

        protected void RemovePage(int page) => Native.remove_page(Handle, page);

        public int GetPageNum(Widget child) => Native.page_num(Handle, GetHandle(child));

        public int GetPageCount() => Native.get_n_pages(Handle);

        public Widget this[string label]
        {
            set => Native.append_page(Handle, GetHandle(value), GetHandle(new Label(label)));
        }

        #endregion
    }
}