namespace NotifyMe.Observable
{
    public class StockManager
    {
        private int _stockCount;
        private readonly IStocksObservable _observable;
        public StockManager(IStocksObservable observable)
        {
            _stockCount = 0;
            _observable = observable;
        }
        public void AddStockCount(int newStockAdded)
        {
            _stockCount = _stockCount + newStockAdded;
            if (_stockCount == newStockAdded)
            {
                _observable.NotifySubscribers();
            }
        }

        public void RemoveStockCount(int removeStock)
        {
            _stockCount = _stockCount - removeStock;
        }

        public int GetStockCount()
        {
            return _stockCount;
        }
    }
}