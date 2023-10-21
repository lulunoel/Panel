using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public class Rembourse1
    {
        public string Id
        { get; set; }

        public string PseudoJoueur
        { get; set; }

        public string RaisonWarn
        { get; set; }

        public string PseudoStaff
        { get; set; }

        public string Datedederniéreédition
        { get; set; }

        public string Statue
        { get; set; }

        public string Complet
        {
            get { return "Id: " + Id + " | Pseudo: " + PseudoJoueur + " | " + "Info: " + RaisonWarn + " | " + "Par: " + PseudoStaff  + " | " + "Du: " + Datedederniéreédition + " | " + " -->" + Statue; }
        }

        public override string ToString()
        {
            return Complet;
        }
    }
}
