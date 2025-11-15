namespace NotifyMe.NotificationService
{
    public class EmailService : INotifyService
    {
        private readonly TemplateProvider _provider;
        private readonly TemplateEngine _engine;

        public EmailService(TemplateProvider provider, TemplateEngine engine)
        {
            _provider = provider;
            _engine = engine;
        }

        public void Notify(string userType, Dictionary<string, string> data)
        {
            var template = _provider.GetTemplate(userType);

            string subject = _engine.Render(template.subject, data);
            string body = _engine.Render(template.body, data);

            // Simulate email sending
            Console.WriteLine("=== Sending Email ===");
            Console.WriteLine("Subject: " + subject);
            Console.WriteLine("Body:\n" + body);
            Console.WriteLine("=====================");
        }
    }

}
