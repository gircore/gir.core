using System;
using System.Runtime.Versioning;
using Sample;

[UnsupportedOSPlatform("OSX")]
[UnsupportedOSPlatform("Windows")]
internal class Program
{
    internal const string ApplicationId = "gir.core.secret";
    internal static Gio.Cancellable Cancellable = default!;

    internal static Adw.ToastOverlay Message = default!;
    internal static void ShowError(string message) => ShowMessage($"❌ {message}");
    internal static void ShowSuccess(string message) => ShowMessage($"✅ {message}");
    internal static void ShowWarning(string message) => ShowMessage($"⚠️ {message}");
    private static void ShowMessage(string message)
    {
        var toast = Adw.Toast.New(message);
        toast.Timeout = 1;
        Message.AddToast(toast);
    }

    private static void Main(string[] _)
    {
        Adw.Module.Initialize();
        Secret.Module.Initialize();

        Cancellable = Gio.Cancellable.New();
        Message = Adw.ToastOverlay.New();

        var application = Adw.Application.New(ApplicationId, Gio.ApplicationFlags.FlagsNone);
        application.OnActivate += (sender, args) =>
        {
            var window = Adw.ApplicationWindow.New((Adw.Application) sender);

            var viewSwitcher = Adw.ViewSwitcher.New();

            viewSwitcher.Stack = Adw.ViewStack.New();
            viewSwitcher.Stack.Vexpand = true;
            viewSwitcher.Stack.Hexpand = true;
            viewSwitcher.Policy = Adw.ViewSwitcherPolicy.Wide;

            var headerBar = Adw.HeaderBar.New();
            headerBar.SetTitleWidget(viewSwitcher);

            var toolbarView = Adw.ToolbarView.New();
            toolbarView.AddTopBar(headerBar);
            toolbarView.Content = viewSwitcher.Stack;

            viewSwitcher.Stack.AddTitledWithIcon(
                AccessToken.Tab(),
                Guid.NewGuid().ToString(),
                "Access Token",
                "security-high-symbolic"
            );

            viewSwitcher.Stack.AddTitledWithIcon(
                MicrosoftIdentity.Tab(),
                Guid.NewGuid().ToString(),
                "Microsoft Identity",
                "user-identity-symbolic"
            );

            Message.SetChild(toolbarView);

            window.Content = Message;

            window.SetDefaultSize(700, 700);
            window.Present();
        };
        application.RunWithSynchronizationContext(null);
    }
}

