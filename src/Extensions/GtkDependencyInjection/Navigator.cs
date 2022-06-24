using System;
using Microsoft.Extensions.DependencyInjection;

namespace Gtk.Extensions.DependencyInjection;

public class Navigator
{
    private readonly ApplicationWindow _applicationWindow;
    private readonly Lazy<IServiceProvider> _serviceProvider;

    private IServiceScope? _currentScope;

    internal Navigator(ApplicationWindow applicationWindow, Lazy<IServiceProvider> serviceProvider)
    {
        _applicationWindow = applicationWindow;
        _serviceProvider = serviceProvider;
    }

    public void Navigate<T>() where T : Widget
    {
        _currentScope?.Dispose();
        _currentScope = _serviceProvider.Value.CreateScope();

        _applicationWindow.Child = _currentScope.ServiceProvider.GetService<T>();
    }
}
