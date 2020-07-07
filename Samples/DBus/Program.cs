using System;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample.DBus.ShowApplicationsAsync();
            Sample.DBus.SendNotification();
        }
    }
}
