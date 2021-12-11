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

        public static int trouverLaValeur(char lettre)
        {
            int valeur = 1;
            switch (lettre)
            {
                case '*':
                    valeur = 0;
                    break;
                case 'B':
                    valeur = 3;
                    break;
                case 'C':
                    valeur = 3;
                    break;
                case 'D':
                    valeur = 2;
                    break;
                case 'F':
                    valeur = 4;
                    break;
                case 'G':
                    valeur = 2;
                    break;
                case 'H':
                    valeur = 4;
                    break;
                case 'J':
                    valeur = 8;
                    break;
                case 'K':
                    valeur = 10;
                    break;
                case 'M':
                    valeur = 2;
                    break;
                case 'P':
                    valeur = 3;
                    break;
                case 'Q':
                    valeur = 8;
                    break;
                case 'V':
                    valeur = 4;
                    break;
                case 'W':
                    valeur = 10;
                    break;
                case 'X':
                    valeur = 10;
                    break;
                case 'Y':
                    valeur = 10;
                    break;
                case 'Z':
                    valeur = 10;
                    break;
                default:
                    break;


            }
            return valeur;
        }
    }
}
