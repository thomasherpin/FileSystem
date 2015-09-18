using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    class Fichier
    {
        public String Nom;
        public int Permission;
        public Repertoire Parent;

        public Fichier(String Nom, Repertoire Parent)
        {
            this.Nom = Nom;
            this.Parent = Parent;
            Permission = 4;
        }

        public bool canWrite()
        {
            return (Permission & 2) > 0;
        }
        public bool canExecute()
        {
            return (Permission & 1) > 0;
        }
        public bool canRead()
        {
            return (Permission & 4) > 0;
        }
        public virtual bool createNewFile(string Nom)
        {
            return false;
        }
        public virtual bool mkdir(string Nom)
        {
            return false;
        }
        public virtual bool delete(string Nom)
        {
            return false;
        }
        public void chmod(int Permission)
        {
            this.Permission = Permission;
        }
        public virtual List<Fichier> ls()
        {
            Console.WriteLine("Vous êtes dans le fichier: " + this.Nom + "Il n'y a pas de fichier(s) dans un fichier");
            return null;
        }
        public string getName()
        {
            return this.Nom;
        }
        public virtual bool renameTo(string Nom, string newNom)
        {
            return false;
        }
        public virtual bool isfile()
        {
            return true;
        }
        public virtual bool isDirectory()
        {
            return false;
        }
        public string getPath()
        {
            string path = this.Nom;
            //on donne le parent initial du fichier
            Repertoire Parents = Parent;
            while (Parents != null)
            {
                path = Parents.Nom + "\\" + path;
                Parents = Parents.Parent;
            }
            //on reprend le parent initial du fichier
            //Parent = Parents;
            return path;
        }
        public string getRoot()
        {
            string root = "Vous êtes déjà dans un fichier racine";
            //on créé un parent temporaire (qui sera réutilisé à chaque appel de fonction et réinitialisé au parent du fichier courant
            Repertoire Parent2 = Parent;
            while (Parent2.Nom != "C:")
            {
                root = Parent2.Nom;
                Parent2 = Parent2.Parent;
            }
            return root;
        }
        public Fichier getParent()
        {
            return Parent;
        }

        public virtual Fichier cd(string Nom)
        {
            return null;
        }
        public void chmod(string Permission)
        {
            this.Permission = int.Parse(Permission);
        }
        public virtual List<Fichier> search(string Nom)
        {
            return null;
        }

    }
}
