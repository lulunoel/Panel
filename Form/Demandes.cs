using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public class Demandes
    {
        public string Id
        { get; set; }

        public string Type
        { get; set; }

        public string PseudoJoueur
        { get; set; }

        public string Raison
        { get; set; }

        public string Preuve
        { get; set; }
        
        public string nomFichier
        { get; set; }

        public string PseudoStaff
        { get; set; }

        public string Datedederniéreédition
        { get; set; }

        public string Statut
        { get; set; }

        public string WarnComplet
        {
            get { return "Id: " + Id + " | Type: " + Type + " | Pseudo: " + PseudoJoueur + " | " + "Raison: " + Raison + " | " + "Par: " + PseudoStaff  + " | " + "Du: " + Datedederniéreédition + " | " + " -->" + Statut; }
        }

        public override string ToString()
        {
            return WarnComplet;
        }
    }
}
