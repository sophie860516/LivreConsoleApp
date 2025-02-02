using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class  Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int IdAuteur{ get; set; }
        public int Annee { get; set; }
        public int Pages { get; set; }
        public string Categ { get; set; }

        public Livre(int id, string titre, int idAuteur, int annee, int pages, string categ)
        {
            Id = id;
            Titre = titre;
            IdAuteur= idAuteur;
            Annee = annee;
            Pages = pages;
            Categ = categ;
        }
    }
}
