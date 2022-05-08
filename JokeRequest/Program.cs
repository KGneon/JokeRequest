using System;
using System.IO;
using Aspose.Words;
using HtmlAgilityPack;

namespace JokeRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            int randomPage = 0;
            int randomJoke = 0; 
            string pageOfJokes = $"https://rozrywka.ox.pl/rozne?page={randomPage}";
            string xPathOfJoke = $"/html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[{randomJoke}]/div[1]/div/p";
            string userInput;

            while (true)
            {
                JokeMenu();
                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        while (true)
                        {
                            randomPage = RandomNumberFromTo(1, 59);
                            randomJoke = RandomNumberFromTo(1, 20);
                            TextFromPageByHtml(pageOfJokes, xPathOfJoke);
                            Console.WriteLine("Aby wylosować kolejny żart kliknij dowolny przycisk, aby wrócić do menu naciśnij 1 i Enter.");

                            string input = Console.ReadLine();
                            if (input == "1")
                            {
                                return;
                            }
                        }
                        break;
                    case "2":

                        while (true)
                        {

                            Console.WriteLine("Wybierz stronę z kawałami:");
                            randomPage = InputCheckIfInt(Console.ReadLine(), RandomNumberFromTo(1, 59));
                            Console.WriteLine("Wybierz numer kawału:");
                            randomJoke = InputCheckIfInt(Console.ReadLine(), RandomNumberFromTo(1, 20));

                            TextFromPageByHtml(pageOfJokes, xPathOfJoke);

                            Console.WriteLine("Jeżeli wpisana wartość nie jest numerem to został on wylosowany! hehe");
                            Console.WriteLine("Aby wybrać kolejny żart kliknij dowolny przycisk, aby wrócić do menu naciśnij 1 i Enter.");

                            string input = Console.ReadLine();
                            if (input == "1")
                            {
                                return;
                            }
                        }
                        break;
                        case "3":
                        Console.WriteLine("Do zobaczenia!");
                        return;
                    default:
                        Console.WriteLine("Nie ma takiej opcji!");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                }



            }


        }

        // /html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[1]/div[1]/div/p/text()
        // /html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[2]/div[1]/div/p
        // /html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[2]/div[1]/div/p

        static void JokeMenu()
        {
            Console.WriteLine("Wybierz co chcesz zrobić:");
            Console.WriteLine("1. Wylosować żart.");
            Console.WriteLine("2. Wybrać stronę i żart.");
            Console.WriteLine("3. Zamknąć aplikację.");
        }
        static void TextFromPageByHtml(string page, string xPath)
        {
            HtmlWeb web = new HtmlWeb();
            //this could be any web page
            HtmlDocument document = web.Load(page);
            //HtmlNode[] nodes = document.DocumentNode.SelectNodes(xPath).ToArray();

            HtmlNode nodex = document.DocumentNode.SelectNodes(xPath).First();

            string myNewString = nodex.SelectNodes(xPath).First().InnerText;
            ShowSplitedArrayOfString(MakeSplitedSting(myNewString));
            //Console.WriteLine(myNewString);
        }

        static int RandomNumberFromTo(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to + 1);
        }

        static string[] MakeSplitedSting(string stringText)
        {
            string[] stringArray = stringText.Split("(?=.)(?=?)(?=:)");
            return stringArray;
        }

        static void ShowSplitedArrayOfString(string[] arrayOfString)
        {
            foreach (string str in arrayOfString)
            {
                Console.WriteLine(str);
            }
        }
        static int InputCheckIfInt(string userInput, int other)
        {
            int otherNumber = other;
            if (int.TryParse(userInput, out int j))
            {
                return j;
            }
            else
            {
                Console.WriteLine("Wrong input. Next time write a number. Press ENTER to continue...");
                Console.ReadLine();
                return otherNumber;
            }
        }
    }
}
