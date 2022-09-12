using System.Drawing;
using DependencyInjection;
using Gtk.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

new ApplicationBuilder()
    .SetServiceProviderFactory(new DefaultServiceProviderFactory())
    .AddTransient<ButtonView1>()
    .AddTransient<ButtonView2>()
    .SetApplicationId("org.gir.core")
    .SetWindowTitle("WindowTitle")
    .SetWindowDefaultSize(new Size(800, 600))
    .Show<ButtonView1>();
