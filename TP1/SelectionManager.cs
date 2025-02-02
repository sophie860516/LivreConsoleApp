using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void RequeteLivreCritere(string filePath, int? annee=null, string? idAuteur=null, string? categ=null)
        {
            var livres = ChargerLivres(filePath);
            //convertir la liste à IQueyable <T> pour utiliser LINQ
            var query = livres.AsQueryable();


            if (annee != null)
            {
                query = query.Where(l => l.Annee >= annee);
            }
            // convertir vers une liste pour les résultats
            var livresTrouves = query.ToList();

            // Si la liste n'est pas vide, on affiche les résultat selon le critère 
            if (livresTrouves.Any())
            {
                AffichageResultat(livresTrouves, annee);
            }
        }

        public static void AffichageResultat(List<Livre> liste, int? annee)
        {

            // Largeur totale de notre "table"
            int largeurTable = 70;


            if (annee != null)
            {
                // 1) Afficher le titre principal centré
                EcrireCentre(string.Format("***** LISTE DES FILMS APRÈS ANNÉE {0}*****",annee), largeurTable);
                Console.WriteLine();

                // 2) Construire l'en-tête (titres de colonnes) avec un formatage
                //    {0, -5}  signifie : 1er champ, aligné à gauche sur 5 caractères
                //    {1, -20} signifie : 2e champ, aligné à gauche sur 20 caractères, etc.
                string enteteColonnes = string.Format("{0, -5}  {1, -20}  {2, -15}  {3, -5}  {4, -10}",
                                                      "Id", "Titre", "IdAuteur", "Pages", "Catégorie");

                Console.WriteLine(enteteColonnes);

                // 3) Ligne de séparation
                Console.WriteLine(new string('_', largeurTable));

                // 4) Afficher chaque film dans un format tabulaire
                foreach (var livre in liste)
                {
                    // Même pattern que pour l'entête
                    string ligneLivre = string.Format("{0, -5}  {1, -20}  {2, -15}  {3, -5}  {4, -10}",
                                                     livre.Id,
                                                     livre.Titre,
                                                     livre.IdAuteur,
                                                     livre.Pages,
                                                     livre.Categ);
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
