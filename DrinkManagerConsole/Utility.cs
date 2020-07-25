using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace DrinkManagerConsole
{
    internal static class Utility
    {
        public static string GetAlcoholicInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Is this an Alcoholic drink?: ");
            Console.WriteLine();
            Console.WriteLine("1. Alcoholic");
            Console.WriteLine("2. Non Alcoholic");
            Console.WriteLine("3. Optional Alcoholic");

            return GetAlcoholicInfoChoice();
        }

        private static string GetAlcoholicInfoChoice()
        {
            var choice = new ConsoleKeyInfo();

            do
            {
                choice = Console.ReadKey(true);
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        return "Alcoholic";

                    case ConsoleKey.D2:
                        return "Non alcoholic";

                    case ConsoleKey.D3:
                        return "Optional alcohol";

                    default:
                        Console.WriteLine("\nUnsupported input, try again...\n");
                        continue;
                }
            } while (choice.Key != ConsoleKey.D1 || choice.Key != ConsoleKey.D2);

            return "";
        }

        public static string GetGenericData(string message = null)
        {
            if (message != null)
            {
                Console.WriteLine();
                Console.Write(message);
            }

            return Console.ReadLine();
        }

        public static List<Ingredient> GetIngredients()
        {
            var ingredients = new List<Ingredient>();

            Console.WriteLine();
            Console.Write("Number of ingredients: ");
            var numberOfIngredients = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.WriteLine();
                Console.Write("Name: ");
                var name = Console.ReadLine();
                Console.Write("Amount: ");
                var amount = Console.ReadLine();

                ingredients.Add(new Ingredient()
                {
                    Name = name,
                    Amount = amount
                });
            }

            return ingredients;
        }

        public static void SearchByAlcoholContent(List<Drink> drinks)
        {
            var choice = new ConsoleKeyInfo();
            do
            {
                Console.Clear();
                Console.WriteLine("Choose one of the searching criteria: ");
                Console.WriteLine("1. Alcoholic drinks");
                Console.WriteLine("2. Non alcoholic drinks");
                Console.WriteLine("3. Optional alcohol drinks");
                Console.WriteLine("4. Alcoholic and optional alcohol drinks");
                Console.WriteLine("5. Non alcoholic and optional alcohol drinks");
                Console.WriteLine("Press any other key to go back to previous menu");

                var contemporaryList = new List<Drink>();
                choice = Console.ReadKey();
                if (choice.Key == ConsoleKey.D1 || choice.Key == ConsoleKey.D2 || choice.Key == ConsoleKey.D3 || choice.Key == ConsoleKey.D4 || choice.Key == ConsoleKey.D5)
                {
                    UserChoice(choice, drinks, contemporaryList);
                }
                else
                {      
                    break;
                }
            }
            while (true);
        }
        internal static void UserChoice(ConsoleKeyInfo key, List<Drink> drinks, List<Drink> contemporaryList)
        {
            string alcoholicInfo;
            Console.Clear();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    {
                        alcoholicInfo = "Alcoholic";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        ShowDrinksPaged(contemporaryList);
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        alcoholicInfo = "Non alcoholic";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        ShowDrinksPaged(contemporaryList);
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        alcoholicInfo = "Optional alcohol";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        ShowDrinksPaged(contemporaryList);
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        alcoholicInfo = "Alcoholic";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        alcoholicInfo = "Optional alcohol";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        ShowDrinksPaged(contemporaryList);
                        break;
                    }
                case ConsoleKey.D5:
                    {
                        alcoholicInfo = "Non alcoholic";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        alcoholicInfo = "Optional alcohol";
                        contemporaryList = AddToSearchList(alcoholicInfo, drinks, contemporaryList);
                        ShowDrinksPaged(contemporaryList);
                        break;
                    }
            }
        }
        public static List<Drink> AddToSearchList(string alcoholicInfo, List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == alcoholicInfo)
                {
                    contemporaryList.Add(drink);
                }
            }
            return contemporaryList;
        }
        public static void ShowDrinksPaged(List<Drink> contemporaryList)
        {
            int page = 0;
            int index = 0;
            int counter = 0;
            while (index < contemporaryList.Count)
            {
                counter++;
                if(counter % 10 == 0)
                {
                    page = CheckDrink(contemporaryList, page);
                    counter = 0;
                }
                else 
                {
                    Console.WriteLine($"{counter}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(index).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(index).AlcoholicInfo.PadRight(12, ' '));
                }
                index = counter + page * 9;
                if(index == contemporaryList.Count)
                {
                    page = CheckDrink(contemporaryList, page);
                    counter = 0;
                    index = counter + page * 9;
                }
                if (page < 0)
                {
                    return;
                }
            }
        }
        public static int CheckDrink(List<Drink> contemporaryList, int page)
        {
            var choice = new ConsoleKeyInfo();
            do
            {
                if(page * 9 + 9 > contemporaryList.Count)
                {
                    if (page == 0)
                    { 
                        Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go back to previous menu, press ESC"); 
                    }
                    else
                    {
                        Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go to previous page press P\nIf you want to go back to previous menu, press ESC");
                    }
                }
                else if (page == 0)
                {
                    Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go to next page press N\nIf you want to go back to previous menu, press ESC");
                }
                else 
                {
                    Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go to next page press N, or P to go to next to previous page\nIf you want to go back to previous menu, press ESC");
                }
                choice = Console.ReadKey();
                Console.Clear();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        WriteDrinkInfo(contemporaryList.ElementAt(page * 9));
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D2:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 1, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D3:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 2, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        WriteDrinkInfo(contemporaryList.ElementAt(page * 9 + 2));
                        break;
                    case ConsoleKey.D4:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 3, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D5:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 4, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D6:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 5, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D7:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 6, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D8:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 7, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.D9:
                        page = ExceptionHandlerCheckDrink(page, page * 9 + 8, contemporaryList);
                        if (page < 0)
                        {
                            return page;
                        }    
                        ReWriteAfterDrinkCheck(contemporaryList, page);
                        break;
                    case ConsoleKey.N:
                        {
                            if (page * 9 + 9 > contemporaryList.Count)
                            {
                                for (int i = page * 9; i < contemporaryList.Count; i++)
                                {
                                    Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                                }
                                if (page == 0)
                                {
                                    Console.WriteLine("\nThis is the only page, there is no more pages");
                                }
                                else
                                {
                                    Console.WriteLine("\nThis is the last page, there is no more");
                                }
                                break;
                            }
                            else
                            {
                                page++;
                                return page;
                            }
                        }
                    case ConsoleKey.P:
                        {
                            if (page == 0)
                            {
                                for (int i = page * 9; i < Math.Min(9, contemporaryList.Count); i++)
                                {
                                    Console.WriteLine($"{i + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                                }
                                if (page * 9 + 9 > contemporaryList.Count)
                                {
                                    Console.WriteLine("\nThis is the one and only page");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nThis is the first page");
                                    break;
                                }
                            }
                            page--;
                            return page;
                        }
                    case ConsoleKey.Escape:
                    {
                            return -1;
                    }
                    default:
                        {
                            for (int i = page * 9; i < Math.Min(page * 9 + 9, contemporaryList.Count); i++)
                            {
                                Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                            }
                            break;
                        }
                }
            }
            while (true);
        }
        public static int ExceptionHandlerCheckDrink(int page, int element, List<Drink> contemporaryList)
        {
            try
            {
                WriteDrinkInfo(contemporaryList.ElementAt(element));
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Exception caught: ArgumentOutOfRangeException. Next time choose the number that is on the list!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return -1;
            }
            return page;
        }
        public static void ReWriteAfterDrinkCheck(List<Drink> contemporaryList, int page)
        {
            Console.WriteLine("\nTo go back to previous menu, press any key");
            Console.ReadKey();
            Console.Clear();
            for (int i = page * 9; i < Math.Min(page * 9 + 9, contemporaryList.Count); i++)
            {
                    Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
            }
        }
        public static void WriteDrinkInfo(Drink drink)
        {
            Console.WriteLine("Drink name: ".PadRight(20, ' '), $"{drink.Name}".PadRight (16, ' '));
            Console.WriteLine("Category: ".PadRight(20, ' '), $"{drink.Category}".PadRight(16, ' '));
            Console.WriteLine("Alcoholic info: ".PadRight(20, ' '), $"{drink.AlcoholicInfo}".PadRight(16, ' '));
            Console.WriteLine("1st ingredient: ".PadRight(20, ' '), $"{drink.IngredientName1} - {drink.IngredientMeasure1}");
            Console.WriteLine("2nd ingredient: ".PadRight(20, ' '), $"{drink.IngredientName2} - {drink.IngredientMeasure2}");
            if (String.IsNullOrEmpty(drink.IngredientName3) == false)
            {
                Console.WriteLine("3rd ingredient: ".PadRight(20, ' '), $"{drink.IngredientName3} - {drink.IngredientMeasure3}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName4) == false)
            {
                Console.WriteLine("4th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName4} - {drink.IngredientMeasure4}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName5) == false)
            {
                Console.WriteLine("5th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName5} - {drink.IngredientMeasure5}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName6) == false)
            {
                Console.WriteLine("6th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName6} - {drink.IngredientMeasure6}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName7) == false)
            {
                Console.WriteLine("7th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName7} - {drink.IngredientMeasure7}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName8) == false)
            {
                Console.WriteLine("8th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName8} - {drink.IngredientMeasure8}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName9) == false)
            {
                Console.WriteLine("9th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName9} - {drink.IngredientMeasure9}");
            }
            if (String.IsNullOrEmpty(drink.IngredientName10) == false)
            {
                Console.WriteLine("10th ingredient: ".PadRight(20, ' '), $"{drink.IngredientName10} - {drink.IngredientMeasure10}");
            }
            return;
        }
    }
}