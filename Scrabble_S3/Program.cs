using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Scabble_JOUATEL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dictionnaire DicoFR = new Dictionnaire();
            // Console.WriteLine(DicoFR.RechDichoRecursif("ablikt"));

            THEMENU();

            // Test :


            /*
            string[] str = new string[]
            {
                "Lorem Ipsum",
                "Lorem Ipsum",
                "Lorem Ipsum",
                "Lorem Ipsum"
            };

            using (StreamWriter sw = new StreamWriter("myFile.txt"))
            {
                foreach (string s in str)
                {
                    sw.WriteLine(s);
                }
            }
            */



            /* BUG LIST :
             * 
             * Corrigé : 
             * Possibilité d'enlever TOUS les joueurs en passant bougeant un peu
             * Possibilité de créeer la même sauvegarde plusieurs fois
             * 
             * 
             * 
             * 
             * IA peuvent avoir deux fois le même prénom
             * 
             */







            /*
            Console.SetWindowSize(60,30);
            Plateau PartieTest = new Plateau();
            PartieTest.AffichageOMG();
            Console.ReadKey();
            Console.Clear();
            PartieTest.AffichageOMG();

            Création + affichage du sac (Stack)
            Sac_Jetons Partie1 = new Sac_Jetons("Jetons.txt");
            Console.WriteLine(Partie1.ToString());
            */
        }

        #region Menu Principal

        static void THEMENU() // Boucle principale
        {
            int option = -1;
            while(option == -1) //tant qu'aucune option n'est choisie
            {
                option = menuScrabble(); // Définir une option à l'aide de la méthode menuScrabble
                Console.Clear();

                switch (option) // En fonction de l'option choisie
                {
                    case 0: // Lancer une nouvelle partie
                        Jeu MaPartie = new Jeu();
                        MaPartie.PartieEnCours();
                        option = -1;
                        Console.WriteLine("Retour au menu...");
                        Thread.Sleep(500);
                        break;
                    case 1: // Charger une partie
                        string charger = MenuCharger();
                        if(charger != null)
                        {
                            Jeu PartieChargée = ChargerUnePartie(charger);
                            PartieChargée.PartieEnCours();
                        }
                        option = -1;
                        Console.WriteLine("Retour au menu...");
                        Thread.Sleep(500);
                        break;
                    case 2: // Afficher les règles
                        Console.WriteLine("Règles encore en beta, appuyez sur une touche pour revenir au menu.");
                        Console.ReadKey();
                        option = -1;
                        Console.WriteLine("Retour au menu...");
                        Thread.Sleep(500);
                        break;
                    default: // Quitter la boucle
                        break;
                }
            }
        }

        static int menuScrabble()
        {
            int optionSelectionnée = 0;
            string[] options = new string[4];


            options[0] = "Nouvelle partie";
            options[1] = "Charger une partie";
            options[2] = "Règles";
            options[3] = "Quitter"; // Ranger tous les choix possibles en un tableau

            int confirmation = -1;

            ConsoleKeyInfo cki; //déclare une variable de type ConsoleKeyInfo


            while (confirmation == -1) // Tant qu'aucune option n'est confirmée
            {

                Console.Clear();
                affichageMenu900IQ(options, optionSelectionnée, "------ SCRABBLE ------");    // Afficher le menu
                cki = Console.ReadKey(); // cki contient entre autres le code de la

                switch (cki.Key)
                {
                    case ConsoleKey.DownArrow:
                        optionSelectionnée++; // Bouger le curseur vers le bas
                        if (optionSelectionnée >= 4)
                            optionSelectionnée = 0;
                        break;
                    case ConsoleKey.UpArrow: 
                        optionSelectionnée--; // Bouger le curseur vers le bas
                        if (optionSelectionnée < 0)
                            optionSelectionnée = 3;
                        break;
                    case ConsoleKey.Escape:
                        confirmation = 3; // Quitter le menu
                        break;
                    case ConsoleKey.Enter:
                        confirmation = optionSelectionnée; // Confirmer l'option 
                        break;
                    case ConsoleKey.Spacebar:
                        confirmation = optionSelectionnée; // Confirmer l'option
                        break;
                }
            }
            return confirmation;
        }

        static void affichageMenu900IQ(string[] options, int surbrillance, string bienvenue)
        {
            Console.WriteLine(bienvenue);
            Console.WriteLine();
            Console.WriteLine("Veuillez sélectionner une option :");
            Console.WriteLine("(Pour sortir du menu, appuyez sur echapp)");
            Console.WriteLine();

            for (int i = 1; i <= options.Length; i++)
            {
                if (surbrillance + 1 == i) // Affichage de toutes les options avec l'option sélectionnée en noir sur fond blanc
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(i + ": " + options[i - 1]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.WriteLine(i + ": " + options[i - 1]);
                }
            }
            Console.WriteLine();
        }


        #endregion

        #region Charger Une Partie
        static string MenuCharger()
        {
            string chargeage = null;
            string fullpath = Path.GetFullPath("Saves.txt");
            List<string> toutesLesLignes = new List<string>();
            try
            {
                //Créez une instance de StreamReader pour lire à partir d'un fichier
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line;
                    // Lire les lignes du fichier jusqu'à la fin.
                    while ((line = sr.ReadLine()) != null)
                    {
                        toutesLesLignes.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
            if (toutesLesLignes.Count == 1)
            {
                Console.Write("Aucun fichier n'est sauvegardé...");
            }
            else if (toutesLesLignes.Count == 1)
            {
                chargeage = toutesLesLignes[1];
            }
            else
            {
                string[] menuDesSaves = new string[toutesLesLignes.Count - 1];
                for (int i = 1; i < toutesLesLignes.Count; i++)
                {
                    menuDesSaves[i - 1] = toutesLesLignes[i];
                }

                #region MenuDesSaves

                int sélection = 0;
                bool choisi = false;
                ConsoleKeyInfo cki;

                while (!choisi)
                {
                    Console.Clear();
                    Console.WriteLine("Quel sauvegarde voulez vous charger ?");
                    Console.WriteLine();

                    for (int i = 0; i < menuDesSaves.Length; i++)
                    {
                        if (i == sélection)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(menuDesSaves[i]);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.WriteLine(menuDesSaves[i]);
                        }
                    }

                    Console.WriteLine();

                    cki = Console.ReadKey(); // cki contient entre autres le code de la

                    switch (cki.Key)
                    {
                        case ConsoleKey.UpArrow:
                            sélection--;
                            if (sélection == -1)
                                sélection = menuDesSaves.Length - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            sélection++;
                            if (sélection == menuDesSaves.Length)
                                sélection = 0;
                            break;
                        default:
                            choisi = true;
                            break;
                    }
                    chargeage = menuDesSaves[sélection];


                }

                Console.WriteLine("Chargement de la sauvegarde...");
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine();
                #endregion
            }
            return chargeage;
        }

        static Jeu ChargerUnePartie(string fichier)
        {
            Plateau plateauChargé = new Plateau();
            Sac_Jetons sacChargé = new Sac_Jetons(fichier);

            string fullpath = Path.GetFullPath(fichier + "-Joueurs.txt");
            List<string> toutesLesLignes = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        toutesLesLignes.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
            string[] un = null;
            string[] deux = null;
            string[] trois = null;
            List<Joueur> listeDesJoueurs = new List<Joueur>();
            for (int i = 0; i < toutesLesLignes.Count; i++)
            {
                un = toutesLesLignes[i].Split(";");
                i++;
                deux = toutesLesLignes[i].Split(";");
                i++;
                trois = toutesLesLignes[i].Split(";");
                List<string> motsTrouvés = new List<string>();
                foreach (string mot in deux)
                {
                    motsTrouvés.Add(mot);
                }
                List<Jetons> main = new List<Jetons>();
                for(int j = 0; j < trois.Length; j += 2)
                {
                    Jetons jetonDansLaMain = new Jetons(Convert.ToChar(trois[j]), Convert.ToInt32(trois[j + 1]));
                    main.Add(jetonDansLaMain);
                }
                Joueur GERARDO = new Joueur(un[0], Convert.ToInt32(un[1]), motsTrouvés, main, Convert.ToBoolean(un[2]));
                listeDesJoueurs.Add(GERARDO);
            }
            Jeu PartieChargéeEnfin = new Jeu(plateauChargé, sacChargé, listeDesJoueurs);
            return PartieChargéeEnfin;
        }
        #endregion
    }
}
