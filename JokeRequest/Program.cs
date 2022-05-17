using System;
using System.IO;
using Aspose.Words;
using HtmlAgilityPack;

namespace JokeRequest
{
    class JokeProviderFactory
    {

        private HtmlJokeScraper scraper;
        public JokeProviderFactory(HtmlJokeScraper scraper)
        {
            this.scraper = scraper;
        }
        public IJokeProvider PickJokeProvider(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    return new RandomJokeProvider(scraper);
                case "2":
                    return new UserPickJokeProvider(scraper);
                default:
                    throw new ArgumentException("Joke provider not exists");
            }
        }
    }
    interface IJokeProvider
    {
        public string getJoke();
    }

    class RandomJokeProvider : IJokeProvider
    {
        private const int RANDOM_PAGE_RANGE_MAX = 59;
        private const int RANDOM_PAGE_RANGE_MIN = 1;
        private const int RANDOM_JOKE_RANGE_MAX = 5;
        private const int RANDOM_JOKE_RANGE_MIN = 1;


        private HtmlJokeScraper scraper;
        public RandomJokeProvider(HtmlJokeScraper scraper)
        {
            this.scraper = scraper;
        }
        public string getJoke()
        {
            Random rand = new Random();
            int randomPageNumber = rand.Next(RANDOM_PAGE_RANGE_MIN, RANDOM_PAGE_RANGE_MAX);
            int randomJokeNumber = rand.Next(RANDOM_JOKE_RANGE_MIN, RANDOM_JOKE_RANGE_MAX);
            return scraper.ScrapeJoke(randomPageNumber, randomJokeNumber);
        }

    }


    class UserPickJokeProvider : IJokeProvider
    {
        private HtmlJokeScraper scraper;
        public UserPickJokeProvider(HtmlJokeScraper scraper)
        {
            this.scraper = scraper;
        }
        public string getJoke()
        {
            try
            {
                Console.WriteLine("Wybierz stronę z kawałami:");
                int userPageChoice = int.Parse(Console.ReadLine());
                Console.WriteLine("Wybierz numer kawału:");
                int userJokeChoice = int.Parse(Console.ReadLine());

                return scraper.ScrapeJoke(userPageChoice, userJokeChoice);
            } 
            catch (Exception ex)
            {
                return "No jokes for you. User input was invalid.";
            }

        }

    }

    class HtmlJokeScraper
    {
        public string ScrapeJoke(int page, int jokeNumber)
        {
            string jokeUrl = GetPageUrl(page);
            string jokeXPath = GetJokeXPath(jokeNumber);

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(jokeUrl);
            return document.DocumentNode.SelectNodes(jokeXPath).First().InnerText;
        }
        private string GetPageUrl(int pageNumber)
        {
            return $"https://rozrywka.ox.pl/rozne?page={pageNumber}";
        }
        private string GetJokeXPath(int pageNumber)
        {
            return $"/html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[{pageNumber}]/div[1]/div/p";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            HtmlJokeScraper scraper = new HtmlJokeScraper();
            JokeProviderFactory JokeProviderFactory = new JokeProviderFactory(scraper); // TODO poczytać o Factory method!

            while (true)
            {
                DisplayMenuOptions();
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                    case "2":
                        IJokeProvider jokeProvider = JokeProviderFactory.PickJokeProvider(userInput);
                        do
                        {
                            Console.Clear();
                            string joke = jokeProvider.getJoke().Trim();
                            Console.WriteLine(joke);
                        } while (continueCurrentOption());
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
        static void DisplayMenuOptions()
        {
            Console.WriteLine("Wybierz co chcesz zrobić:");
            Console.WriteLine("1. Wylosować żart.");
            Console.WriteLine("2. Wybrać stronę i żart.");
            Console.WriteLine("3. Zamknąć aplikację.");
        }

        static Boolean continueCurrentOption()
        {
            Console.WriteLine("Aby kontynuowac wcisnij dowolny przycisk, aby wrócić do menu naciśnij 1 i Enter.");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.Clear();
                return false;
            }
            return true;
        }
    }
}
