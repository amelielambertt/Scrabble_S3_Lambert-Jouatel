using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scabble_JOUATEL
{
    class Sac_Jetons
    {
        private Stack<Jetons> Pioche; // Création de la pioche sous forme de pile

        public Stack<Jetons> pioche
        {
            get { return this.Pioche; } // accès publique pour faciliter l'enregistrement
        }
        public Sac_Jetons() // Constructeur à partir du fichier vierge
        {
            string fullpath = Path.GetFullPath("Jetons.txt"); // obtention du chemin complet
            List<string> toutesLesLignes = new List<string>(); // Création de la liste contenant toutes les lignes du fichier "Jetons.txt"
            try // Tentative d'ouverture
            {
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line;
                    // Lire les lignes du fichier jusqu'à la fin.
                    while ((line = sr.ReadLine()) != null)
                    {
                        toutesLesLignes.Add(line); // Remplir la liste
                    }
                }
            }
            catch (Exception e) // Si le fichier n'a pas pu être ouvert, afficher le message d'erreur
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }

            List<Jetons> tousLesJetons = new List<Jetons>(); // Créer la liste contenant tous les jetons triés
            foreach(string ligne in toutesLesLignes) // Pour chaque ligne lue dans le fichier
            {
                string[] separation = ligne.Split(';', 3);  // On crée un tableau de string contenant les informations des jetons
                for(int i = 0; i < Convert.ToInt32(separation[2]); i++) // Pour tous les jetons d'un type particulier (nombre de jetons récupéré en fichier
                {
                    Jetons enCours = new Jetons(Convert.ToChar(separation[0]), Convert.ToInt32(separation[1])); // créer tous les jetons d'un même type en leur donnant leur valeur et leur lettre
                    tousLesJetons.Add(enCours); // ajouter le jeton nouvellement créé dans la liste des jetons
                }
            }

            Random pick = new Random(); // Créer une instance aléatoire
            int pioché;
            Stack<Jetons> piocheFinale = new Stack<Jetons>(); // Créer la pioche finale sous forme de pile
            int borne = tousLesJetons.Count; //Initialisation de la borne de génération du random commme le nombre total de jetons créés
            for (int i = 0; i < borne; i++)
            {
                pioché = pick.Next(tousLesJetons.Count); // tirer un jeton aléatoirement
                piocheFinale.Push(tousLesJetons[pioché]); // L'ajouter à la pile
                tousLesJetons.RemoveAt(pioché); // le retirer de la liste (pour qu'il ne soit pas tiré deux fois)
            }
            this.Pioche = piocheFinale; // La pioche créée est donc une pile déjà mélangée et il suffira de prendre le jeton au dessus de la pile pour tirer aléatoirement
        }
        public Sac_Jetons(string sauvegarde) // Constructeur à partir de la sauvegarde
        {
            Stack<Jetons> piocheFinale = new Stack<Jetons>();
            string fullpath = Path.GetFullPath(sauvegarde + "-Jetons.txt");
            string[] tousLesCaracteres = null;
            string[] toutesLesValeurs = null;
            try
            {
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line = sr.ReadLine();
                    tousLesCaracteres = line.Split(';');
                    line = sr.ReadLine();
                    toutesLesValeurs = line.Split(';');
                } // on crée depuis la lecture du fichier de sauvegarde
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
            for(int i = 0; i < tousLesCaracteres.Length; i++)
            {
                Jetons Letitsnow = new Jetons(Convert.ToChar(tousLesCaracteres[i]), Convert.ToInt32(toutesLesValeurs[i])); // On recrée tous les jetons dans l'ordre où ils ont été lus pour ne pas changer l'aléatoire
                piocheFinale.Push(Letitsnow); // on recrée la pile
            }
            this.Pioche = piocheFinale;
        }

        public Jetons retire_Jeton()
        {
            return this.Pioche.Pop(); // retirer un jeton aléatoirement revient à retirer le premier jeton puisque la pile est déjà mélangée
        }

        public override string ToString()
        {
            string leToString = "";
            foreach(Jetons élément in this.Pioche)
            {
                leToString += élément.ToString() + '\n';
            }
            return leToString;
        }
    }
}
