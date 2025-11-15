using NotifyMe.Observable;
using NotifyMe.Observer;
namespace NotifyMe
{
    public class Program
    {
        public static void Main()
        {
            var iphoneStockObservable = new IphoneObservable();

            var observer1 = new EmailAlertObserver("xyz@gmail.com", iphoneStockObservable);
            var observer2 = new MobileAlertObserver("9082510949", iphoneStockObservable);

            iphoneStockObservable.Add(observer1);
            iphoneStockObservable.Add(observer2);

            iphoneStockObservable.SetStockCount(10);
            iphoneStockObservable.SetStockCount(-10);
            iphoneStockObservable.SetStockCount(100);
        }
    }
}