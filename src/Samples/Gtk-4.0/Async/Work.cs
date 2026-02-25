using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async;

public class Work
{
    public static async Task WaitForStuffAsync(Gtk.ProgressBar progressBar, int index, CancellationToken cancellationToken)
    {
        try
        {
            var millisecondsDelay = 1000 + index * 50;

            // Set new progress bar text reflecting the current progress
            progressBar.Fraction = 0.0;
            progressBar.Text = $"Starting to wait (task {index + 1})...";
            await Task.Delay(millisecondsDelay, cancellationToken);

            progressBar.Fraction = 0.25;
            progressBar.Text = $"Doing some waiting (task {index + 1})...";
            await Task.Delay(millisecondsDelay, cancellationToken);

            progressBar.Fraction = 0.5;
            progressBar.Text = $"This wait is boring (task {index + 1})...";
            await Task.Delay(millisecondsDelay, cancellationToken);

            progressBar.Fraction = 0.75;
            progressBar.Text = $"It's almost there (task {index + 1})...";
            await Task.Delay(millisecondsDelay, cancellationToken);

            progressBar.Fraction = 1.0;
            progressBar.Text = $"Done! (task {index + 1})";
        }
        catch (Exception)
        {
            // Cancelling the waits for the delay tasks with a cancellation token throws an OperationCanceledException
            progressBar.Fraction = 0.0;
            progressBar.Text = "Cancelled!";
        }
    }

    /* 
     * Run action on main thread by invoking it from an idle handler.
     * Used to process UI updates on the main thread.
     */
    private static void Invoke(Action? action)
    {
        GLib.Functions.IdleAdd(GLib.Constants.PRIORITY_DEFAULT, () =>
        {
            action?.Invoke();
            return false;
        });
    }

    public static void DoHeavyWorkWithInvoke(Gtk.ProgressBar progressBar, int index, CancellationToken cancellationToken)
    {
        try
        {
            var millisecondsDelay = 1000 + index * 50;

            // Set new progress bar text reflecting the current progress
            Invoke(() =>
            {
                progressBar.Fraction = 0.0;
                progressBar.Text = $"Starting some heavy work (task {index + 1})...";
            });
            Task.Delay(millisecondsDelay, CancellationToken.None).Wait(cancellationToken);

            Invoke(() =>
            {
                progressBar.Fraction = 0.25;
                progressBar.Text = $"Doing stuff for task {index + 1}...";
            });
            Task.Delay(millisecondsDelay, CancellationToken.None).Wait(cancellationToken);

            Invoke(() =>
            {
                progressBar.Fraction = 0.5;
                progressBar.Text = $"This task {index + 1} is really hard.";
            });
            Task.Delay(millisecondsDelay, CancellationToken.None).Wait(cancellationToken);

            Invoke(() =>
            {
                progressBar.Fraction = 0.75;
                progressBar.Text = $"Almost there with task {index + 1}.";
            });
            Task.Delay(millisecondsDelay, CancellationToken.None).Wait(cancellationToken);

            Invoke(() =>
            {
                progressBar.Fraction = 1.0;
                progressBar.Text = $"Done! (task {index + 1})";
            });
        }
        catch (Exception)
        {
            // Cancelling the waits for the delay tasks with a cancellation token throws an OperationCanceledException
            Invoke(() =>
            {
                progressBar.Fraction = 0.0;
                progressBar.Text = "Cancelled!";
            });
        }
    }
}
