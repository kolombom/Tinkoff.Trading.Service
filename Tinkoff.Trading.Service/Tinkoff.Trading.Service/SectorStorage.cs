using System.Collections.Generic;

namespace Tinkoff.Trading.Service
{
    public static class SectorStorage
    {
        public static Dictionary<string, string> sectorByTicket = new Dictionary<string, string>()
        {
            { "MTG", "Financials" },
            { "MAIL", "Communication Services" },
            { "ZYNE", "Health Care" },
            { "MRK", "Health Care" },
            { "INTC", "Information Technology" },
            { "T", "Communication Services" },
            { "FSLR", "Information Technology" },
            { "BMW@DE", "Consumer Discretionary" },
            { "SBER", "Financials" },
            { "IBM", "Information Technology" },
            { "EBAY", "Consumer Discretionary" },
            { "SEDG", "Information Technology" },
            { "NEM", "Materials" },
            { "GS", "Financials" },
            { "ATVI", "Communication Services" },
            { "BABA", "Consumer Discretionary" },
            { "FXGD", "Materials" },
            { "HOT@DE", "Industrials" },
            { "KR", "Consumer staples" },
        };
    }
}
