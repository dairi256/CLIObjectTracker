using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace CLIObjectTracker.Services
{
    public class TLEService
    {
        private readonly HttpClient _http;

        private const string CelestrakUrl = "https://celestrak.org/NORAD/elements/gp.php?NAME={0}&FORMAT=TLE"; // This will be the main URL to fetch TLE data

        public TLEService()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64 x64) CLIObjectTracker/1.0"
            );

            _http.DefaultRequestHeaders.Accept.ParseAdd("text/plain");
            _http.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");
        }

        public async Task<string[]> GetTLEAsync(string name)
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
