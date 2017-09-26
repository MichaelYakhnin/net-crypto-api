namespace TickerMonitor
{
    public interface IPublicApi
    {
        LastDataOrderBook lastDataOrderBook { get; set; }
         string url  { get; set; }
         int Order_num { get; set; }
         void GetNdataFromOrderBook();
        void GetOrderBook(string asset);
        void GetTicker(string asset);
    }
}