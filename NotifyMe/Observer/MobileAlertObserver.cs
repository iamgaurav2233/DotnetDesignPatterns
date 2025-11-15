using NotifyMe.NotificationService;
using NotifyMe.Observable;
namespace NotifyMe.Observer
{
    public class MobileAlertObserver : INotificationAlertObserver
    {
        private readonly string _name;
        private readonly string _mobileNo;
        private readonly IStocksObservable _observable;
        private readonly INotifyService _smsService;

        public MobileAlertObserver(string name, string mobileNo, IStocksObservable observable, INotifyService smsService)
        {
            _name = name;
            _mobileNo = mobileNo;
            _observable = observable;
            _smsService = smsService;
        }

        public void Update()
        {
            var infoDictionary = new Dictionary<string, string>();
            infoDictionary["name"] = _name;
            Type type = _observable.GetType();
            infoDictionary["product"] = type.ToString().Replace("Observable", "").Replace("NotifyMe..", "");
            _smsService.Notify("BACK_IN_STOCK", infoDictionary);
        }
    }
}