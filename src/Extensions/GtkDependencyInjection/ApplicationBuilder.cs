using System;
using System.Drawing;
using Gio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gtk.Extensions.DependencyInjection;

public partial class ApplicationBuilder
{
    private readonly ServiceCollection _serviceCollection = new();
    private readonly ApplicationSettings _applicationSettings = new();
    private readonly WindowSettings _windowSettings = new();

    private IServiceProvider? _serviceProvider;
    private Func<ServiceCollection, IServiceProvider>? _serviceProviderFactory;

    public ApplicationBuilder SetServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        where TContainerBuilder : notnull
    {
        _serviceProviderFactory = (serviceCollection) =>
        {
            var builder = factory.CreateBuilder(serviceCollection);
            return factory.CreateServiceProvider(builder);
        };
        return this;
    }

    public ApplicationBuilder AddTransient<T>() where T : class
    {
        _serviceCollection.AddTransient<T>();
        return this;
    }

    public int Show<StartView>() where StartView : Widget
    {
        Gtk.Module.Initialize();

        if (_serviceProviderFactory is null)
            throw new Exception($"Please call {nameof(SetServiceProviderFactory)} first.");

        var application = Application.New(_applicationSettings.Id, _applicationSettings.Flags ?? ApplicationFlags.FlagsNone);
        application.OnActivate += (sender, args) =>
        {
            var window = ApplicationWindow.New((Application) sender);

            var navigator = new Navigator(
                applicationWindow: window,
                serviceProvider: new Lazy<IServiceProvider>(
                    () => _serviceProvider ?? throw new Exception("Missing service provider")
                )
            );

            _serviceCollection.AddSingleton(navigator);
            _serviceProvider = _serviceProviderFactory(_serviceCollection);

            if (_windowSettings.Title is not null)
                window.Title = _windowSettings.Title;

            if (_windowSettings.Size is not null)
                window.SetDefaultSize(_windowSettings.Size.Value.Width, _windowSettings.Size.Value.Height);

            navigator.Navigate<StartView>();
            window.Show();
        };

        return application.Run();
    }
}
