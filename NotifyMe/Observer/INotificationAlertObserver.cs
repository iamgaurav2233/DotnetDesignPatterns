namespace NotifyMe.Observer
{
    public interface INotificationAlertObserver
    {
        void Update();
        void SendMail(string emailId, string message);
    }
}