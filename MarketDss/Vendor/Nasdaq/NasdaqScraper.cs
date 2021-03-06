﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MarketDss.Vendor.Nasdaq
{
    public class NasdaqScraper
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(NasdaqScraper));
        private static readonly Regex NasdaqCompanySymbolRegex =
            new Regex(@"^(.+)\((\S+)\)$", RegexOptions.Compiled);

        private readonly NasdaqScraperConfiguration _configuration;
        private readonly ChromeDriverService _chromeDriverService;
        private readonly ChromeOptions _chromeOptions;

        public NasdaqScraper(NasdaqScraperConfiguration nasdaqScraperConfiguration)
        {
            _configuration = nasdaqScraperConfiguration;
            _chromeDriverService = ChromeDriverService.CreateDefaultService();
            _chromeDriverService.SuppressInitialDiagnosticInformation = true;
            _chromeDriverService.HideCommandPromptWindow = true;
            _chromeOptions = new ChromeOptions();
            _chromeOptions.AddArgument("headless");
        }

        public async Task<IEnumerable<NasdaqDividendItem>> GetUpcomingDividendsAsync()
        {
            var items = new List<NasdaqDividendItem>();
            for(int x=0; x<_configuration.LookupDays; x++)
            {
                var item = await ScrapeDividendDayAsync(DateTime.Today.AddDays(x)).ConfigureAwait(false);
                await Task.Delay(_configuration.RequestDelaySeconds*1000);
                items.AddRange(item);
            }
            return items;
        }

        private async Task<IEnumerable<NasdaqDividendItem>> ScrapeDividendDayAsync(DateTime date)
        {
            var fail = false;
            ChromeDriver driver = null;
            var uriBuilder = new UriBuilder(_configuration.Url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["date"] = date.ToString("yyyy-MMM-dd");
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.Uri.ToString();
            do
            {
                try
                {
                    driver = new ChromeDriver(_chromeDriverService, _chromeOptions);
                    driver.Url = url;
                    var element = new WebDriverWait(driver, TimeSpan.FromSeconds(20))
                        .Until(a => a.FindElement(By.Id("earnings-announcements"))); //common to all days
                    fail = false;
                }
                catch (Exception ex)
                {
                    fail = true;
                    driver.Quit();
                    driver.Dispose();
                    Log.Error($"Exception scraping page for date {date.ToShortDateString()}\r\n{ex}");
                    await Task.Delay(5000);
                }
            }
            while (fail);
                
            var doc = new HtmlDocument();
            doc.LoadHtml(driver.PageSource);
            var nodes = doc.DocumentNode.Descendants("table");
            var dividendTable = nodes
                .FirstOrDefault(node => 
                    node.Attributes.Contains("class") && 
                    node.Attributes["class"].Value.Contains("DividendCalendar"));

            var results = new List<NasdaqDividendItem>();

            if (dividendTable == null)
            {
                Log.Warn($"No dividend calendar table found for {date.ToShortDateString()}");
                return results;
            }
            var rows = dividendTable.SelectNodes(".//tr");
            if(rows == null)
            {
                throw new Exception();
            }
            int rowCount = 0;

            foreach (var row in rows)
            {
                //header
                if (rowCount == 0)
                {
                    var columns = row.SelectNodes(".//th");
                    var columnCount = 0;
                    foreach (var column in columns)
                    {
                        var name = column.InnerText.Trim();
                        switch (columnCount)
                        {
                            case 0:
                                if (name != "Company (Symbol)") throw new Exception();
                                break;
                            case 1:
                                if (name != "Ex-Dividend Date") throw new Exception();
                                break;
                            case 2:
                                if (name != "Dividend") throw new Exception();
                                break;
                            case 3:
                                if (name != "Indicated Annual Dividend") throw new Exception();
                                break;
                            case 4:
                                if (name != "Record Date") throw new Exception();
                                break;
                            case 5:
                                if (name != "Announcement Date") throw new Exception();
                                break;
                            case 6:
                                if (name != "Payment Date") throw new Exception();
                                break;
                            default:
                                throw new Exception();
                        }

                        columnCount++;
                    }
                }
                //body
                else
                {
                    var columns = row.SelectNodes(".//td");
                    var columnCount = 0;
                    var result = new NasdaqDividendItem();
                    foreach (var column in columns)
                    {
                        var value = column.InnerText.Trim();
                        switch (columnCount)
                        {
                            case 0:
                                var companyNameSymbol = value.Trim();
                                if (!NasdaqCompanySymbolRegex.IsMatch(companyNameSymbol))
                                {
                                    Log.Warn($"Could not locate regex match for company name+symbol '{companyNameSymbol}'");   
                                }
                                else
                                {
                                    var matches = NasdaqCompanySymbolRegex.Matches(companyNameSymbol);
                                    result.Symbol = matches[0].Groups[2].Value;
                                    result.Company = matches[0].Groups[1].Value;
                                }
                                break;
                            case 1:
                                DateTime exDividendDate;
                                if (DateTime.TryParse(value, out exDividendDate))
                                {
                                    result.ExDividendDate = exDividendDate;
                                }
                                break;
                            case 2:
                                double dividend;
                                if (Double.TryParse(value, out dividend))
                                {
                                    result.Dividend = dividend;
                                }
                                break;
                            case 3:
                                break;
                            case 4:
                                DateTime recordDate;
                                if (DateTime.TryParse(value, out recordDate))
                                {
                                    result.RecordDate = recordDate;
                                }
                                break;
                            case 5:
                                DateTime announcementDate;
                                if (DateTime.TryParse(value, out announcementDate))
                                {
                                    result.AnnouncementDate = announcementDate;
                                }
                                break;
                            case 6:
                                DateTime paymentDate;
                                if (DateTime.TryParse(value, out paymentDate))
                                {
                                    result.PaymentDate = paymentDate;
                                }
                                break;
                            default:
                                throw new Exception();
                        }

                        columnCount++;
                    }
                    result.FetchedDateUtc = DateTime.UtcNow;
                    results.Add(result);
                }

                rowCount++;
            }
            driver.Quit();
            driver.Dispose();
            Log.Info($"Scraped {results.Count} items for {date.ToShortDateString()}");
            return results;
        }
    }
}
