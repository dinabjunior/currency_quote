namespace Br.Com.Company.CurrencyQuote.Data.Support.Dtos
{
    public class ExchangeRatesApiResponseModel
    {
        public bool Success { get; set; }
        public string Base { get; set; }
        public Rate Rates { get; set; }

        public class Rate
        {
            public decimal BRL { get; set; }
        }
    }
}
