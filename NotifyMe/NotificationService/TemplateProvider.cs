namespace NotifyMe
{
    using System.Text.Json;

    public class TemplateProvider
    {
        private readonly Dictionary<string, EmailTemplate> _templates;

        public TemplateProvider(string jsonFilePath)
        {
            string json = File.ReadAllText(jsonFilePath);
            _templates = JsonSerializer.Deserialize<Dictionary<string, EmailTemplate>>(json);
        }

        public EmailTemplate GetTemplate(string templateType)
        {
            if (_templates.TryGetValue(templateType, out var template))
                return template;

            throw new Exception($"Template '{templateType}' not found.");
        }
    }

    public class EmailTemplate
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

}