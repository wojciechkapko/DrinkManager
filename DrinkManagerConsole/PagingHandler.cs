using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace DrinkManagerConsole
{
    public class PagingHandler
    {
        public static bool TryToWriteDrinkInfo(List<Drink> contemporaryList, int page, ConsoleKeyInfo choice)
        {
            try
            {
                PrintOnConsole.WriteDrinkInfo(contemporaryList.ElementAt(page * 9 + int.Parse(choice.KeyChar.ToString()) - 1));
            }
            catch (ArgumentOutOfRangeException)
            {
                PrintOnConsole.WriteExceptionCaughtInfo();
                return false;
            }
            return true;
        }

        public static bool CheckIfUserCanGoToNextPage(List<Drink> contemporaryList, int page, ConsoleKeyInfo choice)
        {
            if (page * 9 + 9 > contemporaryList.Count)
            {
                PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                PrintOnConsole.TellUserThatHeCanNotGoToNextPage(page);
                return false;
            }
            return true;
        }
        public static bool CheckIfUserCanGoBackToPreviousPage(List<Drink> contemporaryList, int page, ConsoleKeyInfo choice)
        {
            if (page == 0)
            {
                PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                PrintOnConsole.TellUserThatHeCanNotGoBack(contemporaryList, page);
                return false;
            }
            return true;
        }
        public static int MoveThroughPagesOrCheckDrinkInfo(List<Drink> contemporaryList, int page)
        {
            do
            {
                PrintOnConsole.PrintInstructionWhileOnPagedList(contemporaryList, page);
                var choice = Console.ReadKey();
                Console.Clear();
                //Moves to drink info if user press 1 - 9
                if (char.IsDigit(choice.KeyChar))
                {
                    if (choice.KeyChar != '0')
                    {
                        if (TryToWriteDrinkInfo(contemporaryList, page, choice) == false)
                        {
                            return -1;
                        }
                        PrintOnConsole.PressAnyKeyToGoBackToPreviousMenu();
                        PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                    }
                    //Cleans and rewrites list if user picked 0
                    else
                    {
                        PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                    }
                }
                //If user picked N for Next Page, page is increased
                else if (choice.Key == ConsoleKey.N && CheckIfUserCanGoToNextPage(contemporaryList, page, choice))
                {
                    page++;
                    return page;
                }
                //If user picked P for Previous Page, page is decreased
                else if (choice.Key == ConsoleKey.P && CheckIfUserCanGoBackToPreviousPage(contemporaryList, page, choice))
                {
                    page--;
                    return page;
                }
                //Exits to main menu
                else if (choice.Key == ConsoleKey.Escape)
                {
                    return -1;
                }
                //Rewrites drink list to console if user gives unsupported input
                else
                {
                    PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                }
            }
            while (true);
        }
        public static void DivideDrinkListIntoPages(List<Drink> contemporaryList)
        {
            int page = 0;
            int index = 0;
            int counter = 0;
            while (index < contemporaryList.Count)
            {
                counter++;
                if (counter % 10 == 0)
                {
                    page = MoveThroughPagesOrCheckDrinkInfo(contemporaryList, page);
                    counter = 0;
                }
                else
                {
                    PrintOnConsole.PrintDrinksOnPage(contemporaryList, counter, index);
                }
                index = counter + page * 9;
                if (index == contemporaryList.Count)
                {
                    page = MoveThroughPagesOrCheckDrinkInfo(contemporaryList, page);
                    counter = 0;
                    index = counter + page * 9;
                }
                if (page < 0)
                {
                    return;
                }
            }
        }
    }
}
