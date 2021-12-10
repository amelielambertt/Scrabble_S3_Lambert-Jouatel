using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scabble_JOUATEL
{
    class Jetons
    {
        private char lettre;
        private int valeur;

        public char Lettre
        {
            get { return this.lettre; }
        }

        public int Valeur
        {
            get { return this.valeur; }
        }

        public Jetons(char lettre, int valeur)
        {
            this.lettre = lettre;
            this.valeur = valeur;
        }

        public override string ToString()
        {
            string leToString = "";
            leToString += this.lettre + " (" + this.valeur + ")";
            return leToString;
        }
    }
}
