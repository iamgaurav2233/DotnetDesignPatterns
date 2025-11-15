namespace NotifyMe.Observer
{
    public interface IObserverFactory
    {
        EmailAlertObserver CreateEmailObserver(string username, string email);
        MobileAlertObserver CreateMobileObserver(string username, string number);
    }

}
