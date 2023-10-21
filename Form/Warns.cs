using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public class warns
    {
        public string Id
        { get; set; }

        public string Type
        { get; set; }

        public string PseudoJoueur
        { get; set; }

        public string RaisonWarn
        { get; set; }

        public string PseudoStaff
        { get; set; }

        public string Datedederniereedition
        { get; set; }

        public string Datedefin
        { get; set; }

        public string Statue
        { get; set; }

        public string WarnComplet
        {
            get { return "Id: " + Id + " | Pseudo: " + PseudoJoueur + " | " + "Raison: " + RaisonWarn + " | " + "Par: " + PseudoStaff  + " | " + "Du: " + Datedederniereedition + " | " + "Au: "+ Datedefin + " -->" + Statue; }
        }

        public override string ToString()
        {
            return WarnComplet;
        }
    }
}
