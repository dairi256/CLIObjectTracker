using System.Runtime.CompilerServices;

namespace DSNSpaceTracker.OrbitEngine
{
    public class TLEngine
    {
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        public double Inclination { get; set; }
        public double RAAN { get; set; }
        public double Eccetricity { get; set; }
        public double ArgumentOfPiregee { get; set; }
        public double MeanAnomaly { get; set; }
        public double MeanMotion { get; set; }
        public double BStar { get; set; }
        public DateTime Epoch { get; set; }

        public TLEngine(string name, string line1, string line2)
        {
            Name = name;
            Line1 = line1;
            Line2 = line2;

            Inclination = double.Parse(line2.Substring(8, 8));
            RAAN = double.Parse(line2.Substring(17, 8));
            Eccetricity = double.Parse("0." + line2.Substring(34, 8));
            MeanAnomaly = double.Parse(line2.Substring(43, 8));
            MeanMotion = double.Parse(line2.Substring(52, 11));

            BStar = ParseScientific(line1.Substring(53, 6), line1.Substring(59, 2));

            Epoch = ParseEpoch(line1.Substring(18, 14));
        }

        private double ParseScientific(string mantissa, string exponent)
        {
            return double.Parse($"{mantissa}e{exponent}");
        }

        private DateTime ParseEpoch(string epochString)
        {
            int year = int.Parse(epochString.Substring(0, 2));
            double dayOfYear = double.Parse(epochString.Substring(2));

            year += (year < 57) ? 2000 : 1900;

            return new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
        }

    }
}

