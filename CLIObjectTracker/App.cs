using System;
using System.Collections.Generic;
using System.Text;
using CLIObjectTracker.Services;

namespace CLIObjectTracker
{
    internal class App
    {
        private readonly TLEService _tleService;
        public App()
        {
            _tleService = new TLEService();
        }

        public async Task RunAsync()
        {
            Console.Write("Please enter the spacecraft name:");
            var name = Console.ReadLine();

            var tle = await _tleService.GetTLEAsync(name!);

            if (tle.Length < 0)
            {
                Console.WriteLine("No TLE Data found.");
                return;
            }

            Console.WriteLine("\nTLE Data:");
            foreach (var line in tle)
                Console.WriteLine(line);
        }
    }
}
