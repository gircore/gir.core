using System;
using Gtk;
using Gtk.Extensions.DependencyInjection;

namespace DependencyInjection;

public class ButtonView1 : Button
{
    private readonly Navigator _navigator;

    public ButtonView1(Navigator navigator)
    {
        _navigator = navigator;
        this.Label = "1";
        base.OnClicked += Clicked;
    }

    private void Clicked(Button sender, EventArgs args)
    {
        _navigator.Navigate<ButtonView2>();
    }
}
