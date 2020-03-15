using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebFund.Data;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text;
using System.Globalization;

namespace WebFund.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FundContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FundContext>>()))
            {
                // Look for any movies.
                if (context.Funds.Any())
                {
                    return;   // DB has been seeded
                }

                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                foreach (var fund in RunCrawler(httpClientFactory).Result)
                {
                    context.Funds.AddRange(fund);
                }

                context.SaveChanges();
            }
        }

        static async Task<Fund[]> RunCrawler(IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient();
            // client.DefaultRequestHeaders.Accept.Clear();
            // client.DefaultRequestHeaders.Accept.Add(
            //     new MediaTypeWithQualityHeaderValue("text/html");
            // client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var response = await client.GetAsync("http://fund.eastmoney.com/trade/qdii.html");

            Stream stream = await response.Content.ReadAsStreamAsync();

            StreamReader read = new StreamReader(stream, Encoding.GetEncoding("gb2312"));

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.Load(read);

            //Console.Write(pageDocument);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"tblite_qdii\"]/tbody/tr");
            Fund[] funds = new Fund[nodes.Count];
            int i = 0;

            foreach (HtmlNode node in nodes)
            {
                funds[i] = new Fund();
                
                var columes = node.SelectNodes("child::td");
                var spans = columes[2].SelectNodes("child::span");

                funds[i].code = columes[0].InnerText;  // code
                funds[i].name = columes[1].InnerText;
                funds[i].value = Convert.ToDouble(spans[0].InnerText);
                funds[i].UpdateDate = DateTime.ParseExact("2020-" + spans[1].InnerText, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                funds[i].ChangeInDay = Convert.ToDouble(columes[3].InnerText.Replace('%', '0'));
                funds[i].ChangeInWeek = Convert.ToDouble(columes[4].InnerText.Replace('%', '0'));
                funds[i].ChangeInMonth = Convert.ToDouble(columes[5].InnerText.Replace('%', '0'));

                i++;
            }

            Array.Sort(funds, (x1, x2) => x1.ChangeInMonth.CompareTo(x2.ChangeInMonth));

            foreach (var fund in funds)
            {
                Console.WriteLine(
                    "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                    fund.code,
                    fund.name,
                    fund.value,
                    fund.UpdateDate,
                    fund.ChangeInDay,
                    fund.ChangeInWeek,
                    fund.ChangeInMonth);
            }

            return funds;
        }
    }
}