using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scabble_JOUATEL
{
    class Joueur
    {
        private string nom;
        private int score;
        private List<string> motsTrouvés;
        private List<Jetons> main;
        bool estIA;


        public bool EstIA
        {
            get { return this.estIA; }
        }
        public string Nom
        {
            get { return this.nom; }
        }
        public int Score
        {
            get { return this.score; }
        }
        public List<string> MotsTrouvés
        {
            get { return this.motsTrouvés; }
        }
        public List<Jetons> Main
        {
            get { return this.main; }
            set { this.main = value; }
        }

        public Joueur(string nom)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouvés = new List<string>();
            this.main = new List<Jetons>();
            this.estIA = false;
        }
        public Joueur(string nom, bool estIA)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouvés = new List<string>();
            this.main = new List<Jetons>();
            this.estIA = estIA;
        }
        public Joueur(string nom, int score, List<string> motsTrouvés, List<Jetons> main, bool estIA)
        {
            this.nom = nom;
            this.score = score;
            this.motsTrouvés = motsTrouvés;
            this.main = main;
            this.estIA = estIA;
        }


        public void Add_Main_Courante(Jetons monjeton)
        {
            this.main.Add(monjeton);
        }

        public void Add_Score(int val)
        {
            this.score += val;
        }

        public void Add_Mot(string mot)
        {
            this.motsTrouvés.Add(mot);
        }

        public void Remove_Main_Courante(Jetons monjeton)
        {
            this.main.Remove(monjeton);
        }

        public override string ToString()
        {
            string leToString = "";
            leToString += nom + " a un score de 0";
            if (this.estIA)
            {
                leToString += " (C'est une IA)";
            }
            leToString += '\n';
            leToString += "Contenu de sa main : ";

            for (int i = 0; i < this.main.Count; i++)
            {
                leToString += this.main[i].Lettre + ", ";
            }

            return leToString;
        }
    }
}
