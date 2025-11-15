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
            services.AddSingleton<TemplateProvider>(new TemplateProvider("templates.json"));
            services.AddSingleton<TemplateEngine, TemplateEngine>();

            services.AddTransient<EmailService>();
            services.AddTransient<SmsService>();

            services.AddTransient<IStocksObservable, IphoneObservable>();

            services.AddTransient<EmailAlertObserver>();
            services.AddTransient<MobileAlertObserver>();

            var provider = services.BuildServiceProvider();


            var templateProvider = new TemplateProvider("templates.json");
            var templateEngine = new TemplateEngine();
            var iphoneStockObservable = new IphoneObservable();
            var observer1 = new EmailAlertObserver("gaurav", "xyz@gmail.com", iphoneStockObservable, new EmailService(templateProvider, templateEngine));
            var observer2 = new MobileAlertObserver("varuag", "9082510949", iphoneStockObservable, new SmsService(templateProvider, templateEngine));

            iphoneStockObservable.Add(observer1);
            iphoneStockObservable.Add(observer2);

            iphoneStockObservable.AddStockCount(10);
            iphoneStockObservable.RemoveStockCount(10);
            iphoneStockObservable.AddStockCount(100);
        }

    }
}