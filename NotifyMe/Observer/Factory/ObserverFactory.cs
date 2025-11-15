using Microsoft.Extensions.DependencyInjection;
namespace NotifyMe.Observer
{
    public class ObserverFactory : IObserverFactory
    {
        private readonly IServiceProvider _provider;

        public ObserverFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public EmailAlertObserver CreateEmailObserver(string username, string email)
        {
            return ActivatorUtilities.CreateInstance<EmailAlertObserver>(_provider, username, email);
        }

        public MobileAlertObserver CreateMobileObserver(string username, string number)
        {
            return ActivatorUtilities.CreateInstance<MobileAlertObserver>(_provider, username, number);
        }
    }

}
