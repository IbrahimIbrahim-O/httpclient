namespace httpclient.ServiceInterface
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>> GetStockPriceQuote(string company);
    }
}
