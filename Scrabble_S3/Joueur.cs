using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scabble_JOUATEL
{
    class Joueur
    {
        private string nom; //declaration des champs (variables)
        private int score;
        private List<string> motsTrouvés;
        private List<Jetons> main;
        bool estIA;


        public bool EstIA. //propriétés - définition des accès en consultation
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

        public Joueur(string nom) //constructeur qui défini le joueur (au début du jeu)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouvés = new List<string>();
            this.main = new List<Jetons>();
            this.estIA = false;
        }
        public Joueur(string nom, bool estIA) //constructeur qui défini le joueur (au début du jeu)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouvés = new List<string>();
            this.main = new List<Jetons>();
            this.estIA = estIA;
        }
        public Joueur(string nom, int score, List<string> motsTrouvés, List<Jetons> main, bool estIA) //constructeur pour partie en cours
        {
            this.nom = nom;
            this.score = score;
            this.motsTrouvés = motsTrouvés;
            this.main = main;
            this.estIA = estIA;
        }


        public void Add_Main_Courante(Jetons monjeton) //ajoute un jeton dans la main courante
        {
            this.main.Add(monjeton);
        }

        public void Add_Score(int val) //ajoute une valeur au score
        {
            this.score += val;
        }

        public void Add_Mot(string mot) //ajoute un mot dans l'historique des mots trouvés
        {
            this.motsTrouvés.Add(mot);
        }

        public void Remove_Main_Courante(Jetons monjeton) //retire un jeton de la main courante
        {
            this.main.Remove(monjeton);
        }

        public override string ToString() //retourne une chaine de caractère qui décrit le joueur
        {
            string leToString = "";
            leToString += nom + " a un score de 0";  //nom et score
            if (this.estIA)
            {
                leToString += " (C'est une IA)";
            }
            leToString += '\n';
            leToString += "Contenu de sa main : ";

            for (int i = 0; i < this.main.Count; i++) //jetons de la main
            {
                leToString += this.main[i].Lettre + ", ";
            }

            return leToString;
        }
    }
}
