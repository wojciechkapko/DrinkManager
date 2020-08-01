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
            else
            {
                return true;
            }
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
            var choice = new ConsoleKeyInfo();
            do
            {
                PrintOnConsole.PrintInstructionWhileOnPagedList(contemporaryList, page);
                choice = Console.ReadKey();
                Console.Clear();
                if (char.IsDigit(choice.KeyChar))
                {
                    if (choice.KeyChar != '0')
                    {
                        if (TryToWriteDrinkInfo(contemporaryList, page, choice) == false)
                        {
                            return -1;
                        }
                        else
                        {
                            PrintOnConsole.PressAnyKeyToGoBackToPreviousMenu();
                            PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                        }
                    }
                    else
                    {
                        PrintOnConsole.ReWriteDrinkListOnConsole(contemporaryList, page, choice);
                        continue;
                    }
                }
                else if (choice.Key == ConsoleKey.N)
                {
                    if (CheckIfUserCanGoToNextPage(contemporaryList, page, choice) == false)
                    {
                        continue;
                    }
                    else
                    {
                        page++;
                        return page;
                    }
                }
                else if (choice.Key == ConsoleKey.P)
                {
                    if (CheckIfUserCanGoBackToPreviousPage(contemporaryList, page, choice) == false)
                    {
                        continue;
                    }
                    else
                    {
                        page--;
                        return page;
                    }
                }
                else if (choice.Key == ConsoleKey.Escape)
                {
                    return -1;
                }
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
