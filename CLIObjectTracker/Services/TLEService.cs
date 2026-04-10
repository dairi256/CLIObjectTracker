using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace CLIObjectTracker.Services
{
    public class TLEService
    {
        private readonly HttpClient _http = new();

        private const string CelestrakUrl = "https://celestrak.com/NORAD/elements/gp.php?GROUP=active&FORMAT=tle"; // This will be the main URL to fetch TLE data

        private async Task<string[]> GetTLEAsync(string name)
        {
            try
            {
                var url = string.Format(CelestrakUrl, Uri.EscapeDataString(name));
                var response = await _http.GetStringAsync(url);

                var lines = response
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l  => l.Trim())
                    .ToArray();

                if (lines.Length < 3)
                    throw new Exception("TLE Data incomplete or not found.");

                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TLE ERROR] {ex.Message}");
                return Array.Empty<string>(); // Fallback in case if the response doesn't work
            }
        }
    }
}
