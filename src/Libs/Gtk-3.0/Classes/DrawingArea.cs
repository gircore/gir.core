﻿namespace Gtk
{
    public partial class DrawingArea
    {
        public static DrawingArea New()
            => new DrawingArea(Internal.DrawingArea.New(), false);
    }
}
