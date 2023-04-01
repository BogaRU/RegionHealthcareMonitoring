using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmazovaProject
{
    internal class RegionInfo
    {
        public static string[] RegionIds { get; } = new string[45];
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public List<DateTime> Date { get; internal set; } = new List<DateTime>();
        public List<long?> Population { get; internal set; } = new List<long?>();
        public List<int> TltPrehospital { get; internal set; } = new List<int>();
        public List<int> SteAcs { get; internal set; } = new List<int>();
        public List<int> NsteAcs { get; internal set; } = new List<int>();
        public List<double> PciAcs { get; internal set; } = new List<double>();
        public List<double> PciSteAcs { get; internal set; } = new List<double>();
        public List<double> PciNsteAcs { get; internal set; } = new List<double>();
        public List<double> PciNsteAcsHighRisk { get; internal set; } = new List<double>();
        public List<int> PciSum { get; internal set; } = new List<int>();
        public List<int> ElectivePci { get; internal set; } = new List<int>();
        public List<double> FatalityRsc { get; internal set; } = new List<double>();
        public List<double> FatalityPso { get; internal set; } = new List<double>();
        public List<double> FatalityNonProper { get; internal set; } = new List<double>();
        public List<double> TltPharmProp { get; internal set; } = new List<double>();
        public List<double> TwoHours { get; internal set; } = new List<double>();
        public List<double> TwelveHours { get; internal set; } = new List<double>();
        public List<int> AtHome { get; internal set; } = new List<int>();
        public List<double> FatalityFirstDay { get; internal set; } = new List<double>();
    }
}
