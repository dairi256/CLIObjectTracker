using System;
using System.Collections.Generic;
using System.Text;

namespace CLIObjectTracker.Services
{
    public class UIService
    {
        private const int MaxWidth = 80;
        
        public void ClearScreen()
        {
            Console.Clear();
        }

        private string CenterText(string text)
        {
            if (text.Length > MaxWidth)
                return text;

            int padding = (MaxWidth - text.Length) / 2;
            return new string(' ', padding) + text;
        }

        private string PadLine(string text)
        {
            if (text.Length > MaxWidth - 2)
                text = text.Substring(0, MaxWidth - 2);

            return "| " + text.PadRight(MaxWidth - 3) + "|";
        }

        private void DrawBoxHeader(string title)
        {
            Console.WriteLine("┌" + new string('─', MaxWidth - 2) + "┐");
            Console.WriteLine(PadLine(title));
            Console.WriteLine("├" + new string('─', MaxWidth - 2) + "┤");

        }

        private void DrawBoxFooter()
        {
            Console.WriteLine("└" + new string('─', MaxWidth - 2) + "┘");
        }

        private void RenderDashboard(string title, string[] lines)
        {
            ClearScreen();
            DrawBoxHeader(title);

            foreach (var line in lines)
                Console.WriteLine(PadLine(line));

            DrawBoxFooter();
            Console.WriteLine(CenterText("Press CTRL+C to stop tracking this item."));
        }
    }
}
