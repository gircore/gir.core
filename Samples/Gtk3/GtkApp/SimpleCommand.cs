using System;
using System.Windows.Input;

namespace GtkApp
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private bool canExecute;
        private readonly Action<object> callback;

        public SimpleCommand(Action<object> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public bool CanExecute(object? parameter) => canExecute;
        public void SetCanExecute(bool canExecute)
        {
            this.canExecute = canExecute;
            OnCanExecuteChanged();
        }

        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public void Execute(object parameter) => callback(parameter);
    }
}
