namespace ExternalServices
{
    public interface IPublicApi
    {
        LastDataOrderBook Get25dataFromOrderBook();
        void GetOrderBook(string asset);
        void GetTicker(string asset);
    }
}