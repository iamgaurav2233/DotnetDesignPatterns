using Nest;

namespace CarSearchApi.Mvc.Models;

public class Car
{
    // FIX: Initialized to default non-null values to satisfy C# null-state analysis.
    public string Name { get; set; } = string.Empty;
    public CompletionField NameSuggest { get; set; } = new(); // C# 9+ shorthand for new CompletionField()
    public string NameSatype { get; set; } = string.Empty;
    public int Popularity { get; set; }
    public string Region { get; set; } = string.Empty;
}