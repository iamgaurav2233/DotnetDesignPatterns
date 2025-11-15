namespace NotifyMe.NotificationService
{
    public class TemplateEngine
    {
        public string Render(string template, Dictionary<string, string> data)
        {
            foreach (var item in data)
            {
                template = template.Replace("{" + item.Key + "}", item.Value);
            }
            return template;
        }
    }

}
