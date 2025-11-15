using NotifyMe.Observer;
namespace NotifyMe.Observable
{
    public interface IStocksObservable
    {
        void Add(INotificationAlertObserver observer);

        void Remove(INotificationAlertObserver observer);

        void NotifySubscribers();

        void AddStockCount(int newStockAdded);

        void RemoveStockCount(int removeStock);

        int GetStockCount();
    }
}