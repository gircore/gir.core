namespace Samples
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Sample.DBus.ShowApplicationsAsync();
            Sample.DBus.SendNotification();
        }
    }
}
