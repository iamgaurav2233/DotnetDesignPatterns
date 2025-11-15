using NotifyMe.Observer;
namespace NotifyMe.Observable
{
    public class IphoneObservable : IStocksObservable
    {
        private int _stockCount;
        private readonly List<INotificationAlertObserver> _observerList;
        public IphoneObservable()
        {
            _stockCount = 0;
            _observerList = new List<INotificationAlertObserver>();
        }
        public void Add(INotificationAlertObserver observer)
        {
            _observerList.Add(observer);
        }

        public void Remove(INotificationAlertObserver observer)
        {
            _observerList.Remove(observer);
        }

        public void NotifySubscribers()
        {
            foreach (INotificationAlertObserver observer in _observerList)
            {
                observer.Update();
            }
        }

        public void AddStockCount(int newStockAdded)
        {
            _stockCount = _stockCount + newStockAdded;
            if (_stockCount == newStockAdded)
            {
                NotifySubscribers();
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