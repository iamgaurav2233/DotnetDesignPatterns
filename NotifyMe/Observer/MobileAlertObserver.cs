using System.Globalization;
using System.Runtime.CompilerServices;
using NotifyMe.Observable;
namespace NotifyMe.Observer
{
    public class MobileAlertObserver : INotificationAlertObserver
    {
        private readonly string _message = "Product is in stock hurry up";
        private string _mobileNo;
        private IStocksObservable _observable;

        public MobileAlertObserver(string mobileNo, IStocksObservable observable)
        {
            _mobileNo = mobileNo;
            _observable = observable;
        }

        public void Update()
        {
            SendMail(_mobileNo, _message);
        }

        public void SendMail(string mobileNo, string message)
        {
            Console.WriteLine("SMS sent to : " + mobileNo);
        }
    }
}