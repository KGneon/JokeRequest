using System;
using System.IO;
using Aspose.Words;

namespace JokeRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            int randomPage = RandomNumberFromTo(1, 59);
            int randomJoke = RandomNumberFromTo(1, 20);
            string pageOfJokes = $"https://rozrywka.ox.pl/rozne?page={randomPage}";
            string xPathOfJoke = $"/html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[{randomJoke}]/div[1]/div/p";
            TextFromPageByHtml(pageOfJokes, xPathOfJoke);
        }

        // /html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[1]/div[1]/div/p/text()
        // /html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[2]/div[1]/div/p
        // /html/body/div[1]/main/div[6]/div/div[1]/div[3]/div[2]/div[1]/div/p

        static void TextFromPageByHtml(string page, string xPath)
        {
            HtmlWeb web = new HtmlWeb();
            //this could be any web page
            HtmlDocument document = web.Load(page);
            //HtmlNode[] nodes = document.DocumentNode.SelectNodes(xPath).ToArray();

            HtmlNode nodex = document.DocumentNode.SelectNodes(xPath).First();

            string myNewString = nodex.SelectNodes(xPath).First().InnerText;
            Console.WriteLine(myNewString);
        }

        static int RandomNumberFromTo(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to + 1);
        }
    }
}
