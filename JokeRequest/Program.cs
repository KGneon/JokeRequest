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
           
            string userInput;

            while (true)
            {
                DisplayMenuOptions();
                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        while (true)
                        {
                            string pageUrl = GetPageUrl(RandomNumberFromTo(1, 59));
                            string jokeXPath = GetJokeXPath(RandomNumberFromTo(1, 5));
                            TextFromPageByHtml(pageUrl, jokeXPath);
                            Console.WriteLine("Aby wylosować kolejny żart kliknij dowolny przycisk, aby wrócić do menu naciśnij 1 i Enter.");

                            string input = Console.ReadLine();
                            if (input == "1")
                            {
                                break;
                            }
                        }
                        break;
                    case "2":

                        while (true)
                        {

                            Console.WriteLine("Wybierz stronę z kawałami:");
                            int userPageChoice = InputCheckIfInt(Console.ReadLine(), RandomNumberFromTo(1, 59));
                            Console.WriteLine("Wybierz numer kawału:");
                            int userJokeChoice = InputCheckIfInt(Console.ReadLine(), RandomNumberFromTo(1, 20));

                            string pageUrl = GetPageUrl(userPageChoice);
                            string jokeXPath = GetJokeXPath(userJokeChoice);

                            TextFromPageByHtml(pageUrl, jokeXPath);

                            Console.WriteLine("Jeżeli wpisana wartość nie jest numerem to został on wylosowany! hehe");
                            Console.WriteLine("Aby wybrać kolejny żart kliknij dowolny przycisk, aby wrócić do menu naciśnij 1 i Enter.");

                            string input = Console.ReadLine();
                            if (input == "1")
                            {
                                break;
                            }
                        }
                        break;
                        case "3":
                        Console.WriteLine("Do zobaczenia!");
                        return;
                        //case "4":
                        //RequestExternalApi();
                        //break;
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

        static void DisplayMenuOptions()
        {
            Console.Clear();
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
            Object documentNode = document.DocumentNode;
            Object nodes = document.DocumentNode.SelectNodes(xPath);
            HtmlNode nodex = document.DocumentNode.SelectNodes(xPath).First();

            string myNewString = nodex.SelectNodes(xPath).First().InnerText.Trim();
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

        static string GetPageUrl(int pageNumber)
        {
            return $"https://rozrywka.ox.pl/rozne?page={pageNumber}";
        }
        static string GetJokeXPath(int pageNumber)
        {
            return $"/html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[{pageNumber}]/div[1]/div/p";
        }

    //    async static void RequestExternalApi()
    //    {
    //        var client = new HttpClient();
    //        var request = new HttpRequestMessage
    //        {
    //            Method = HttpMethod.Get,
    //            RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/random/joke"),
    //            Headers =
    //{
    //    { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
    //    { "X-RapidAPI-Key", "3e23185590msh261d9986bfbbf02p11c230jsn4e131737301c" },
    //},
    //        };
    //        using (var response = await client.SendAsync(request))
    //        {
    //            response.EnsureSuccessStatusCode();
    //            var body = await response.Content.ReadAsStringAsync();
    //            Console.WriteLine(body);
    //        }
    //    }
    }
}
