namespace BoxLayout
{
    // Define a class named BoxLayout that extends Gtk.Window.
    // Gtk.Window represents a top-level window in a GTK application.
    public class BoxLayout : Gtk.Window
    {
        // Constructor for the BoxLayout class.
        // This method sets up the window and its contents.
        // Gtk.Application is an passed in so we can access the application in this app
        // we will use the reference to the Gtk.Application to quit from a button.
        public BoxLayout(Gtk.Application app)
        {
            Title = "Box Layout App";
            SetDefaultSize(300, 30);

            var helloButton = Gtk.Button.New();
            helloButton.Label = "Say Hello"; // Same as helloButton.SetLabel("Say Hello")
            helloButton.SetMarginStart(10);  // Left margin
            helloButton.SetMarginEnd(10);    // Right margin
            helloButton.SetMarginTop(10);    // Top margin
            helloButton.SetMarginBottom(10); // Bottom margin

            // Create a Gtk.Button widget and set the label when it is created
            var goodbyeButton = Gtk.Button.NewWithLabel("Say Goodbye");
            goodbyeButton.SetMarginStart(10);  // Left margin
            goodbyeButton.SetMarginEnd(10);    // Right margin
            goodbyeButton.SetMarginTop(10);    // Top margin
            goodbyeButton.SetMarginBottom(10); // Bottom margin

            // A Gtk.Button can only display a Label or a Icon but not both together
            // Gtk.Button with label: var button = Gtk.Button.NewWithLabel("Label");
            // Gtk.Button with icon: var button = Gtk.Button.NewFromIconName("icon-name");
            // To have multiple widgets inside a Gtk.Button you can set the child of the
            // button to be a Gtk.Box as shown below.
            var quitButton = Gtk.Button.New();
            // Create a Gtk.Box that can hold multiple widgets. Orientation is set
            // to horizontal which will display widgets side by side.
            // there is a 10px gap between each new widget in the box.
            var quitBox = Gtk.Box.New(Gtk.Orientation.Horizontal, 10);
            // Create a label that allows user to press Alt+Q to act as button press.
            var quitLabel = Gtk.Label.NewWithMnemonic("_Quit");
            // Icon Names come from https://specifications.freedesktop.org/icon-naming-spec/latest/
            // The theme needs to have the icon. Gtk Apps by default use the Adwaita theme from Gnome
            var quitIcon = Gtk.Image.NewFromIconName("application-exit");
            // This will add the quitIcon and quitLabel widgets into the quitBox.
            quitBox.Append(quitIcon);
            quitBox.Append(quitLabel);
            // Sets the quitBox as the child of the quitButton so that the contents
            // of the quitBox will display inside the button.
            quitButton.SetChild(quitBox);
            quitButton.SetMarginStart(10);  // Left margin
            quitButton.SetMarginEnd(10);    // Right margin
            quitButton.SetMarginTop(10);    // Top margin
            quitButton.SetMarginBottom(10); // Bottom margin

            // Create a Gtk.Label with a blank label
            var sayLabel = Gtk.Label.New("");
            sayLabel.SetMarginStart(10);  // Left margin
            sayLabel.SetMarginEnd(10);    // Right margin
            sayLabel.SetMarginTop(10);    // Top margin
            sayLabel.SetMarginBottom(10); // Bottom margin

            // Create a Gtk.Entry and then set placeholder text telling the user
            // what they need to do.
            var nameEntry = Gtk.Entry.New();
            nameEntry.PlaceholderText = "Enter your name";
            nameEntry.SetMarginStart(10);  // Left margin
            nameEntry.SetMarginEnd(10);    // Right margin
            nameEntry.SetMarginTop(10);    // Top margin
            nameEntry.SetMarginBottom(10); // Bottom margin

            // Creates a Gtk.Box widget with a vertical orientation.
            // This will allow you to add multiple widgets stacked vertically
            // with each new appended widget stacking below the widget above it.
            var vBox = Gtk.Box.New(Gtk.Orientation.Vertical, 0);

            // Creates a Gtk.Box widget with a horizontal orientation.
            // This will allow you to add multiple widgets stacked horizontally
            // with each new appended widget stacking to the right of the widget 
            // beside it.
            var hBox = Gtk.Box.New(Gtk.Orientation.Horizontal, 0);

            // Creates a Gtk.Box widget with a vertical orientation.
            // Then the SetVexpand is set to true so the widget takes up all the
            // available vertical space between the widgets above it and below it.
            // This is useful to make the widgets below the spacer be at the bottom
            // of the window even when it is resized to be larger or smaller.
            var vSpacer = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
            vSpacer.SetVexpand(true);

            // Creates a Gtk.Box widget with a horizontal orientation.
            // Then the SetHexpand is set to true so the widget takes up all the
            // available horizontal space between the widgets to the left and 
            // the widgets to the right of it.
            // This is useful to make the widgets to the right of the spacer be 
            // at the right of the window even when it is resized to be larger 
            // or smaller.
            var hSpacer = Gtk.Box.New(Gtk.Orientation.Horizontal, 0);
            hSpacer.SetHexpand(true);

            // This will add the widgets to the vBox Gtk.Box widget with each new
            // append adding the new widget below the one above it.
            vBox.Append(sayLabel);
            vBox.Append(nameEntry);
            // The vSpacer is used to create a gap between the widgets above and
            // below the vSpacer. It will force the widgets in the hBox to be at
            // the bottom of the window.
            vBox.Append(vSpacer);
            vBox.Append(hBox);

            // This will add the widgets to the hBox Gtk.Box widget with each new
            // append adding the new widget to the right of the one beside it.
            hBox.Append(helloButton);
            hBox.Append(goodbyeButton);
            // the hSpacer is used to create a gap between the widgets to the left
            // and the widgets to the right of the hSpacer. It will force the 
            // quitButton to be at the right of the window.
            hBox.Append(hSpacer);
            hBox.Append(quitButton);

            // This sets the child of the window to be the vBox Gtk.Box widget.
            // This will show all the widgets in the vBox in the window.
            Child = vBox;

            helloButton.OnClicked += (_, _) =>
            {
                // Sets the label to say Hello and then the value the user typed 
                // into the nameEntry widget.
                sayLabel.SetLabel($"Hello {nameEntry.GetText()}");
            };

            goodbyeButton.OnClicked += (_, _) =>
            {
                // Sets the label to say Goodbye and then the value the user typed 
                // into the nameEntry widget.
                sayLabel.SetLabel($"Goodbye {nameEntry.GetText()}");
            };

            quitButton.OnClicked += (_, _) =>
            {
                app.Quit();
            };
        }
    }
}

