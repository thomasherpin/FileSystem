using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string commande;
            Repertoire C = new Repertoire("C:", null);
            Fichier fileCurrent = C;
            string [] listcommande;
                
            for (int i = 0; i < i + 2; i++)
            {
                //Ajouter la ligne d'en dessous si l'on veut afficher à chaque fois le chemin complet d'ou l'on est
                //sConsole.Write(fileCurrent.getPath()+"\\>");
                commande = Console.ReadLine();
                listcommande = commande.Split(' ');

               
                if (listcommande[0] == "parent" && listcommande.Count() == 1 && fileCurrent !=C)
                {
                    if(fileCurrent.canRead())
                    {
                        fileCurrent = fileCurrent.getParent();
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour avoir le parent (canRead => chmod 4/5/6/7)");
                    }
                    
                } 
                else if (listcommande[0] == "create" && listcommande.Count()==2)
                {
                    if (fileCurrent.canWrite())
                    {
                        Console.WriteLine(fileCurrent.createNewFile(listcommande[1]));
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour créer un fichier ici (canWrite => chmod 2/3/6/7)");
                    }
                    
                }
                else if (listcommande[0] == "delete" && listcommande.Count() == 2)
                {
                    if (fileCurrent.canWrite())
                    {
                        Console.WriteLine(fileCurrent.delete(listcommande[1]));
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour supprimer ici (canWrite => chmod 2/3/6/7)");
                    }
                }
                else if (listcommande [0] == "mkdir" && listcommande.Count()==2)
                {
                    if (fileCurrent.canWrite())
                    {
                        Console.WriteLine(fileCurrent.mkdir(listcommande[1]));
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour créer un répertoire ici (canWrite => chmod 2/3/6/7)");
                    }
                }
                else if (listcommande[0] == "file" && listcommande.Count() == 1)
                {
                    if (fileCurrent.canRead())
                    {
                        Console.WriteLine(fileCurrent.isfile());
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour savoir si c'est un fichier (canRead => chmod 4/5/6/7)");
                    }
                    
                }
                else if (listcommande [0] == "directory" && listcommande.Count()==1)
                {
                    if (fileCurrent.canRead())
                    {
                        Console.WriteLine(fileCurrent.isDirectory());
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour savoir si c'est un répertoire (canRead => chmod 4/5/6/7)");
                    }
                    
                }
                else if (listcommande[0] == "ls" && listcommande.Count() == 1)
                {
                    if (fileCurrent.canRead())
                    {
                        if (fileCurrent.ls() != null)
                        {
                            foreach (Fichier item in fileCurrent.ls())
                            {
                                Console.WriteLine(item.Nom + " (" + item.GetType() + ") " + item.Permission);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour lire (canRead => chmod 4/5/6/7)");
                    }
                    
                }
                else if (listcommande[0] == "search" && listcommande.Count() == 2)
                {
                    if (fileCurrent.canRead())
                    {
                        foreach (Fichier fichier in fileCurrent.search(listcommande[1]))
                        {
                            if (fileCurrent.canRead())
                            {
                                Console.WriteLine("Nom du fichier recherché: " + fichier.Nom + "\nChemin: " + fichier.getPath());
                            }
                            else
                            {
                                Console.WriteLine("Vous n\'avez pas les droits pour lire à partir de ce fichier (canRead => chmod 4/5/6/7)");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour lire à partir de ce fichier (canRead => chmod 4/5/6/7)");
                    }
                }
                else if (listcommande[0] == "rename" && listcommande.Count() == 3)
                {
                    if (fileCurrent.canWrite())
                    {
                        Console.WriteLine(fileCurrent.renameTo(listcommande[1], listcommande[2]));
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez par les droits pour écrire (canWrite => chmod 2/3/6/7");
                    }
                }
                else if (listcommande[0] == "name" && listcommande.Count() == 1)
                {
                    if (fileCurrent.canRead())
                    {
                        Console.WriteLine(fileCurrent.getName());
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour lire à partir de ce fichier (canRead => chmod 4/5/6/7)");
                    }
                }
                else if (listcommande[0] == "path" && listcommande.Count() == 1)
                {
                    Console.WriteLine(fileCurrent.getPath());
                }
                else if (listcommande[0] == "cd" && listcommande.Count() == 2)
                {
                    if (fileCurrent.canRead())
                    {
                        fileCurrent = fileCurrent.cd(listcommande[1]);
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour rentrer dans un fichier à partir de celui-ci (canRead => chmod 4/5/6/7)");
                    }
                    
                }
                else if (listcommande[0] == "chmod" && listcommande.Count() == 2)
                {
                    //non soumis à la permission, pour ne pas être bloqué. (On reste toujours super-utilisateur)
                    int newPermission;
                    bool result = Int32.TryParse(listcommande[1], out newPermission);
                    if (1 <= newPermission && newPermission <= 7 && result == true)
                    {
                    fileCurrent.chmod(listcommande[1]);
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un chiffre entre 1 et 7");
                    }
                }
                else if (listcommande[0] == "root" && listcommande.Count() == 1)
                {
                    if (fileCurrent.canRead())
                    {
                        if (fileCurrent != C)
                        {
                            Console.WriteLine("Fichier racine: "+fileCurrent.getRoot());
                        }
                        else
                        {
                            Console.WriteLine("Vous êtes dans le fichier racine, C n\'a pas de parent");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vous n\'avez pas les droits pour connaître le fichier root (canRead => chmod 4/5/6/7)");
                    }
                }
                else
                {
                    Console.WriteLine("Saisie invalide ou commande non comprise, veuillez réessayer.");
                }
            }
        }
    }
}
