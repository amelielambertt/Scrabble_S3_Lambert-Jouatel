using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scabble_JOUATEL
{
    class Plateau
    {
        private char[,] plato;

        public char[,] Plato
        {
            get { return this.plato; }
        }

        public Plateau()
        {
            string fullpath = Path.GetFullPath("PlateauVierge.Txt");
            string[][] tiréDuTexte = new string[15][];
            try
            {
                //Créez une instance de StreamReader pour lire à partir d'un fichier
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line;

                    for(int i = 0; i < 15; i++)
                    {
                        line = sr.ReadLine();
                        tiréDuTexte[i] = line.Split(';', 15);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
            this.plato = new char[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    this.plato[i, j] = Convert.ToChar(tiréDuTexte[i][j]);
                }
            }
        }
        public Plateau(string sauvegarde)
        {
            string fullpath = Path.GetFullPath(sauvegarde + "-Plateau.txt");
            string[][] tiréDuTexte = new string[15][];
            try
            {
                //Créez une instance de StreamReader pour lire à partir d'un fichier
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line;

                    for (int i = 0; i < 15; i++)
                    {
                        line = sr.ReadLine();
                        tiréDuTexte[i] = line.Split(';', 15);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
            this.plato = new char[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    this.plato[i, j] = Convert.ToChar(tiréDuTexte[i][j]);
                }
            }
        }


        public void AffichageOMG(List<Joueur> listeDesJoueurs)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Write('\t');
                for (int j = 0; j < 15; j++)
                {
                    switch (this.plato[i, j])
                    {
                        case '3':
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case '2':
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case '7':
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case '8':
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case '*':
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("**");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case '_':
                            Console.Write("  ");
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(this.plato[i, j] + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                if (i == 5)
                {
                    Console.Write('\t');
                    Console.Write('\t');
                    Console.Write("R |Pts| Nom");
                }
                if(i>5 && i <= listeDesJoueurs.Count + 5)
                {
                    Console.Write('\t');
                    Console.Write('\t');
                    Console.Write((i - 5) + " | " + listeDesJoueurs[i - 6].Score + " | " + listeDesJoueurs[i - 6].Nom);
                }
                
                Console.WriteLine();

            }
        }

        public void AffichageOMGFactice(List<Joueur> listeDesJoueurs, char[,] plateauCurseur, char[,] plateauFactice, int curseurx, int curseury, bool SURLEPLATEAU, bool[,] matriceVerif)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Write('\t');
                for (int j = 0; j < 15; j++)
                {
                    switch (plateauFactice[i, j])
                    {
                        case '3':
                            if(curseurx == i && curseury == j)
                            {
                                if (matriceVerif[i,j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            break;
                        case '2':
                            if (curseurx == i && curseury == j)
                            {
                                if (matriceVerif[i, j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Magenta;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            break;
                        case '7':
                            if (curseurx == i && curseury == j)
                            {
                                if (matriceVerif[i, j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            break;
                        case '8':
                            if (curseurx == i && curseury == j)
                            {
                                if (matriceVerif[i, j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            break;
                        case '*':
                            if (curseurx == i && curseury == j && SURLEPLATEAU)
                            {
                                if (matriceVerif[i, j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Magenta;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("**");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            break;
                        case '_':
                            if (curseurx == i && curseury == j)
                            {
                                if (matriceVerif[i, j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.Write("  ");
                            }
                            break;
                        default:
                            if (curseurx == i && curseury == j && SURLEPLATEAU)
                            {
                                if (matriceVerif[i, j] && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else if ((!matriceVerif[i, j]) && SURLEPLATEAU)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(plateauCurseur[i, j] + " ");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(plateauFactice[i,j] + " ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            break;
                    }
                }
                if (i == 5)
                {
                    Console.Write('\t');
                    Console.Write('\t');
                    Console.Write("R |Pts| Nom");
                }
                if(i>5 && i <= listeDesJoueurs.Count + 5)
                {
                    Console.Write('\t');
                    Console.Write('\t');
                    Console.Write((i - 5) + " | " + listeDesJoueurs[i - 6].Score + " | " + listeDesJoueurs[i - 6].Nom);
                }
                
                Console.WriteLine();

            }
        }

        public override string ToString()
        {
            return base.ToString();
        }


        public bool[,] verifLettresV2(char[,] plateauAbsolumentComplet, char[,] plateauFactice, char[,] plateauPlacé, ref bool[,] verifPlacement)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    verifPlacement[i, j] = false;
                }
            } // tout invalider

            if (this.Plato[7,7] == '*' && plateauFactice[7,7] == '*')
            {
                for(int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (i == 7 && j == 7)
                            verifPlacement[i, j] = true;
                    }
                }
            } // Pour la première lettre du jeu
            else
            {
                int alignementx = -1;
                int alignementy = -1;
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        char l = plateauPlacé[i, j];
                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                        {
                            if(alignementx == -1 && alignementy == -1)
                            {
                                alignementx = i;
                                alignementy = j;
                            }
                            else if(alignementx != -1 && alignementy != -1)
                            {
                                if (i != alignementx)
                                    alignementx = -1;
                                if (j != alignementy)
                                    alignementy = -1;
                            }
                        }
                    }
                } // Vérifie l'alignement

                for (int i = 1; i < 14; i++)
                {
                    for (int j = 1; j < 14; j++)
                    {
                        char l = this.Plato[i, j];
                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                        {
                            verifPlacement[i - 1, j] = true;
                            verifPlacement[i + 1, j] = true;
                            verifPlacement[i, j - 1] = true;
                            verifPlacement[i, j + 1] = true;
                        }
                        l = plateauFactice[i, j];
                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                        {
                            verifPlacement[i - 1, j] = true;
                            verifPlacement[i + 1, j] = true;
                            verifPlacement[i, j - 1] = true;
                            verifPlacement[i, j + 1] = true;
                        }
                    }
                } // Valide toutes les cases adjacentes à une lettre

                #region debug #1
                /*
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        switch (verifPlacement[i, j])
                        {
                            case true:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            case false:
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine(alignementx);
                Console.WriteLine(alignementy);
                Console.WriteLine();
                */
                #endregion


                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        char l = this.Plato[i, j];
                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                        {
                            verifPlacement[i, j] = false;
                        }
                        l = plateauFactice[i, j];
                        if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                        {
                            verifPlacement[i, j] = false;
                        }
                    }
                } // invalide toutes les cases non vides

                #region debug #2
                /*
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        switch (verifPlacement[i, j])
                        {
                            case true:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            case false:
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine(alignementx);
                Console.WriteLine(alignementy);
                Console.WriteLine();
                */
                #endregion


                if (alignementx == -1 && alignementy == -1) // Si le joueur a commencé à placer des lettres
                {
                    // Console.WriteLine("lol");
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (alignementx != -1 && alignementy == -1)
                            {
                                if (i != alignementx)
                                    verifPlacement[i, j] = false;
                            }
                            if (alignementy != -1 && alignementx == -1)
                            {
                                if (j != alignementy)
                                    verifPlacement[i, j] = false;
                            }
                            if (alignementx != 1 && alignementy != 1)
                            {
                                if (alignementy != j && alignementx != i)
                                    verifPlacement[i, j] = false;
                            }
                            if (alignementx == -1 && alignementy == -1)
                                Console.WriteLine("lol");
                        }
                    } // invalide toutes les cases non alignées
                }


                #region debug #3
                /*
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        switch (verifPlacement[i, j])
                        {
                            case true:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            case false:
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine(alignementx);
                Console.WriteLine(alignementy);
                Console.WriteLine();
                */
                #endregion

                if (alignementx == -1 && alignementy == -1) // Si le joueur a commencé à placer des lettres
                {
                    // Console.WriteLine("lol");
                }
                else
                {
                    for (int i = 1; i < 14; i++)
                    {
                        for (int j = 1; j < 14; j++)
                        {
                            char L1 = plateauFactice[i - 1, j];
                            char L2 = plateauFactice[i + 1, j];
                            char L3 = plateauFactice[i, j - 1];
                            char L4 = plateauFactice[i, j + 1];
                            if (!((L1 != '3' && L1 != '2' && L1 != '7' && L1 != '8' && L1 != '_' && L1 != '*') || (L4 != '3' && L4 != '2' && L4 != '7' && L4 != '8' && L4 != '_' && L4 != '*') || (L3 != '3' && L3 != '2' && L3 != '7' && L3 != '8' && L3 != '_' && L3 != '*') || (L2 != '3' && L2 != '2' && L2 != '7' && L2 != '8' && L2 != '_' && L2 != '*')))
                                verifPlacement[i, j] = false;
                        }
                    } // invalide toutes les cases trop éloignées
                }
                #region debug #4
                /*
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        switch (verifPlacement[i, j])
                        {
                            case true:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            case false:
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write(plateauPlacé[i, j] + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine(alignementx);
                Console.WriteLine(alignementy);
                Console.WriteLine();
                */
                #endregion

            }
            /*
            for(int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.Write(verifPlacement[i, j]);
                }
                Console.WriteLine();
            }
            */
            return verifPlacement;
        }


        // ancienne fonction vérifiant les lettres, désuette
        public int LaFonctionQuiMePerdra(char[,] plateauFactice, int curseurx, int curseury, List<int> alignementReferencex, List<int> alignementReferencey, bool alignementx = false, bool alignementy = false)
        {
            char l = plateauFactice[curseurx, curseury];
            if (l != '3' && l != '2' && l != '7' && l != '8' && l != '_' && l != '*')
                return 2; // Si on lit une lettre à l'endroit du curseur, la lettre n'est pas plaçable
            else
            {
                if (alignementReferencex.Contains(curseurx)) // Si le curseur est aligné en x
                    alignementx = true;
                if (alignementReferencey.Contains(curseury)) // Si le curseur est aligné en y
                    alignementy = true;

                if (alignementx && alignementy)
                    return 0;
                else if (alignementx || alignementy)
                    return 1;
                else
                    return 2;
            }

            //retourne 0 si la lettre est plaçable, 1 si la lettre est peut-être plaçable mais pas encore et 2 Si elle n'est pas plaçable

            /* Ordre des vérifications :
             * Reconnaître toutes les lettres posées 2/10
             * Toutes les lettres posées sont alignées 1/10
             * Reconnaître tous les nouveaux mots crées 9/10
             * Toutes les lettres alignées verticalement forment des mots valides 4-6/10
             * Toutes les lettres alignées horizontalement forment des mots valides 4-6/10
             * 
             * Une fonction récursive devrait aller plus vite et devrait être bien plus facile à coder
             * 
             * Difficulté de la fonction : argh
             * Est-ce que c'est fini après ça ? Absolument pas...
             * 
             * 
             * Elements nécessaires :
             * Le plateau déja posé -> this.plato ok
             * Le plateau en cours de pose -> plateauFactice ok
             * La lettre à vérifier -> plateauCurseur ok
             * 
             * Question : Comment mettre en forme facilement les lettres déjà posées et celle qui ont besoin d'être testées ?
             * Réponse : Supprimer l'ancien tableau du plateauFactice
             * 
             * Question : Comment reconnaître tous les mots nouvellement créés ?
             * 
             * Question : Comment vérifier facilement et rapidement tous les mots créés ?
             * 
             */

        }
    }
}
