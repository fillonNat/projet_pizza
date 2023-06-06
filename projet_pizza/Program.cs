using System;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

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
            protected string nom;
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
        static void Main(string[] args)
        {

            //Créer un liste de pizzas
            var listePizzas = new List<Pizza> {
                new Pizza("4 fromages", 9.5f, true, new List<string> {"mozarella", "cantal", "gruyère", "conté"}),
                new Pizza("pépéroni", 12.0f, false, new List<string> {"pépéroni", "mozarella", "olives", "champigons"}),
                new Pizza("végétarienn", 9.5f, true, new List<string> {"mozarella", "olives", "broccoli", "oignons", "tomates"} ),
                new Pizza("primavera", 11.0f, true, new List<string> {"mozarella", "oignons", "tomates", "broccoli", "coeurs d'articheau", "choufleur"}),
                new Pizza("amoureux de la viande", 14.0f, false, new List<string> {"mozarella", "boeuf haché", "peperoni", "jambon", "saucisses"}),
                new Pizza("hawaïenne", 12.5f, false, new List<string> {"mozarella", "jambon", "ananas", "tomates"}),
                new Pizza("toute garnie", 15.0f, false, new List<string> {"mozarella", "peperoni", "tomates", "olives", "poivron"}),
                new PizzaPersonnalisee(),
                new PizzaPersonnalisee(),
            };

            // afficher les pizza du prix moins cher au plus cher

            // listePizzas = listePizzas.OrderBy(p => p.prix).ToList();
            // et du prix plus cher au moins cher
            // listePizzas = listePizzas.OrderByDescending(p => p.prix).ToList();

            //listePizzas = listePizzas.FindAll(p => p.vegetarienne);
           // listePizzas = listePizzas.Where(p => p.vegetarienne).ToList();  // autre façon de le faire
           
            // listePizzas = listePizzas.Where(p => p.ContientIngredient("oignon")).ToList();
            //boucler pour afficher les pizzas
            foreach (var pizza in listePizzas)
            {
                pizza.Afficher();
            }
           /* float pizzaMoinsChere = listePizzas[0].prix;
            int indexMoinsChere = 0;
            // La pizza la moins chere
            for(int i=0; i< listePizzas.Count; i++)
            {
                if (listePizzas[i].prix < pizzaMoinsChere) { 
                pizzaMoinsChere = listePizzas[i].prix;
                indexMoinsChere = i;                
                }
            }
            Console.WriteLine("La pizza la moins chère est : " );
            listePizzas[indexMoinsChere].Afficher();

            float pizzaPlusChere = listePizzas[0].prix;
            int indexPlusChere = 0;
            // La pizza la moins chere
            for (int i = 0; i < listePizzas.Count; i++)
            {
                if (listePizzas[i].prix > pizzaPlusChere)
                {
                    pizzaPlusChere = listePizzas[i].prix;
                    indexPlusChere = i;
                }
            }
            Console.WriteLine("La pizza la plus chère est : ");
            listePizzas[indexPlusChere].Afficher();*/

        }
    }
}