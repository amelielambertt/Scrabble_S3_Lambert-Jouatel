﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scabble_JOUATEL
{
    class Dictionnaire
    {
        string[][] GODZILLA;
        public Dictionnaire()
        {
            string fullpath = Path.GetFullPath("Francais.txt"); //mettre dans la variable fullpath l'accès complet du fichier grâce à la méthode GetFulllPath ("nom du fichier dans l'ordi")
            List<string> toutesLesLignes = new List<string>(); //création d'une liste dans laquelle on stockeratoutes les lignes du fichier texte que l'on souhaite ouvrir
            try
            {
                //Créez une instance de StreamReader pour lire à partir d'un fichier
                using (StreamReader sr = new StreamReader(fullpath)) //ouverure de la lecture d'un fichier rangé dans le path trouvé en premiere ligne
                {
                    string line;
                    // Lire les lignes du fichier jusqu'à la fin.
                    while ((line = sr.ReadLine()) != null)
                    {
                        toutesLesLignes.Add(line);
                    }
                }
            }
            catch (Exception e) //Si le fichier n'a pas été trouvé, le programme affichera le message d'erreur au lieu de planter
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }

            string[][] GODZILLA = new string[14][];
            for(int i = 0; i < toutesLesLignes.Count; i+=2)
                GODZILLA[i/2] = toutesLesLignes[i + 1].Split(' ');
            this.GODZILLA = GODZILLA;
        }


        public bool RechDichoRecursif(string mot, int début = 0, int fin = -1) //teste si le mot est bien dans le dictionnaire 
        {
            if (mot == null)
                return false;
            else if (mot.Length < 2 || mot.Length > 15)
                return false;
            else if (fin == -1)
                fin = this.GODZILLA[mot.Length - 2].Length;
            else if (début > fin)
                return false;

            string motEnMajuscule = mot.ToUpper();
            int milieuDynamique = (début + fin) / 2;
            if (this.GODZILLA[mot.Length - 2][milieuDynamique] == motEnMajuscule)
                return true;
            else if (String.Compare(mot, this.GODZILLA[mot.Length - 2][milieuDynamique]) < 0)
                return RechDichoRecursif(mot, début, milieuDynamique - 1);
            else
                return RechDichoRecursif(mot, milieuDynamique + 1, fin);

        }


        public override string ToString() qui retourne une chaîne de caractères qui décrit le dictionnaire à savoir ici le nombre de mots par longueur et la langue
        {
            Console.WriteLine("Prépare toi à un turbo string de l'enfer");
            Console.WriteLine("Tu vas avoir des séries de " + this.GODZILLA[5].Length + " mots d'un coup");
            string leRetourDeGodzilla = "";
            for (int i = 0; i < this.GODZILLA.Length; i++) 
            {
                Console.WriteLine(i);
                leRetourDeGodzilla += "Tous les mots à " + (i + 2) + " lettres :" + '\n';
                for(int j = 0; j < this.GODZILLA[i].Length; j++)
                {
                    leRetourDeGodzilla += this.GODZILLA[i][j] + "; ";
                    if (j % (20 - i) == 0)
                    {
                        leRetourDeGodzilla += '\n';
                    }
                }
                leRetourDeGodzilla += '\n';
                leRetourDeGodzilla += '\n';
            }
            return leRetourDeGodzilla;
        }
    }
}
