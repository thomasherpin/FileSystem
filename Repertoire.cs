using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    class Repertoire : Fichier
    {

        List<Fichier> fichier = new List<Fichier>();

        public Repertoire(String Nom, Repertoire parent): base (Nom, parent)
        {
            Permission = 4;
        }
        public override bool mkdir(string Nom)
        {
            bool exist = false;

            for (int i = 0; i < fichier.Count() && exist == false; i++)
            {
                exist = Nom == fichier[i].Nom;
            }
            if (this.canWrite() && exist == false)
            {
                fichier.Add(new Repertoire(Nom, this));
                return true;
            }
            return false;
            
        }
        public override List<Fichier> ls()
        {
            return this.fichier;
        }
        public override bool createNewFile(string Nom)
        {
            bool exist = false;

            for (int i = 0; i < fichier.Count() && exist == false; i++)
            {
                exist = Nom == fichier[i].Nom;
            }
            if (this.canWrite() && exist == false)
            {
                fichier.Add(new Fichier(Nom, this));
                return true;
            }
            return false;
        }
        public override bool renameTo(string Nom, string NewNom)
        {
            bool exist = false;
            int j = 0;
            for (int i = 0; i < fichier.Count() && exist == false; i++)
            {
                exist = Nom == fichier[i].Nom;
                if (exist == true)
                {
                    j = i;
                }
            }
            if (this.canWrite() && exist == true)
            {
                bool exist2 = false;
                for (int k = 0; k < fichier.Count() && exist2 == false; k++)
                {
                    exist2 = NewNom == fichier[k].Nom;
                }
                if (exist2 == false)
                {
                    
                    fichier[j].Nom = NewNom;
                    return true; 
                }
            }
            return false;
        }

        public override bool delete(string Nom)
        {
            bool exist = true;
            int j =  0;
            for (int i = 0; i < fichier.Count() && exist == true; i++)
            {
                exist = Nom == fichier[i].Nom;
                if (exist == true)
                {
                    j = i;
                }
            }
            if (this.canWrite() && exist == true)
            {
                this.fichier.Remove(fichier[j]);
                return true;
            }
            return false;
        }
        public override bool isfile()
        {
            return false;
        }

        public override bool isDirectory()
        {
            return true;
        }
        public override Fichier cd(string Nom)
        {
            //On créé un fichier qui deviendra le nouveau fichier courant
            //On initialise au fichier courant pour éviter le renvoie d'un fichier courant null quand on fait un cd sur un fichier/répertorie qui n'existe pas.
            Fichier newFileCurrent = this;
            bool change = false;
            for (int i = 0; change == false && i<fichier.Count;i++)
            {
                if (fichier[i].Nom == Nom && fichier[i].canRead() &&change == false)
                {
                    newFileCurrent = fichier[i];
                    change = true;
                }
            }
            return newFileCurrent;
        }
        public override List<Fichier> search(string name)
        {
            List<Fichier> fichiersearch = new List<Fichier>();
            for (int i = 0; i < fichier.Count(); i++)
            {
                if (fichier[i].Nom == name)
                {
                    fichiersearch.Add(fichier[i]);
                }
                //Si c'est un répertoire, on rappel la fonction pour rechercher dans les fichiers de celui-ci.
                if (fichier[i].isDirectory() == true)
                {
                    fichiersearch.AddRange(fichier[i].search(name));
                }
            }
            return fichiersearch;
        }
    }
}
