using NotifyMe.Observer;
namespace NotifyMe.Observable
{
    public class IphoneObservable : IStocksObservable
    {
        private readonly List<INotificationAlertObserver> _observerList;
        public IphoneObservable()
        {
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
    }
}