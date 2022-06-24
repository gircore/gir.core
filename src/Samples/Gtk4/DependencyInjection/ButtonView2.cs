using System;
using Gtk;
using Gtk.Extensions.DependencyInjection;

namespace DependencyInjection;

public class ButtonView2 : Button
{
    private readonly Navigator _navigator;

    public ButtonView2(Navigator navigator)
    {
        _navigator = navigator;
        this.Label = "2";
        this.OnClicked += Clicked;
    }

    private void Clicked(Button sender, EventArgs args)
    {
        _navigator.Navigate<ButtonView1>();
    }
}
