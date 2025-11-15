using System.Globalization;
using System.Runtime.CompilerServices;
using NotifyMe.Observable;
namespace NotifyMe.Observer
{
    public class EmailAlertObserver : INotificationAlertObserver
    {
        private readonly string _message = "Product is in stock hurry up";
        private readonly string _emailId;
        private IStocksObservable _observable;

        public EmailAlertObserver(string emailId, IStocksObservable observable)
        {
            _emailId = emailId;
            _observable = observable;
        }

        public void Update()
        {
            SendMail(_emailId, _message);
        }

        public void SendMail(string emailId, string message)
        {
            Console.WriteLine("Mail sent to : " + emailId);
        }
    }
}