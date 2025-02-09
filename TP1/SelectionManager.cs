using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class SelectionManager
    {
        // charger les livre du fichier livre.txt
        public static List<Livre> ChargerLivres(string filePath)
        {
            var livres = new List<Livre>();
            var lignes = File.ReadAllLines(filePath);


            //Vérifier que le format de chauqe ligne est conforme

            foreach (var ligne in lignes)
            {
                string[] champs = ligne.Split(';');
                if (champs.Length == 6 && int.TryParse(champs[0], out int id) &&
                    int.TryParse(champs[2], out int idAuteur) &&
                    int.TryParse(champs[3], out int annee) &&
                    int.TryParse(champs[4], out int pages))
                {
                    var livre = new Livre(id, champs[1], idAuteur, annee, pages, champs[5]);
                    livres.Add(livre);
                }
            }

            return livres;

        }


        //les null dans string? est obligatoire si l'argument est optionnel
        public static void RequeteLivreCritere(string filePath, int? annee=null, int? idAuteur=null, string? categ=null)
        {
            var livres = ChargerLivres(filePath);
            //convertir la liste à IQueyable <T> pour utiliser LINQ
            var query = livres.AsQueryable();
            var livresTrouves = new List<Livre>(); ;


            //si l'utilisateur entre l'année
            if (annee != null)
            {
                Console.WriteLine("annee "+ annee);
                query = query.Where(l => l.Annee >= annee);

                //convertir en liste pour parcourir les résulats, si la liste n'est pas vide, afficher les résultats

                livresTrouves = query.ToList();
                if (livresTrouves.Any())
                {
                    AffichageResultat(livresTrouves, annee : annee);
                }
                else
                {
                    Console.WriteLine("Pas de livre pour la critère recherchée ");
                }
            }

            //si l'utilisateur entre id Auteur
            if (idAuteur != null)
            {
                query = query.Where(l => l.IdAuteur == idAuteur);
                livresTrouves = query.ToList();
                if (livresTrouves.Any())
                {
                    AffichageResultat(livresTrouves, idAuteur:idAuteur);
                }
                else
                {
                    Console.WriteLine("Pas de livre pour la critère recherchée ");
                }

            }

            //si l'utilisateur entre catégorie
            if (categ != null) 
            {
                query = query.Where(l => l.Categ == categ);
                livresTrouves = query.ToList();
                if (livresTrouves.Any())
                {
                    AffichageResultat(livresTrouves, categ:categ);
                }
                else
                {
                    Console.WriteLine("Pas de livre pour la critère recherchée ");
                }
            }

        }

        public static void AffichageResultat(List<Livre> liste, int? annee=null, int? idAuteur = null, string? categ=null)
        {

            // Largeur totale de notre "table"
            int largeurTable = 110;


            if (annee != null)
            {
                // 1) Afficher le titre principal centré
                EcrireCentre(string.Format("***** LISTE DES LIVRES APRÈS ANNÉE {0}*****",annee), largeurTable);
                Console.WriteLine();

                // 2) Construire l'en-tête (titres de colonnes) avec un formatage
                //    {0, -5}  signifie : 1er champ, aligné à gauche sur 5 caractères
                //    {1, -20} signifie : 2e champ, aligné à gauche sur 20 caractères, etc.
                string enteteColonnes = string.Format("{0, -5}  {1, -60}  {2, -20}  {3, -10}  {4, -10}",
                                                      "Id", "Titre", "IdAuteur", "Pages", "Catégorie");

                Console.WriteLine(enteteColonnes);

                // 3) Ligne de séparation
                Console.WriteLine(new string('_', largeurTable));

                // 4) Afficher chaque film dans un format tabulaire
                foreach (var livre in liste)
                {
                    // Même pattern que pour l'entête
                    string ligneLivre = string.Format("{0, -5}  {1, -60}  {2, -20}  {3, -10}  {4, -10}",
                                                     livre.Id,
                                                     livre.Titre,
                                                     livre.IdAuteur,
                                                     livre.Pages,
                                                     livre.Categ);
                    Console.WriteLine(ligneLivre);
                }
                


             }
            if (idAuteur != null)
            {
                // 1) Afficher le titre principal centré
                EcrireCentre(string.Format("***** LISTE DES LIVRES DONT LE ID D'AUTEUR EST  {0}*****", idAuteur), largeurTable);
                Console.WriteLine();

                // 2) Construire l'en-tête (titres de colonnes) avec un formatage.
                string enteteColonnes = string.Format("{0, -5}  {1, -60}  {2, -10}  {3, -10}  {4, -10}",
                                                      "Id", "Titre", "Année", "Pages", "Catégorie");

                Console.WriteLine(enteteColonnes);

                // 3) Ligne de séparation
                Console.WriteLine(new string('_', largeurTable));

                // 4) Afficher chaque film dans un format tabulaire
                foreach (var livre in liste)
                {
                    // Même pattern que pour l'entête
                    string ligneLivre = string.Format("{0, -5}  {1, -60}  {2, -10}  {3, -10}  {4, -10}",
                                                     livre.Id,
                                                     livre.Titre,
                                                     livre.Annee,
                                                     livre.Pages,
                                                     livre.Categ);
                    Console.WriteLine(ligneLivre);
                }
            }
            if (categ != null)
            {
                // 1) Afficher le titre principal centré
                EcrireCentre(string.Format("***** LISTE DES LIVRES DE LA CATÉGORIE {0}*****",  categ.ToUpper()), largeurTable);
                Console.WriteLine();

                // 2) Construire l'en-tête (titres de colonnes) avec un formatage.
                string enteteColonnes = string.Format("{0, -5}  {1, -60}  {2, -10}  {3, -10}  {4, -10}",
                                                      "Id", "Titre", "IdAuteur","Année", "Pages");

                Console.WriteLine(enteteColonnes);

                // 3) Ligne de séparation
                Console.WriteLine(new string('_', largeurTable));

                // 4) Afficher chaque film dans un format tabulaire
                foreach (var livre in liste)
                {
                    // Même pattern que pour l'entête
                    string ligneLivre = string.Format("{0, -5}  {1, -60}  {2, -10}  {3, -10}  {4, -10}",
                                                     livre.Id,
                                                     livre.Titre,
                                                     livre.IdAuteur,
                                                     livre.Annee,
                                                     livre.Pages);
                    Console.WriteLine(ligneLivre);
                }
            }
            // 6) Message final
            Console.WriteLine($"Nombre de livress : {liste.Count}");

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }

        // Écrit un texte centré sur la console selon une largeur donnée.
        static void EcrireCentre(string texte, int largeur)
        {
            if (string.IsNullOrEmpty(texte))
            {
                Console.WriteLine();
                return;
            }

            int espacesAGauche = (largeur - texte.Length) / 2;
            if (espacesAGauche < 0) espacesAGauche = 0;

            string ligne = new string(' ', espacesAGauche) + texte;
            Console.WriteLine(ligne);
        }
    }
}
