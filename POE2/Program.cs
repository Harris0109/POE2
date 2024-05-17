/* The following code will consist of 3 classes 2 used to store methods and 
   and background code that will be used to gather user input these 2 classes 
   are Ingredients and Recipe. The 3rd class is called Program which consistes of all
   instructions and interactions used to engage with the user this is also known the interface.
   Below the main method you will find 2 more backgorund methods used to add a new recipe and to display it.
*/

//Calling from the ssytem library in order to sanction the use of generic collections
using System;
using System.Collections.Generic;
using System.Linq;

//First class ingredients with 2 newly added place-holders for calories and foodGroup
public class Ingredient
{
    public string Name { get; set; }
    public string Unit { get; set; }
    public double Quantity { get; set; }
     public int Calories { get; set; }
    public string FoodGroup { get; set; }
    //The method where the necessary information is stored
    public Ingredient(string name, string unit, double quantity, int calories, string foodGroup)
    {
        Name = name;
        Unit = unit;
        Quantity = quantity;
        Calories = calories;
        FoodGroup = foodGroup;
    }
}

//Second class for recipe where everything is set to be stored in a generic collection
public class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } //the list or generic collection that srores all the information from ingredients

    //The method that makes is possble for you to call it into action fron the main method
    public Recipe(string name)
    {
        Name = name;
        Ingredients = new List<Ingredient>();//Creating a method for the list so we may be able to call it
    }

    public int CalcCalories()//Method used to calculate the total calorie count for each recipe
    {
        return Ingredients.Sum(ingredient => ingredient.Calories);
    }
}

class Program
{   //Method used to call the entirety of the recipe class into the main method as well as creating the list that shall store all that info
    static List<Recipe> recipes = new List<Recipe>();

    //The main method
    static void Main(string[] args)
    {

        {
            while (true) //A loop that shall fuction in accordance to the users choices
            {
                Console.WriteLine("1. Add a recipe");
                Console.WriteLine("2. Display recipes");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice) //switch statments that shall bring forth the necessary code in accordance to the usere's choice
                {
                    case 1:
                        AddRecipe();//Calling the add recipe method if the user chooses to add a recipe
                        break;
                    case 2:
                        DisplayRecipes();//Calling the display recipe method if the user chooses to display the recipes
                        break;
                    case 3:
                        Console.WriteLine("Goodbye...");//Extiting the program the the user so chooses to end the program
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");//Error message for incase the user makes a mistake
                        break;
                }
            }
        }
    }


    static void AddRecipe()//Method used to call the add recipe option
    {
        //Gathering the necessary information from the user
        Console.Write("Enter recipe name: ");
        string recipeName = Console.ReadLine();
        Recipe recipe = new Recipe(recipeName);

        Console.WriteLine("Enter the number of ingredients:");
        int ingredientCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < ingredientCount; i++)//Loop used to get information pertaiing to ingredients
        {
            Console.WriteLine($"Enter details for ingredient {i + 1}:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Unit of measurement: ");
            string unit = Console.ReadLine();
            Console.Write("Quantity: ");
            double quantity = double.Parse(Console.ReadLine());
            Console.Write("Calories: ");
            int calories = int.Parse(Console.ReadLine());
            Console.Write("Food Group: ");
            string foodGroup = Console.ReadLine();

            //A place holder to surmise and store all the gathered information and call it when needed
            recipe.Ingredients.Add(new Ingredient(name, unit, quantity, calories, foodGroup));
        }

        recipes.Add(recipe);//The means to call it
        Console.WriteLine("Recipe added successfully!");
    }


    static void DisplayRecipes()//Method to display all recipes if chosen
    {
        if (recipes.Count == 0)//If statement will be displayed if there is no information to show
        {
            Console.WriteLine("No recipes available.");
            return;
        }


        recipes.Sort((x, y) => string.Compare(x.Name, y.Name));//Sort recipes in alphabetical order

        //Use of foreach to call upon the calculation of calories as well as a delegate to warn user if maximum calories has been exceeded
        foreach (var recipe in recipes)
        {
            Console.WriteLine($"{recipe.Name}: Total Calories - {recipe.CalcCalories()}");
            if (recipe.CalcCalories() > 300)//Warn user of exceeding calorie count
            {
                Console.WriteLine("Warning: Total calories exceed 300!");
            }
        }

        Console.Write("Enter the name of the recipe to display details: ");//Search for recipe via name
        string recipeName = Console.ReadLine();

        Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
        if (selectedRecipe != null)//If recipe is found it shall display the necessary information
        {
            Console.WriteLine($"Ingredients for {selectedRecipe.Name}:");
            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
            }
        }
        else
        {
            Console.WriteLine("Recipe not found.");//If not found error message shall be displayed
        }
    }

}

