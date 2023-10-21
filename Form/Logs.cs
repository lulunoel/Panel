using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public class logs
    {
        public string Id
        { get; set; }

        public string Pseudo
        { get; set; }

        public string Action
        { get; set; }

        public string Date
        { get; set; }

        public string LogComplet
        {
            get { return Id + " | Pseudo: " + Pseudo + " | " + "Raison: " + Action + " | " + "Par: " + Date; }
        }

        public override string ToString()
        {
            return LogComplet;
        }
    }
}
