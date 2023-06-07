using System;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace projet_pizza // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        class PizzaPersonnalisee : Pizza {
            static int nombrePizzas = 0;
            public PizzaPersonnalisee() : base("Personnalisee", 5f, false, null)
            {

                nombrePizzas++;
                nom = "Personnalisee" + nombrePizzas;
                
                ingredients = new List<string> ();

                while(true) { 
                    Console.Write("Rentrer un ingrédient pour la pizza personnalisée no" +nombrePizzas + "(ENTER pour terminer) : ");
                    
                    var ing = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(ing))
                    {
                        break;
                    }
                    if(ingredients.Contains(ing))
                    {
                        Console.WriteLine("Cet ingrédients est déjà dans la liste");                       
                    }
                    else { 
                        ingredients.Add(ing);
                        Console.WriteLine("Liste des ingrédients : " + String.Join(", ", ingredients));
                    }
                    Console.WriteLine();
                }
                prix = 5f + (1.5f * ingredients.Count);
                
            }

        }

       
        class Pizza
        {
            public string nom { get; protected set; }
            public float prix { get; protected set; }
            public bool vegetarienne { get; private set; }
            public List<string> ingredients { get; protected set; }

            public Pizza(string nom, float prix, bool vege, List<string> ingredients) {

                this.nom = nom;
                this.prix = prix;
                this.vegetarienne = vege;
                this.ingredients = ingredients;
            }

           public void Afficher()
            {
                string badgeVegetarienne = vegetarienne ? " (V)" : "";
                Console.WriteLine(FormatPremiereLettreMajuscule(nom) + badgeVegetarienne + " - prix : " + prix + " $") ;

                /* var ingredientsAfficher = new List<string>() ;
                foreach (var ingredient in ingredients)
                {
                    ingredientsAfficher.Add(FormatPremiereLettreMajuscule(ingredient));
                }*/

                var ingredientsAfficher = ingredients.Select(i => FormatPremiereLettreMajuscule(i)).ToList();
                Console.WriteLine(String.Join(", ", ingredientsAfficher));
                Console.WriteLine();
            }

            private static string FormatPremiereLettreMajuscule(string s)
            {
                if (string.IsNullOrEmpty(s)) return s;

                string Minuscules = s.ToLower();
                string Majuscules = s.ToUpper();

                string resultat = Majuscules[0] + Minuscules[1..];

                return resultat;
               
            }
            public bool ContientIngredient(string ingredient)
            {
                return ingredients.Where(i => i.ToLower().Contains(ingredient)).ToList().Count > 0;
            }
        }

        static List<Pizza> GetPizzasFromCode()
        {
            var listePizzas = new List<Pizza> {
                new Pizza("4 fromages", 9.5f, true, new List<string> {"mozarella", "cantal", "gruyère", "conté"}),
                new Pizza("pépéroni", 12.0f, false, new List<string> {"pépéroni", "mozarella", "olives", "champigons"}),
                new Pizza("végétarienn", 9.5f, true, new List<string> {"mozarella", "olives", "broccoli", "oignons", "tomates"} ),
                new Pizza("primavera", 11.0f, true, new List<string> {"mozarella", "oignons", "tomates", "broccoli", "coeurs d'articheau", "choufleur"}),
                new Pizza("amoureux de la viande", 14.0f, false, new List<string> {"mozarella", "boeuf haché", "peperoni", "jambon", "saucisses"}),
                new Pizza("hawaïenne", 12.5f, false, new List<string> {"mozarella", "jambon", "ananas", "tomates"}),
                new Pizza("toute garnie", 15.0f, false, new List<string> {"mozarella", "peperoni", "tomates", "olives", "poivron"}),
                // new PizzaPersonnalisee(),
                // new PizzaPersonnalisee(),
            };
            return listePizzas;
        }
        
        static List<Pizza> GetPizzasFromFile(string filename)
        {
            string json = null;
                    try { 
                         json = File.ReadAllText(filename);
                    }
                    catch {
                        Console.WriteLine("Erreur de lecture du fichier : " + filename);
                        return null;
                    }

                    List<Pizza> listePizzas = null; ;
                    try
                    {
                        listePizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
                        return listePizzas;
                    }
                    catch {
                        Console.WriteLine("Erreur : les données json ne sont pas valides");
                        return null;
                    }
            
        }

        static void GenerateJsonFile(List<Pizza> listePizzas, string filename)
        {
            string json = JsonConvert.SerializeObject(listePizzas);
            filename = "pizzas.json";
            File.WriteAllText(filename, json);
            Console.WriteLine("Le fichier a été converti en JSON");
        }

        static List<Pizza> GetPizzasFromUrl(string url)
        {
            var webClient = new WebClient();

            try { 
            string json = webClient.DownloadString(url);
             }
            catch
            {
                Console.WriteLine("Erreur réseau");
            }
            List<Pizza> listePizzas = null; ;
            try
            {
                listePizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
                return listePizzas;
            }
            catch
            {
                Console.WriteLine("Erreur : les données json ne sont pas valides");
                return null;
            }

            return listePizzas;
        }
        static void Main(string[] args)
        {
            var listePizzas = new List<Pizza>();
            string url = "https://codeavecjonathan.com/res/pizzas2.json";
            listePizzas = GetPizzasFromUrl(url);

            if (listePizzas != null) { 
                 foreach (var pizza in listePizzas)
                 {
                     pizza.Afficher();
                 }
            }
            
 

        }
    }
}