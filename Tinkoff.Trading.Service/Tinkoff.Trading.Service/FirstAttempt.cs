using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace Tinkoff.Trading.Service
{
    public class FirstAttempt
    {
        private readonly Context _context;

        public FirstAttempt(string token)
        {
            var connection = ConnectionFactory.GetConnection(token);
            _context = connection.Context;
        }

        public async Task GetPortfolio()
        {
            var sectorPercent = new Dictionary<string, decimal>();
            var accountIds = await _context.AccountsAsync();
            var portfolio = await _context.PortfolioAsync(accountIds.Last().BrokerAccountId);
            decimal totalAmount = 0;
            var usd = portfolio.Positions.First(p => p.Ticker == "USD000UTSTOM");
            var usdPrice = (usd.Balance * usd.AveragePositionPrice.Value + usd.ExpectedYield.Value) / usd.Balance;
            var euro = portfolio.Positions.First(p => p.Ticker == "EUR_RUB__TOM");
            var euroPrice = (euro.Balance * euro.AveragePositionPrice.Value + euro.ExpectedYield.Value) / euro.Balance;
            totalAmount -= usd.Balance * usdPrice + euro.Balance * euroPrice;
            foreach (var position in portfolio.Positions)
            {
                decimal k = 1;
                if (position.AveragePositionPrice.Currency == Currency.Usd) k = usdPrice;
                if (position.AveragePositionPrice.Currency == Currency.Eur) k = euroPrice;
                totalAmount += (position.Balance * position.AveragePositionPrice.Value + position.ExpectedYield.Value) * k;
            }

            Console.WriteLine(totalAmount);
            foreach (var position in portfolio.Positions)
            {
                decimal k = 1;
                if (position.AveragePositionPrice.Currency == Currency.Usd) k = usdPrice;
                if (position.AveragePositionPrice.Currency == Currency.Eur) k = euroPrice;

                var percent = (position.Balance * position.AveragePositionPrice.Value + position.ExpectedYield.Value) * k / totalAmount;
                Console.WriteLine(position.Name + " " + position.Ticker + " Percent: " + decimal.Round(percent, 3));

                if (SectorStorage.sectorByTicket.ContainsKey(position.Ticker))
                {
                    var sector = SectorStorage.sectorByTicket[position.Ticker];
                    if (sectorPercent.ContainsKey(sector))
                    {
                        sectorPercent[sector] += percent;
                    }
                    else
                    {
                        sectorPercent.Add(sector, percent);
                    }
                }
            }
            Console.WriteLine();
            foreach(var s in sectorPercent.OrderBy(p => p.Value))
            {
                Console.WriteLine(s.Key + " " + decimal.Round(s.Value*100, 1));
            }
        }
    }
}
