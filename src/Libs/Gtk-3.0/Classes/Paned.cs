﻿namespace Gtk
{
    public partial class Paned
    {
        public static Paned New(Orientation orientation)
            => new Paned(Internal.Paned.New(orientation), false);
    }
}
