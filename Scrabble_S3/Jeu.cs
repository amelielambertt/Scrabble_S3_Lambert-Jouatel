using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Scabble_JOUATEL
{
    class Jeu
    {
        Dictionnaire MonDico;
        Plateau MonPlateau;
        Sac_Jetons MonSacDeJetons;
        List<Joueur> ListeDesJoueurs;


        public Jeu()
        {
            this.MonDico = new Dictionnaire();
            this.MonPlateau = new Plateau();
            Sac_Jetons Partie1 = new Sac_Jetons();
            this.MonSacDeJetons = Partie1;

            int[] joueurs = Jeu.setupJoueurs(); // Grâce à ce nouveau menu, on connait la nature des quatre joueurs (on a forcé l'existance du premier joueur)
            List<Joueur> listeDesJoueurs = new List<Joueur>();
            for (int i = 0; i < joueurs.Length; i++)
            {
                string nom;
                if (joueurs[i] == 1)
                {
                    Console.WriteLine("Entrez le nom du Joueur " + (i + 1));
                    nom = Console.ReadLine();
                    Joueur JoueurSCABBLE = new Joueur(nom);
                    listeDesJoueurs.Add(JoueurSCABBLE);
                }
                if (joueurs[i] == 2)
                {
                    string fullpath = Path.GetFullPath("NomsIA.txt");
                    string line = "";
                    try
                    {
                        //Créez une instance de StreamReader pour lire à partir d'un fichier
                        using (StreamReader sr = new StreamReader(fullpath))
                        {
                            line = sr.ReadLine();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Le fichier n'a pas pu être lu.");
                        Console.WriteLine(e.Message);
                    }
                    string[] nomsIA = line.Split(';', 33);
                    Random nomsRandom = new Random();
                    int nombreTiré = nomsRandom.Next(33);
                    nom = nomsIA[nombreTiré];
                    Joueur IASCRABBLE = new Joueur(nom, true);
                    listeDesJoueurs.Add(IASCRABBLE);
                }
            }

            /* affiche le sac de jetons pour les tests
            Console.WriteLine(Partie1.ToString());
            Console.WriteLine();
            Console.WriteLine();
            */

            foreach (Joueur participant in listeDesJoueurs)
            {
                for (int i = 0; i < 7; i++)  //Remplir la main de tous les joueurs avec 7 jetons puis les retirer du sac
                {
                    participant.Add_Main_Courante(Partie1.retire_Jeton());
                }
                // Console.WriteLine(participant.ToString()); Afficher les participants pour les tests
            }
            // So far tous les joueurs sont créés et ils ont une main complète.

            /* affiche le sac de jetons pour les tests
            Console.WriteLine(Partie1.ToString());
            Console.WriteLine();
            Console.WriteLine();
            */


            // a faire now : Commencer la partie, afficher les joueurs qui jouent, sélectionner les lettres, les poser, sûrement dans une fonction "JEU"

            this.ListeDesJoueurs = listeDesJoueurs;
        }

        public Jeu(Plateau MonPlateau, Sac_Jetons MonSacDeJetons, List<Joueur> listeDesJoueurs)
        {
            this.MonDico = new Dictionnaire();
            this.MonSacDeJetons = MonSacDeJetons;
            this.MonPlateau = MonPlateau;
            this.ListeDesJoueurs = listeDesJoueurs;
        }

        #region Alliés du constructeur
        static int[] setupJoueurs()
        {
            int optionSelectionnée = 0;
            int[] natureDesOptions = new int[] { 1, 0, 0, 0 };

            int confirmation = -1;

            ConsoleKeyInfo cki; //déclare une variable de type ConsoleKeyInfo


            while (confirmation == -1) // Tant qu'aucune option n'est confirmée
            {

                Console.Clear();
                Jeu.affichageMenu901IQ(natureDesOptions, optionSelectionnée, "Joueurs :");    // Afficher le menu
                cki = Console.ReadKey(); // cki contient entre autres le code de la

                switch (cki.Key)
                {
                    case ConsoleKey.DownArrow:
                        optionSelectionnée++; // Bouger le curseur vers le bas
                        if (optionSelectionnée >= 4)
                        {
                            optionSelectionnée = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        optionSelectionnée--; // Bouger le curseur vers le bas
                        if (optionSelectionnée < 0)
                        {
                            optionSelectionnée = 3;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        natureDesOptions[optionSelectionnée]++; // Bouger le curseur vers la gauche
                        if (natureDesOptions[optionSelectionnée] >= 3)
                        {
                            natureDesOptions[optionSelectionnée] = 0;
                            if (optionSelectionnée == 0)
                            {
                                natureDesOptions[optionSelectionnée] = 1;
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        natureDesOptions[optionSelectionnée]--; // Bouger le curseur vers la droite
                        if (natureDesOptions[optionSelectionnée] < 1)
                        {
                            natureDesOptions[optionSelectionnée] = 2;
                        }
                        break;
                    default:
                        confirmation = 0; // Confirmer la mise en place
                        break;
                }
            }
            return natureDesOptions;
        }

        static void affichageMenu901IQ(int[] options, int surbrillance, string bienvenue)
        {
            Console.WriteLine(bienvenue);
            Console.WriteLine();
            Console.WriteLine("Veuillez sélectionner une option :");
            Console.WriteLine("(Pour confirmer, appuyez sur echapp ou entrée)");
            Console.WriteLine();

            for (int i = 1; i <= options.Length; i++)
            {
                if (surbrillance + 1 == i) // Affichage de toutes les options avec l'option sélectionnée en noir sur fond blanc
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    switch (options[i - 1])
                    {
                        case 0:
                            Console.WriteLine(i + ": Ajouter un joueur " + '\t' + "<>");
                            break;
                        case 1:
                            Console.WriteLine(i + ": Joueur" + i + '\t' + '\t' + "<>");
                            break;
                        case 2:
                            Console.WriteLine(i + ": IA" + '\t' + '\t' + '\t' + "<>");
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    switch (options[i - 1])
                    {
                        case 0:
                            Console.WriteLine(i + ": Ajouter un joueur " + '\t' + "<>");
                            break;
                        case 1:
                            Console.WriteLine(i + ": Joueur" + i + '\t' + '\t' + "<>");
                            break;
                        case 2:
                            Console.WriteLine(i + ": IA" + '\t' + '\t' + '\t' + "<>");
                            break;
                    }
                }
            }
            Console.WriteLine();
        }

        #endregion




        public void PartieEnCours(int compteurDeTour = 1, bool continuer = true)
        {
            Console.SetWindowSize(100, 30);
            Console.Clear();
            Console.WriteLine('\t' + "Tour : " + compteurDeTour);
            this.MonPlateau.AffichageOMG(this.ListeDesJoueurs);
            Thread.Sleep(1000);
            int continuerLaPartie = 0;
            do
            {
                foreach (Joueur joueur in this.ListeDesJoueurs)
                {
                    continuerLaPartie = this.TourDUnJoueur(joueur, compteurDeTour);
                }
                compteurDeTour++;
            } while (QuitterLaPartie(ListeDesJoueurs));

            // Boucler et faire quitter la partie
        }

        public int TourDUnJoueur(Joueur joueurJouant, int compteurDeTour)
        {
            Console.WriteLine();

            // Créer un plateau factice

            char[,] plateauCurseur = new char[15,15];
            char[,] plateauFactice = new char[15, 15];
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    plateauCurseur[i,j] = this.MonPlateau.Plato[i,j];
                    plateauFactice[i, j] = this.MonPlateau.Plato[i, j];
                }
            }

            List<Jetons> jetonsAJouer = new List<Jetons>();
            int indexactuel = -1;
            
            int confirmation = -1;
            int optionSelectionnée = 0;
            int curseurx = 7;
            int curseury = 7;
            bool SURLEPLATEAU = false;
            List<int> alignementReferencex = new List<int>();
            List<int> alignementReferencey = new List<int>();
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    char l = this.MonPlateau.Plato[i, j];
                    if (l != '3' && l!= '2' && l != '4' && l != '7' && l != '8' && l != '_')
                    {
                        alignementReferencey.Add(j);
                    }
                }
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    char l = this.MonPlateau.Plato[j, i];
                    if (l != '3' && l != '2' && l != '4' && l != '7' && l != '8' && l != '_')
                    {
                        alignementReferencex.Add(j);
                    }
                }
            }

            foreach(int index in alignementReferencex)
            {
                Console.Write(index + " ");
            }
            Console.WriteLine();
            foreach (int index in alignementReferencex)
            {
                Console.Write(index + " ");
            }
            // Console.ReadKey();
            ConsoleKeyInfo cki; //déclare une variable de type ConsoleKeyInfo
            while (confirmation == -1) // Tant qu'aucune option n'est confirmée
            {

                Console.Clear();
                Console.WriteLine("Tour de : " + joueurJouant.Nom);
                int laCouleurDeLaRéponseDeLaVie = this.MonPlateau.LaFonctionQuiMePerdra(plateauFactice, curseurx, curseury, alignementReferencex, alignementReferencey);
                Affichage902IQ(joueurJouant.Main, optionSelectionnée, compteurDeTour, plateauCurseur, plateauFactice, curseurx, curseury, SURLEPLATEAU, laCouleurDeLaRéponseDeLaVie, "Placez les lettres souhaitées sur le plateau"); // Afficher le menu

                cki = Console.ReadKey(); // cki contient entre autres le code de la

                if (SURLEPLATEAU)
                {
                    switch (cki.Key)
                    {
                        case ConsoleKey.RightArrow:
                            plateauCurseur[curseurx,curseury] = this.MonPlateau.Plato[curseurx,curseury];
                            curseury++; // Bouger le curseur vers la droite
                            if (curseury >= 15)
                                curseury = 0;
                            plateauCurseur[curseurx,curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                            break;
                        case ConsoleKey.LeftArrow:
                            plateauCurseur[curseurx,curseury] = this.MonPlateau.Plato[curseurx,curseury];
                            curseury--; // Bouger le curseur vers la gauche
                            if (curseury < 0)
                                curseury = 14;
                            plateauCurseur[curseurx,curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                            break;
                        case ConsoleKey.UpArrow:
                            plateauCurseur[curseurx,curseury] = this.MonPlateau.Plato[curseurx,curseury];
                            curseurx--; // Bouger le curseur vers la gauche
                            if (curseurx < 0)
                                curseurx = 14;
                            plateauCurseur[curseurx,curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                            break;
                        case ConsoleKey.DownArrow:
                            plateauCurseur[curseurx, curseury] = this.MonPlateau.Plato[curseurx, curseury];
                            curseurx++; // Bouger le curseur vers la droite
                            if (curseurx >= 15)
                                curseurx = 0;
                            plateauCurseur[curseurx,curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                            break;
                        case ConsoleKey.Spacebar:
                            if(laCouleurDeLaRéponseDeLaVie != 2)
                            {
                                plateauFactice[curseurx, curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                                joueurJouant.Main.RemoveAt(optionSelectionnée);
                                SURLEPLATEAU = false;
                                optionSelectionnée = 0;
                            }
                            break;
                        case ConsoleKey.Escape:
                            plateauCurseur[curseurx,curseury] = this.MonPlateau.Plato[curseurx,curseury];
                            plateauFactice[curseurx, curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                            jetonsAJouer.RemoveAt(jetonsAJouer.Count-1);
                            SURLEPLATEAU = false;
                            indexactuel += -1;
                            break;
                    }
                }
                else
                {
                    switch (cki.Key)
                    {
                        case ConsoleKey.RightArrow:
                            optionSelectionnée++; // Bouger le curseur vers la droite
                            if (optionSelectionnée >= joueurJouant.Main.Count)
                            {
                                optionSelectionnée = 0;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            optionSelectionnée--; // Bouger le curseur vers la gauche
                            if (optionSelectionnée < 0)
                            {
                                optionSelectionnée = joueurJouant.Main.Count - 1;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            break;
                        case ConsoleKey.DownArrow:
                            break;
                        case ConsoleKey.Spacebar:
                            jetonsAJouer.Add(joueurJouant.Main[optionSelectionnée]);
                            SURLEPLATEAU = true;
                            curseurx = 7;
                            curseury = 7;
                            plateauCurseur[curseurx, curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                            break;
                        case ConsoleKey.Escape:
                            for (int i = 0; i < 15; i++)
                            {
                                for (int j = 0; j < 15; j++)
                                {
                                    plateauCurseur[i, j] = this.MonPlateau.Plato[i, j];
                                    plateauFactice[i, j] = this.MonPlateau.Plato[i, j];
                                }
                            }
                            for (int i = jetonsAJouer.Count-1;  i >= 0; i--)
                            {
                                joueurJouant.Add_Main_Courante(jetonsAJouer[i]);
                                jetonsAJouer.RemoveAt(i);
                            }
                            indexactuel = -1;
                            break;
                        default:
                            // Valider le mot
                            break;
                    }
                }

                
            }
            return confirmation;
        }




        public void Affichage902IQ(List<Jetons> main, int surbrillance, int compteurDeTour, char[,] plateauCurseur, char[,] plateauFactice, int curseurx, int curseury, bool SURLEPLATEAU, int presqueBool, string bienvenue)
        {

            Console.WriteLine('\t' + "Tour : " + compteurDeTour);
            this.MonPlateau.AffichageOMGFactice(this.ListeDesJoueurs, plateauCurseur, plateauFactice, curseurx, curseury, SURLEPLATEAU, presqueBool);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(bienvenue);
            Console.WriteLine("Appuyez sur espace pour placer la lettre sur le plateau");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Appuyez sur echap pour annuler votre mot");
            Console.WriteLine("Appuyez sur suppr pour passez votre tour, et vous débarrasser des lettres que vous avez sélectionné.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine();

            for (int i = 1; i <= main.Count; i++)
            {
                Console.Write("|");
                if (surbrillance + 1 == i) // Affichage de toutes les options avec l'option sélectionnée en noir sur fond blanc
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" " + main[i - 1] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.Write(" " + main[i - 1] + " ");
                }
            }
            Console.WriteLine();
        }


        public bool QuitterLaPartie(List<Joueur> listeDesJoueurs)
        {
            bool oui = true;
            bool choisi = false;
            ConsoleKeyInfo cki;

            while (!choisi)
            {
                Console.Clear();
                Console.WriteLine("Voulez vous jouer le tour suivant ?");
                Console.WriteLine();
                Console.Write("|");
                if (oui)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" oui ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("| non ");
                }
                else
                {

                    Console.Write(" oui |");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" non ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write("|");

                cki = Console.ReadKey(); // cki contient entre autres le code de la

                switch (cki.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (oui)
                            oui = false;
                        else
                            oui = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (oui)
                            oui = false;
                        else
                            oui = true;
                        break;
                    case ConsoleKey.Enter:
                        choisi = true;
                        break;
                }

            }

            if (!oui)
            {
                // Décision de quitter la partie
                Console.WriteLine("Veuillez entrer le nom de votre fichier de sauvegarde :");
                string nomSauv = Console.ReadLine();

                #region savePlateau
                string[] savePlateau = new string[15];
                string mem = "";
                for(int i = 0; i < 15; i++)
                {
                    for(int j = 0; j < 15; j++)
                    {
                        string intermediaire = Convert.ToString(this.MonPlateau.Plato[i, j]);
                        if (j == 14)
                            mem += intermediaire;
                        else
                            mem += intermediaire + ";";
                    }
                    savePlateau[i] = mem;
                    Console.WriteLine();
                    mem = "";
                }
                using (StreamWriter sw = new StreamWriter(nomSauv + "-Plateau.txt"))
                {
                    foreach (string s in savePlateau)
                    {
                        sw.WriteLine(s);
                    }
                }
                #endregion
                #region saveJoueurs
                int tailleDeLaSave = 3 * listeDesJoueurs.Count;
                string[] saveJoueurs = new string[tailleDeLaSave];
                int compteur = 0;
                foreach(Joueur joueur in listeDesJoueurs)
                {
                    saveJoueurs[compteur] = joueur.Nom + ';' + joueur.Score + ';' + joueur.EstIA;
                    compteur++;
                    mem = "";
                    for (int i = 0; i < joueur.MotsTrouvés.Count; i++)
                    {
                        if (i == joueur.MotsTrouvés.Count - 1)
                            mem += joueur.MotsTrouvés[i];
                        else
                            mem += joueur.MotsTrouvés[i] + ";";
                    }
                    saveJoueurs[compteur] = mem;
                    compteur++;
                    mem = "";
                    for(int i = 0; i < joueur.Main.Count; i++)
                    {
                        string intermediaire = Convert.ToString(joueur.Main[i].Lettre);
                        string intermediaire2 = Convert.ToString(joueur.Main[i].Valeur);
                        if(i== joueur.Main.Count - 1)
                            mem += intermediaire + ";" + intermediaire2;
                        else
                            mem += intermediaire + ";" + intermediaire2 + ";";
                    }
                    saveJoueurs[compteur] = mem;
                    compteur++;
                }
                using (StreamWriter sw = new StreamWriter(nomSauv + "-Joueurs.txt"))
                {
                    foreach (string s in saveJoueurs)
                    {
                        sw.WriteLine(s);
                    }
                }

                #endregion
                #region saveJetons
                mem = "";
                string mem2 = "";
                foreach(Jetons jeton in this.MonSacDeJetons.pioche)
                {
                    string intermediaire = Convert.ToString(jeton.Lettre);
                    string intermediaire2 = Convert.ToString(jeton.Valeur);
                    mem += intermediaire + ";";
                    mem2 += intermediaire2 + ";";
                }
                mem = mem.Remove(mem.Length - 1);
                mem2 = mem2.Remove(mem2.Length - 1);
                using (StreamWriter sw = new StreamWriter(nomSauv + "-Jetons.txt"))
                {
                    sw.WriteLine(mem);
                    sw.WriteLine(mem2);
                }
                #endregion
                #region saveSave

                // LIRE UN FICHIER
                string fullpath = Path.GetFullPath("Saves.txt"); // Mettre dans la variable full path l'accès complet du fichier grâce à la méthode GetFullPath(/nom du fichier à chercher dans l'ordi/)
                List<string> toutesLesLignes = new List<string>(); // Créer une liste dans laquelle on stockera toutes les lignes du fichier texte qu'on souhaite ouvrir
                bool sauvegardeExistante = false;
                try // Méthode très pratique : Essaye d'effectuer les opérations entre les premières accolades, et si ça fait planter le programme, effectue les opérations entre les deuxièmes accolades
                {
                    //Créez une instance de StreamReader pour lire à partir d'un fichier
                    using (StreamReader sr = new StreamReader(fullpath)) // Overture de la lecture d'un fichier rangé dans le path trouvé en première ligne
                    {
                        string line;
                        // Lire les lignes du fichier jusqu'à la fin.
                        while ((line = sr.ReadLine()) != null)
                        {
                            toutesLesLignes.Add(line); // Ajouter les lignes à la liste
                            if (line == nomSauv)
                                sauvegardeExistante = true;
                        }
                    }
                }
                catch (Exception e) // Si le fichier n'a pas été trouvé, le programme affichera le message d'erreur au lieu de planter
                {
                    Console.WriteLine("Le fichier n'a pas pu être lu.");
                    Console.WriteLine(e.Message);
                }
                if (!sauvegardeExistante)
                    toutesLesLignes.Add(nomSauv);


                // ECRIRE DANS UN FICHIER
                using (StreamWriter sw = new StreamWriter("Saves.txt")) // Ouverture de l'écriture d'un fichier
                {
                    foreach (string s in toutesLesLignes) // Pour tous les strings à ajouter au fichier (ici je veux écrire dans un fichier tous les strings de la liste "toutesLesLignes")
                    {
                        sw.WriteLine(s); // écrire dans le fichier
                    }
                }
                // C'est tout :)
                #endregion
            }

            return oui;
        }
    }
}
