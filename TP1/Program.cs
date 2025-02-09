using System;
using System.Collections.Generic;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TP1
{
    class Program
    {
        // Chemin global
        static string path = Path.Combine(Directory.GetCurrentDirectory(), "Donnees","livres.txt");
        

        static void Main(string[] args)
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Lister livre");
                Console.WriteLine("2. Ajouter livre");
                Console.WriteLine("3. Supprimer livre par ID");
                Console.WriteLine("4. Modifier un livre par ID");
                Console.WriteLine("5. Sélectionner des livres après l'année choisie ");
                Console.WriteLine("6. Sélectionner des livres par auteur ");
                Console.WriteLine("7. Sélectionner des livres par catégorie ");
                Console.WriteLine("8. Quitter");
                Console.Write("Choix : ");
                string? choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        ListerLivres();
                        break;
                    case "2":
                        AjouterLivre();
                        break;
                    case "3":
                        SupprimerLivre();
                        break;
                    case "4":
                        ModifierLivre();
                        break;

                    case "5":
                        Console.WriteLine("Entrez l'année :");
                        string annee = Console.ReadLine();                       
                        SelectionManager.RequeteLivreCritere(path, annee : int.Parse(annee));
                        break;
                    case "6":
                        Console.WriteLine("Entrez le ID de l'auteur :");
                        string id = Console.ReadLine();
                        SelectionManager.RequeteLivreCritere(path, idAuteur: int.Parse(id));
                        break;
                    case "7":
                        Console.WriteLine("Entrez la catégorie :");
                        string categ = Console.ReadLine();
                        SelectionManager.RequeteLivreCritere(path, categ: categ);
                        break;
                    case "8":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }
        //Afficher tous les livres dans le fichier 'livres.txt'
        static void ListerLivres()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("test " + path);
                Console.WriteLine("Aucun fichier 'livres.txt' n'existe pour l'instant.");
                return;
            }

            string[] lignes = File.ReadAllLines(path);
            Console.WriteLine("\n--- LISTE DES LIVRES ---");
            foreach (string ligne in lignes)
            {
                // id;titre;id auteur;annee; pages; catégorie
                var champs = ligne.Split(';');
                if (champs.Length >= 6)
                {
                    Console.WriteLine($"{champs[0]}, {champs[1]}, {champs[2]},{champs[3]},{champs[4]},{champs[5]}");
                }
            }
            } 

        //Ajouter un livre en fournissant le ID, titre, ID auteur, l'année, et la catégorie. Ajout à la fin du fichier 
        static void AjouterLivre()
        {
            Console.Write("ID du livre : ");
            string? id = Console.ReadLine();

            Console.Write("Titre : ");
            string? titre = Console.ReadLine();

            Console.Write("ID auteur : ");
            string? idAuteur = Console.ReadLine();

            Console.Write("Année : ");
            string? année = Console.ReadLine();

            Console.Write("pages : ");
            string? pages = Console.ReadLine();

            Console.Write("Catégorie : ");
            string? catég = Console.ReadLine();

            // Construire la ligne
            string ligne = $"{id};{titre};{idAuteur};{année};{pages};{catég}";

            // Ajouter en fin de fichier
            File.AppendAllText(path, Environment.NewLine + ligne+ Environment.NewLine); //pas sur s'il faut avoir newline avant line, des fois la nouvelle ligne s'ajoute à la fin de la dernière ligne existante

            Console.WriteLine("livre ajouté.");
        }
        //Supprimer un livre selon le ID fourni
        static void SupprimerLivre()
        {
            Console.Write("Entrez l'ID du livre à supprimer : ");
            string? idASupprimer = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Le fichier n'existe pas, impossible de supprimer.");
                return;
            }

            string[] lignes = File.ReadAllLines(path);
            List<string> lignesFinales = new List<string>(); //size dynamique, peut utliser les méthodes  comme add, remove, insert sort

            foreach (string ligne in lignes)
            {
                var champs = ligne.Split(';');
                if (champs.Length >= 1)
                {
                    string idLivre = champs[0];
                    if (idLivre != idASupprimer)
                    {
                        lignesFinales.Add(ligne);
                    }
                }
                else
                {
                    // Ligne corrompue, on peut choisir de la conserver
                    lignesFinales.Add(ligne);
                }
            }
            File.WriteAllLines(path, lignesFinales);
            Console.WriteLine($"Livre ID {idASupprimer} supprimé (s'il existait).");
        }
        //Modifier les infos d'un livre selon le ID fourni
        static void ModifierLivre()
        {
            Console.Write("Entrez l'ID du livre à modifier : ");
            string? idAModifier = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Le fichier n'existe pas, impossible de modifier.");
                return;
            }
            string[] lignes = File.ReadAllLines(path);
            List<string> lignesFinales = new List<string>(); //size dynamique, peut utliser les méthodes  comme add, remove, insert sort

            foreach (string ligne in lignes)
            {
                var champs = ligne.Split(';');
                if (champs.Length >= 1)
                {
                    string idLivre = champs[0];
                    if (idLivre != idAModifier)
                    {
                        lignesFinales.Add(ligne);
                    }

                    else
                    {
                        Console.WriteLine("Donnez les nouvelles informations pour le livre du ID choisi: ");

                        Console.Write("Titre : ");
                        string? titre = Console.ReadLine();
                        champs[1] = titre;

                        Console.Write("ID auteur : ");
                        string? idAuteur = Console.ReadLine();
                        champs[2] = idAuteur;

                        Console.Write("Année : ");
                        string? année = Console.ReadLine();
                        champs[3] = année;

                        Console.Write("pages : ");
                        string? pages = Console.ReadLine();
                        champs[4] = pages;

                        Console.Write("Catégorie : ");
                        string? catég = Console.ReadLine();
                        champs[5] = catég;

                        //ligne= string.Join(";", champs); marche pas pcq foreach loop la liste ne peut pas être modifié duranr la boucle
                        lignesFinales.Add(string.Join(";", champs));
                        //break;  // Exit the loop after updating the first match
                    }
                }
                else
                {
                    // Ligne corrompue, on peut choisir de la conserver
                    lignesFinales.Add(ligne);
                }
            }
            File.WriteAllLines(path, lignesFinales);
            Console.WriteLine($"Livre ID {idAModifier} modifié.");

        }

        
    }
}

