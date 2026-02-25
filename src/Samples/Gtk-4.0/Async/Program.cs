using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Async;

/*
 * Sample for processing UI updates from asynchronous code or tasks using two methods:
 * 
 * 1. Using the GirCore MainLoopSynchronizationContext that makes sure any awaited code is queued in the correct order and then run on the UI thread.
 *   - This method calls into "Work.WaitForStuffAsync".
 *   - Any UI updates are added to the UI queue automatically when awaited.
 * 
 * 2. Manually creating a task from anywhere that doesn't necessarily use async/await patterns.
 *   - This method calls into "Work.DoHeavyWorkWithInvoke" and would be doing some heavy computations.
 *   - Any UI updates need to be done in a delegate invoked by the UI queue later on (see "Invoke").
 */

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Async Sample";
    window.SetDefaultSize(800, 250);

    var hbox = Gtk.Box.New(Gtk.Orientation.Horizontal, 5);

    SampleBox asyncSample = SetupSampleBox("Async");
    hbox.Append(asyncSample.Box);

    SampleBox tasksSample = SetupSampleBox("Tasks");
    hbox.Append(tasksSample.Box);

    /*
     * Sample Part 1
     * 
     * 'OnClicked' event handler using awaits that are queued
     * and kept in sync automatically by GirCore using a synchronization context.
     */
    asyncSample.RunButton.OnClicked += async (button, args) =>
    {
        if (asyncSample.WorkRunning)
            return;

        asyncSample.WorkRunning = true;
        asyncSample.CancelTokenSrc = new CancellationTokenSource();

        asyncSample.RunButton.Visible = false;
        asyncSample.CancelButton.Visible = true;
        asyncSample.ProgressBar.Visible = true;
        asyncSample.Spinner.Start();

        /*
         * Do some heavy work asynchronously using await.
         * Run multiple tasks at the same time to force multiple threads queueing UI actions.
         */
        await Task.WhenAll(
            Enumerable.Range(0, 8)
                .Select(i =>
                    Work.WaitForStuffAsync(
                        asyncSample.ProgressBar,
                        i,
                        asyncSample.CancelTokenSrc.Token))
        );

        asyncSample.RunButton.Visible = true;
        asyncSample.CancelButton.Visible = false;
        asyncSample.Spinner.Stop();
        asyncSample.WorkRunning = false;
    };

    asyncSample.CancelButton.OnClicked += (button, args) =>
    {
        if (!asyncSample.WorkRunning)
            return;

        // Cancel running async task using cancellation token
        asyncSample.CancelTokenSrc?.Cancel();

        asyncSample.CancelButton.Visible = false;
        asyncSample.RunButton.Visible = true;
        asyncSample.Spinner.Stop();
        asyncSample.WorkRunning = false;
    };

    /*
     * Sample Part 2
     * 
     * 'OnClicked' event handler that manually invokes the UI thread for changes inside asynchronous tasks.
     * These tasks may run on a different thread and may not come from the UI originally.
     * The GirCore synchronization context is not being used for the work inside the task because new tasks use the default context instead.
     */
    tasksSample.RunButton.OnClicked += async (button, args) =>
    {
        if (tasksSample.WorkRunning)
            return;

        tasksSample.WorkRunning = true;
        tasksSample.CancelTokenSrc = new CancellationTokenSource();

        tasksSample.RunButton.Visible = false;
        tasksSample.CancelButton.Visible = true;
        tasksSample.ProgressBar.Visible = true;
        tasksSample.Spinner.Start();

        /*
         * Do some heavy work in a task without using await.
         * Run multiple tasks at the same time to force multiple threads queueing UI actions.
         */
        await Task.Run(() =>
        {
            Parallel.For(0, 8, i =>
            {
                Work.DoHeavyWorkWithInvoke(tasksSample.ProgressBar, i, tasksSample.CancelTokenSrc.Token);
            });
        });

        tasksSample.RunButton.Visible = true;
        tasksSample.CancelButton.Visible = false;
        tasksSample.Spinner.Stop();
        tasksSample.WorkRunning = false;
    };

    tasksSample.CancelButton.OnClicked += (button, args) =>
    {
        if (!tasksSample.WorkRunning)
            return;

        // Cancel running task using cancellation token
        tasksSample.CancelTokenSrc?.Cancel();

        tasksSample.CancelButton.Visible = false;
        tasksSample.RunButton.Visible = true;
        tasksSample.Spinner.Stop();
        tasksSample.WorkRunning = false;
    };

    window.Child = hbox;
    window.Show();
};

// Running with the GirCore synchronization context is required for this sample
return application.RunWithSynchronizationContext(null);

// Create a box containing the required widgets for each sample and return them
static SampleBox SetupSampleBox(string text)
{
    var vbox = Gtk.Box.New(Gtk.Orientation.Vertical, 5);

    var runButton = Gtk.Button.NewWithLabel($"Run {text}");
    vbox.Append(runButton);

    var cancelButton = Gtk.Button.NewWithLabel($"Cancel {text}");
    cancelButton.Visible = false;
    vbox.Append(cancelButton);

    var progressBar = Gtk.ProgressBar.New();
    progressBar.Visible = false;
    progressBar.ShowText = true;
    vbox.Append(progressBar);

    var spinner = Gtk.Spinner.New();
    spinner.Vexpand = true;
    spinner.Hexpand = true;
    vbox.Append(spinner);

    return new SampleBox()
    {
        Box = vbox,
        RunButton = runButton,
        CancelButton = cancelButton,
        ProgressBar = progressBar,
        Spinner = spinner
    };
}

internal class SampleBox
{
    public required Gtk.Box Box;
    public required Gtk.Button RunButton;
    public required Gtk.Button CancelButton;
    public required Gtk.ProgressBar ProgressBar;
    public required Gtk.Spinner Spinner;

    public bool WorkRunning;
    public CancellationTokenSource? CancelTokenSrc;
}
