using System.Runtime.CompilerServices;

namespace NotifyMe.NotificationService
{
    public interface INotifyService
    {
        public void Notify(string userType, Dictionary<string, string> data);
    }
}