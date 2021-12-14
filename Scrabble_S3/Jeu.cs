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
            this.MonSacDeJetons = new Sac_Jetons();
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
                    participant.Add_Main_Courante(this.MonSacDeJetons.retire_Jeton());
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
            // Console.WriteLine('\t' + "Tour : " + compteurDeTour);
            this.MonPlateau.AffichageOMG(this.ListeDesJoueurs);
            Thread.Sleep(1000);
            bool continuerLaPartie = true;
            bool quitter = false;
            do
            {
                foreach (Joueur joueur in this.ListeDesJoueurs)
                {
                    continuerLaPartie = this.TourDUnJoueur(joueur, compteurDeTour, continuerLaPartie);
                }
                compteurDeTour++;
                Console.WriteLine(continuerLaPartie);
                if (!continuerLaPartie)
                    quitter = true;
                else
                    quitter = choixQuitter();
            } while (!quitter);
            QuitterLaPartie(ListeDesJoueurs);
        }

        public bool TourDUnJoueur(Joueur joueurJouant, int compteurDeTour, bool continuerLaPartie)
        {
            Console.WriteLine();

            /*
            char[,] plateauPlacé = new char[15, 15];
            char[,] plateauCurseur = new char[15,15];
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    plateauPlacé[i, j] = this.MonPlateau.Plato[i, j];
                    plateauCurseur[i,j] = this.MonPlateau.Plato[i,j];
                }
            }
            */

            char[,] plateauAbsolumentComplet = new char[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    plateauAbsolumentComplet[i, j] = this.MonPlateau.Plato[i, j];
                }
            }


            char[,] plateauFactice = new char[15, 15]
            {
                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
            };

            char[,] plateauPlacé = new char[15, 15]
            {
                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
            };

            char[,] plateauCurseur = new char[15, 15]
            {
                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
            };



            List<Jetons> jetonsAJouer = new List<Jetons>();
            int indexactuel = -1;
            
            int confirmation = -1;
            int optionSelectionnée = 0;
            int curseurx = 7;
            int curseury = 7;
            bool SURLEPLATEAU = false;

            #region Setup AlignementRéférence
            List<int> alignementReferencex = new List<int>();
            List<int> alignementReferencey = new List<int>();
            List<int> alignementReferencexTemporaire = new List<int>();
            List<int> alignementReferenceyTemporaire = new List<int>();
            for (int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    char l = this.MonPlateau.Plato[i, j];
                    if (l != '3' && l!= '2' && l != '4' && l != '7' && l != '8' && l != '_')
                    {
                        alignementReferencey.Add(j);
                        alignementReferenceyTemporaire.Add(j);
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
                        alignementReferencexTemporaire.Add(j);
                    }
                }
            }
            #endregion

            #region Setup Matrice de Vérification
            bool[,] verifMatrix = new bool[15, 15];
            for(int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    verifMatrix[i,j] = true;
                }
            }
            verifMatrix = this.MonPlateau.verifLettresV2(plateauAbsolumentComplet, plateauFactice, plateauPlacé, ref verifMatrix);
            #endregion

            ConsoleKeyInfo cki; //déclare une variable de type ConsoleKeyInfo
            while (confirmation == -1) // Tant qu'aucune option n'est confirmée
            {
                Console.Clear();
                Console.WriteLine("Tour de : " + joueurJouant.Nom);
                verifMatrix = this.MonPlateau.verifLettresV2(plateauAbsolumentComplet, plateauFactice, plateauPlacé, ref verifMatrix);
                Affichage902IQ(joueurJouant.Main, optionSelectionnée, compteurDeTour, plateauCurseur, plateauAbsolumentComplet, curseurx, curseury, SURLEPLATEAU, verifMatrix, "Placez les lettres souhaitées sur le plateau"); // Afficher le menu

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
                            if (verifMatrix[curseurx,curseury])
                            {
                                plateauFactice[curseurx, curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                                plateauPlacé[curseurx, curseury] = joueurJouant.Main[optionSelectionnée].Lettre;
                                plateauAbsolumentComplet[curseurx, curseury] = joueurJouant.Main[optionSelectionnée].Lettre;

                                //ajouter les lettres adjacentes au plateau factice (donc c'est pas grave si on l'efface)
                                bool lettreAdjacenteTrouvée = true;
                                int deuxiemeCurseurx = curseurx;
                                int deuxiemeCurseury = curseury;
                                while (lettreAdjacenteTrouvée) //chercher à en haut
                                {
                                    lettreAdjacenteTrouvée = false; //on estime qu'on ne trouve pas la lettre
                                    
                                    if (deuxiemeCurseurx != 0) // Si on n'arrive pas en bordure de plateau
                                    {
                                        deuxiemeCurseurx -= 1; // On peut vérifier la case au dessus
                                        char l = plateauAbsolumentComplet[deuxiemeCurseurx, curseury]; // simplification du if en dessous
                                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*') //si la case au dessus est une lettre
                                        {
                                            plateauFactice[deuxiemeCurseurx, curseury] = l; // on ajoute cette lettre au plateau factice pour la retenir
                                            lettreAdjacenteTrouvée = true; // On a trouvé une lettre donc on peut recommencer
                                        }
                                    }
                                }
                                lettreAdjacenteTrouvée = true;
                                deuxiemeCurseurx = curseurx;
                                while (lettreAdjacenteTrouvée) //chercher à en bas
                                {
                                    lettreAdjacenteTrouvée = false; //on estime qu'on ne trouve pas la lettre
                                    if (deuxiemeCurseurx != 14) // Si on n'arrive pas en bordure de plateau
                                    {
                                        deuxiemeCurseurx += 1; // On peut vérifier la case en dessous
                                        char l = plateauAbsolumentComplet[deuxiemeCurseurx, curseury]; // simplification du if en dessous
                                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*') //si la case au dessus est une lettre
                                        {
                                            plateauFactice[deuxiemeCurseurx, curseury] = l; // on ajoute cette lettre au plateau factice pour la retenir
                                            lettreAdjacenteTrouvée = true; // On a trouvé une lettre donc on peut recommencer
                                        }
                                    }
                                }

                                lettreAdjacenteTrouvée = true;

                                while (lettreAdjacenteTrouvée) //chercher à gauche
                                {
                                    lettreAdjacenteTrouvée = false; //on estime qu'on ne trouve pas la lettre
                                    if (deuxiemeCurseury != 0) // Si on n'arrive pas en bordure de plateau
                                    {
                                        deuxiemeCurseury -= 1; // On peut vérifier la case à gauche
                                        char l = plateauAbsolumentComplet[curseurx, deuxiemeCurseury]; // simplification du if en dessous
                                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*') //si la case au dessus est une lettre
                                        {
                                            plateauFactice[curseurx, deuxiemeCurseury] = l; // on ajoute cette lettre au plateau factice pour la retenir
                                            lettreAdjacenteTrouvée = true; // On a trouvé une lettre donc on peut recommencer
                                        }
                                    }
                                }
                                deuxiemeCurseury = curseury;

                                lettreAdjacenteTrouvée = true;
                                while (lettreAdjacenteTrouvée) //chercher à droite
                                {
                                    lettreAdjacenteTrouvée = false; //on estime qu'on ne trouve pas la lettre
                                    if (deuxiemeCurseury != 14) // Si on n'arrive pas en bordure de plateau
                                    {
                                        deuxiemeCurseury += 1; // On peut vérifier la case à droite
                                        char l = plateauAbsolumentComplet[curseurx, deuxiemeCurseury]; // simplification du if en dessous
                                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*') //si la case au dessus est une lettre
                                        {
                                            plateauFactice[curseurx, deuxiemeCurseury] = l; // on ajoute cette lettre au plateau factice pour la retenir
                                            lettreAdjacenteTrouvée = true; // On a trouvé une lettre donc on peut recommencer
                                        }
                                    }
                                }
                                // Le plateauFactice contient désormais l'arbre de tous les mots reliés à celui que le joueur est en train de placer

                                joueurJouant.Main.RemoveAt(optionSelectionnée);

                                #region ancien modif aligement
                                /*
                                if (alignementReferencexTemporaire.Contains(curseurx) && !alignementReferenceyTemporaire.Contains(curseury))
                                {
                                    alignementReferenceyTemporaire.Clear();
                                }

                                if (alignementReferenceyTemporaire.Contains(curseury) && !alignementReferencexTemporaire.Contains(curseurx))
                                {
                                    alignementReferencexTemporaire.Clear();
                                }
                                */
                                #endregion
                                SURLEPLATEAU = false;
                                optionSelectionnée = 0;
                            }
                            break;
                        case ConsoleKey.Escape:
                            plateauCurseur[curseurx,curseury] = this.MonPlateau.Plato[curseurx,curseury];
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
                                    plateauAbsolumentComplet[i, j] = this.MonPlateau.Plato[i, j];
                                }
                            }

                            plateauFactice = new char[15, 15]
                            {
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
                            };

                            plateauPlacé = new char[15, 15]
                            {
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
                            };

                            plateauCurseur = new char[15, 15]
                            {
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
                            };

                            for (int i = jetonsAJouer.Count-1;  i >= 0; i--)
                            {
                                joueurJouant.Add_Main_Courante(jetonsAJouer[i]);
                                jetonsAJouer.RemoveAt(i);
                            }
                            indexactuel = -1;
                            break;
                        #region Valider le mot
                        case ConsoleKey.Enter:
                            // Valider le mot

                            bool tourJoué = false;

                            for(int i = 0; i < 15; i++)
                            {
                                for (int j = 0; j < 15; j++)
                                {
                                    char l = plateauFactice[i, j];
                                    if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                                    {
                                        tourJoué = true;
                                    }
                                }
                            }

                            if (tourJoué)
                            {
                                List<char[]> tousLesMotsTrouvés = new List<char[]>();
                                List<char[]> tousLesBonus = new List<char[]>();
                                for (int i = 0; i < 15; i++)
                                {
                                    List<char> motTrouvé = new List<char>();
                                    List<char> bonusAssociés = new List<char>();
                                    for (int j = 0; j < 15; j++)
                                    {
                                        char l = plateauFactice[j, i];
                                        char L = this.MonPlateau.Plato[j, i];
                                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                                        {
                                            motTrouvé.Add(l);
                                            bonusAssociés.Add(L);
                                        }
                                        else
                                        {
                                            if (motTrouvé.Count > 1)
                                            {
                                                char[] motAjouté = motTrouvé.ToArray();
                                                char[] bonusAjoutés = bonusAssociés.ToArray();
                                                tousLesMotsTrouvés.Add(motAjouté);
                                                tousLesBonus.Add(bonusAjoutés);
                                            }
                                            motTrouvé.Clear();
                                            bonusAssociés.Clear();
                                        }
                                    }
                                    if (motTrouvé.Count > 1)
                                    {
                                        char[] motAjouté = motTrouvé.ToArray();
                                        char[] bonusAjoutés = bonusAssociés.ToArray();
                                        tousLesMotsTrouvés.Add(motAjouté);
                                        tousLesBonus.Add(bonusAjoutés);
                                    }
                                    motTrouvé.Clear();
                                    bonusAssociés.Clear();
                                } // Trouver tous les mots écrits verticalement
                                for (int i = 0; i < 15; i++)
                                {
                                    List<char> motTrouvé = new List<char>();
                                    List<char> bonusAssociés = new List<char>();
                                    for (int j = 0; j < 15; j++)
                                    {
                                        char l = plateauFactice[i, j];
                                        char L = this.MonPlateau.Plato[i, j];
                                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                                        {
                                            motTrouvé.Add(l);
                                            bonusAssociés.Add(L);
                                        }
                                        else
                                        {
                                            if (motTrouvé.Count > 1)
                                            {
                                                char[] motAjouté = motTrouvé.ToArray();
                                                char[] bonusAjoutés = bonusAssociés.ToArray();
                                                tousLesMotsTrouvés.Add(motAjouté);
                                                tousLesBonus.Add(bonusAjoutés);
                                            }
                                            motTrouvé.Clear();
                                            bonusAssociés.Clear();
                                        }
                                    }
                                    if (motTrouvé.Count > 1)
                                    {
                                        char[] motAjouté = motTrouvé.ToArray();
                                        char[] bonusAjoutés = bonusAssociés.ToArray();
                                        tousLesMotsTrouvés.Add(motAjouté);
                                        tousLesBonus.Add(bonusAjoutés);
                                    }
                                    motTrouvé.Clear();
                                    bonusAssociés.Clear();
                                } // Trouver tous les mots écrits horizontalement



                                bool validateur = true;

                                List<string> tousLesMotsValides = new List<string>();
                                List<string> tousLesBonusValides = new List<string>();
                                for (int i = 0; i < tousLesMotsTrouvés.Count; i++)
                                {
                                    string inter2 = new string(tousLesMotsTrouvés[i]);
                                    string inter3 = new string(tousLesBonus[i]);
                                    if (MonDico.RechDichoRecursif(inter2))
                                    {
                                        Console.WriteLine(inter2 + " est un mot valide.");
                                        tousLesMotsValides.Add(inter2);
                                        tousLesBonusValides.Add(inter3);
                                    }
                                    else
                                    {
                                        validateur = false; // Si un mot n'est pas valide, on ne permet pas au tour de se terminer
                                        Console.WriteLine(inter2 + " n'est pas un mot valide");
                                    }
                                }
                                Thread.Sleep(2000);




                                if (validateur) // Uniquement si tous les mots sont valides
                                {
                                    confirmation = 0;

                                    for (int i = 0; i < tousLesMotsValides.Count; i++)
                                    {
                                        int scoreDuMot = 0;
                                        int motCompteDouble = 0;
                                        int motCompteTriple = 0;
                                        for (int j = 0; j < tousLesMotsValides[i].Length; j++)
                                        {
                                            int scoreLocal = 0;
                                            if (tousLesBonus[i][j] == '8') scoreLocal += Jetons.trouverLaValeur(tousLesMotsValides[i][j]) * 2;
                                            else if (tousLesBonus[i][j] == '7') scoreLocal += Jetons.trouverLaValeur(tousLesMotsValides[i][j]) * 3;
                                            else
                                            {
                                                scoreLocal += Jetons.trouverLaValeur(tousLesMotsValides[i][j]);
                                                if (tousLesBonusValides[i][j] == '2') motCompteDouble++;
                                                if (tousLesBonusValides[i][j] == '3') motCompteTriple++;
                                            }
                                            scoreDuMot += scoreLocal;
                                        }
                                        for (int k = 0; k < motCompteDouble; k++) scoreDuMot *= 2;
                                        for (int k = 0; k < motCompteTriple; k++) scoreDuMot *= 3;
                                        joueurJouant.Add_Score(scoreDuMot);
                                    } // Calcul du score des mots


                                    for (int i = 0; i < 15; i++)
                                    {
                                        for (int j = 0; j < 15; j++)
                                        {
                                            char l = plateauFactice[i, j];
                                            if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                                            {
                                                this.MonPlateau.Plato[i, j] = l;
                                                plateauAbsolumentComplet[i, j] = l;
                                            }
                                        }
                                    } // On inscrit les lettres sur le plateau

                                    if (joueurJouant.Main.Count == 0) // Si le joueur a fait un scrabble
                                    {
                                        joueurJouant.Add_Score(50); // +50 points
                                    }
                                    while (joueurJouant.Main.Count < 7 && this.MonSacDeJetons.pioche.Count > 0) // Remplir la main tant que c'est possible
                                    {
                                        joueurJouant.Add_Main_Courante(this.MonSacDeJetons.retire_Jeton());
                                    }
                                    if (this.MonSacDeJetons.pioche.Count == 0) // Si le sac de jetons est vide
                                    {
                                        continuerLaPartie = false; // On arrête la partie
                                    }
                                }
                                else // Si un au moins des mots n'est pas valide
                                {
                                    for (int i = 0; i < 15; i++)
                                    {
                                        for (int j = 0; j < 15; j++)
                                        {
                                            plateauAbsolumentComplet[i, j] = this.MonPlateau.Plato[i, j];
                                        }
                                    }

                                    plateauFactice = new char[15, 15]
                                    {
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
                                    };

                                    plateauPlacé = new char[15, 15]
                                    {
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
                                    };

                                    plateauCurseur = new char[15, 15]
                                    {
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'3','_','_','8','_','_','_','*','_','_','_','8','_','_','3',} ,
                                {'_','_','8','_','_','_','8','_','8','_','_','_','8','_','_',} ,
                                {'_','7','_','_','_','7','_','_','_','7','_','_','_','7','_',} ,
                                {'_','_','_','_','2','_','_','_','_','_','2','_','_','_','_',} ,
                                {'8','_','_','2','_','_','_','8','_','_','_','2','_','_','8',} ,
                                {'_','_','2','_','_','_','8','_','8','_','_','_','2','_','_',} ,
                                {'_','2','_','_','_','7','_','_','_','7','_','_','_','2','_',} ,
                                {'3','_','_','8','_','_','_','3','_','_','_','8','_','_','3',}
                                    };
                                    // Vider tous les tableaux nécessaires au tour

                                    for (int i = jetonsAJouer.Count - 1; i >= 0; i--)
                                    {
                                        joueurJouant.Add_Main_Courante(jetonsAJouer[i]);
                                        jetonsAJouer.RemoveAt(i);
                                    } // Remettre les jetons dans la main
                                    indexactuel = -1;
                                }
                            }
                            


                            break;
                            #endregion
                    }
                }

                
            }
            return continuerLaPartie;
        }




        public void Affichage902IQ(List<Jetons> main, int surbrillance, int compteurDeTour, char[,] plateauCurseur, char[,] plateauFactice, int curseurx, int curseury, bool SURLEPLATEAU, bool[,] matriceVerif, string bienvenue)
        {

            this.MonPlateau.AffichageOMGFactice(this.ListeDesJoueurs, plateauCurseur, plateauFactice, curseurx, curseury, SURLEPLATEAU, matriceVerif);
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
        public bool choixQuitter()
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
            return !oui;
        }

        public void QuitterLaPartie(List<Joueur> listeDesJoueurs)
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
    }
}
