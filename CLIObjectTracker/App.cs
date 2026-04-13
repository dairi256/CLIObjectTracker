using System;
using System.Collections.Generic;
using System.Text;
using CLIObjectTracker.Services;

namespace CLIObjectTracker
{
    internal class App
    {
        private readonly TLEService _tleService;
        private UIService _ui; // So we can access the UI class in this file
        public App()
        {
            _tleService = new TLEService();
            _ui = new UIService();
        }

        public async Task RunAsync()
        {
            Console.Write("Please enter the spacecraft name:");
            var name = Console.ReadLine();

            var tle = await _tleService.GetTLEAsync(name!);

            if (tle.Length < 3)
            {
                Console.WriteLine("No TLE Data found.");
                return;
            }

            Console.WriteLine("\nTLE Data:");
            foreach (var line in tle)
                Console.WriteLine(line);
        }

        private (string Name, string Epoch, TimeSpan Age) ParseTLEInfo(string[] tle)
        {
            string name = tle[0];
            string line1 = tle[1];

            string epochRaw = line1.Substring(18, 24);

            int year = int.Parse(epochRaw.Substring(0, 2));
            int dayOfYear = int.Parse(epochRaw.Substring(2, 3));
            double fractionalDay = double.Parse(epochRaw.Substring(5), System.Globalization.CultureInfo.InvariantCulture);

            year += (year < 57) ? 2000 : 1900;

            DateTime epoch = new DateTime(year, 1, 1).AddDays(dayOfYear - 1)
                .AddDays(fractionalDay);

            TimeSpan age = DateTime.UtcNow - epoch;

            return (name, epoch.ToString("yyyy-MM-dd HH:mm:ss"), age);
        }
    }
}
