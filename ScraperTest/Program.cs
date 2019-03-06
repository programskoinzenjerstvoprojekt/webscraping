using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScraperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();
            Console.ReadKey();
        }

        private static async void GetHtmlAsync()
        {
            var url = "https://www.kaptolcinema.hr/kupi-ulaznicu-2532";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("id", "")
                .Equals("all-movies-container")).ToList();

            var ProductListItems = ProductsHtml[0].Descendants("div")
                .Where(node => node.GetAttributeValue("id", "")
                .Contains("movie_")).ToList();
            
            foreach(var ProductListItem in ProductListItems)
            {
                Console.WriteLine(ProductListItem.GetAttributeValue("id", ""));
                Console.WriteLine(ProductListItem.Descendants("a")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("title")).FirstOrDefault().InnerText
                    );
                var onclick = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("onclick", "");
                //Console.WriteLine(ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("onclick", ""));

                Console.WriteLine(ProductListItem.Descendants("a")
                    .Where(node => node.GetAttributeValue("onclick", "")
                    .Equals(onclick)).LastOrDefault().InnerText
                    );
                Console.WriteLine();


            }

            Console.WriteLine();
        }
    }
}
