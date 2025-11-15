using NotifyMe.NotificationService;
using NotifyMe.Observable;
namespace NotifyMe.Observer
{
    public class EmailAlertObserver : INotificationAlertObserver
    {
        private readonly string _name;
        private readonly string _emailId;
        private readonly IStocksObservable _observable;
        private readonly INotifyService _emailService;

        public EmailAlertObserver(string name, string emailId, IStocksObservable observable, INotifyService emailService)
        {
            _name = name;
            _emailId = emailId;
            _observable = observable;
            _emailService = emailService;
        }

        public void Update()
        {
            var infoDictionary = new Dictionary<string, string>();
            infoDictionary["name"] = _name;
            infoDictionary["email"] = _emailId;
            Type type = _observable.GetType();
            infoDictionary["product"] = type.ToString().Replace("Observable", "").Replace("NotifyMe..", "");
            _emailService.Notify("BACK_IN_STOCK", infoDictionary);
        }
    }
}