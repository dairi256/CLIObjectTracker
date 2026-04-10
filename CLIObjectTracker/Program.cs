using CLIObjectTracker.Services;

namespace CLIObjectTracker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var app = new App();
            await app.RunAsync();
        }
    }
}
