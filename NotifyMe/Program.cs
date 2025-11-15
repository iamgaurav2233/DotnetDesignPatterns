using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NotifyMe.Observable;
using NotifyMe.Observer;
using NotifyMe.NotificationService;
namespace NotifyMe
{
    public class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();
            Program.RegisterServices(services);

            var provider = services.BuildServiceProvider();

            var observable = provider.GetRequiredService<IStocksObservable>();

            var factory = provider.GetRequiredService<IObserverFactory>();

            var observer1 = factory.CreateEmailObserver("gaurav", "xyz@gmail.com");
            var observer2 = factory.CreateMobileObserver("varuag", "9082510949");

            observable.Add(observer1);
            observable.Add(observer2);

            observable.AddStockCount(10);
            observable.RemoveStockCount(10);
            observable.AddStockCount(100);

        }
        private static void RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<TemplateProvider>(new TemplateProvider("templates.json"));
            services.AddSingleton<TemplateEngine, TemplateEngine>();

            services.AddTransient<INotifyService, EmailService>();
            services.AddTransient<INotifyService, SmsService>();

            services.AddSingleton<IStocksObservable, IphoneObservable>();

            services.AddTransient<EmailAlertObserver>();
            services.AddTransient<MobileAlertObserver>();

            services.AddSingleton<IObserverFactory, ObserverFactory>();
        }

    }
}