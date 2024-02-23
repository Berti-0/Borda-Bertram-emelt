using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borda_Bertram_emelt
{
    internal class Tabor
    {
        public int honap1 { get; set; }
        public int nap1 { get; set; }
        public int honap2 { get; set; }
        public int nap2 { get; set; }
        public string diak { get; set; }
        public string tema { get; set; }

        public Tabor(string sor)
        {
            var v = sor.Split('\t');

            honap1 = int.Parse(v[0]);
            nap1 = int.Parse(v[1]);
            honap2 = int.Parse(v[2]);
            nap2 = int.Parse(v[3]);
            diak = v[4];
            tema = v[5];
        }
    }
}
