﻿using SimpleWebScraper.Builders;
using SimpleWebScraper.Data;
using SimpleWebScraper.Workers;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("SimpleWebScraper.Test.Unit")]

namespace SimpleWebScraper
{
    class Program
    {
        private const string Method = "search";


        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter which city you would like to scrape information from");
                var craigsListCity = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Please enter the CraigsList category that you would like to scrape");
                var craigsListCategoryName = Console.ReadLine() ?? string.Empty;

                using (WebClient client = new WebClient())
                {
                    string content = client.DownloadString($"http://{craigsListCity.Replace(" ", string.Empty)}.craigslist.org/{Method}/{craigsListCategoryName}");

                    ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                        .WithData(content)
                        .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlnk\"">(.*?)</a>")
                        .WithRegexOption(RegexOptions.ExplicitCapture)
                        .WithParts(new ScrapeCriteriaPartBuilder()
                            .WithRegex(@">(.*?)</a>")
                            .WithRegexOption(RegexOptions.Singleline)
                            .Build())
                        .WithParts(new ScrapeCriteriaPartBuilder()
                            .WithRegex(@"href=\""(.*?)\""")
                            .WithRegexOption(RegexOptions.Singleline)
                            .Build())
                        .Build();

                    Scraper scraper = new Scraper();

                    var scrapedElements = scraper.Scrape(scrapeCriteria);

                    if (scrapedElements.Any())
                    {
                        foreach (var scrapeElement in scrapedElements)
                        {
                            Console.WriteLine(scrapeElement);
                        }
                    }
                    else
                        Console.WriteLine("There were no matches for the specified scrape criteria");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            
        }
    }
}
