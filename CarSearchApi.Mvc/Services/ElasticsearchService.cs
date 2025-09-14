using CarSearchApi.Mvc.Models;
using Nest;

namespace CarSearchApi.Mvc.Services;

public class ElasticsearchService
{
    private readonly IElasticClient _client;
    private readonly string _indexName;

    public ElasticsearchService(IElasticClient client, IConfiguration configuration)
    {
        _client = client;
        _indexName = configuration["Elasticsearch:DefaultIndex"]!;
    }

    public async Task CreateIndexIfNotExistsAsync()
    {
        var indexExists = await _client.Indices.ExistsAsync(_indexName);
        if (indexExists.Exists) return;

        var createIndexResponse = await _client.Indices.CreateAsync(_indexName, c => c
            .Settings(s => s
                .NumberOfShards(5)
                .NumberOfReplicas(1)
                .Analysis(a => a
                    .TokenFilters(tf => tf
                        .EdgeNGram("autocomplete_edge", e => e.MinGram(2).MaxGram(20))
                    )
                    .Analyzers(an => an
                        .Custom("autocomplete_index", ca => ca.Tokenizer("standard").Filters("lowercase", "asciifolding", "autocomplete_edge"))
                        .Custom("autocomplete_search", ca => ca.Tokenizer("standard").Filters("lowercase", "asciifolding"))
                    )
                )
            )
            .Map<Car>(m => m.Properties(p => p
                .Text(t => t.Name(n => n.Name).Analyzer("autocomplete_index").SearchAnalyzer("autocomplete_search").Fields(f => f.Keyword(k => k.Name("raw"))))
                .Completion(co => co.Name(n => n.NameSuggest).Contexts(ctx => ctx.Category(ca => ca.Name("region"))))
                .SearchAsYouType(s => s.Name(n => n.NameSatype))
                .Number(n => n.Name(num => num.Popularity).Type(NumberType.Integer))
                .Keyword(k => k.Name(n => n.Region))
            ))
        );
        if (!createIndexResponse.IsValid)
        {
            var errorMessage = createIndexResponse.ServerError?.Error?.Reason ?? "Unknown error";
            var debugInfo = createIndexResponse.DebugInformation ?? "No debug information available";
            throw new Exception($"Failed to create index: {errorMessage}. Debug info: {debugInfo}");
        }
    }

    public async Task IndexSampleDataAsync()
    {
        var cars = new List<Car>
        {
            // Indian Market Cars
            new Car {
                Name = "Maruti Suzuki Swift",
                NameSuggest = new CompletionField {
                    Input = new[] { "Maruti Suzuki Swift", "Swift", "Maruti", "Suzuki" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Maruti Suzuki Swift",
                Popularity = 1200,
                Region = "in"
            },
            new Car {
                Name = "Honda City",
                NameSuggest = new CompletionField {
                    Input = new[] { "Honda City", "City", "Honda Cars" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Honda City",
                Popularity = 800,
                Region = "in"
            },
            new Car {
                Name = "Tata Nexon",
                NameSuggest = new CompletionField {
                    Input = new[] { "Tata Nexon", "Nexon", "Tata Motors" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Tata Nexon",
                Popularity = 950,
                Region = "in"
            },
            new Car {
                Name = "Hyundai Creta",
                NameSuggest = new CompletionField {
                    Input = new[] { "Hyundai Creta", "Creta", "Hyundai" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Hyundai Creta",
                Popularity = 1100,
                Region = "in"
            },
            new Car {
                Name = "Mahindra XUV700",
                NameSuggest = new CompletionField {
                    Input = new[] { "Mahindra XUV700", "XUV700", "Mahindra", "XUV" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Mahindra XUV700",
                Popularity = 900,
                Region = "in"
            },
            new Car {
                Name = "Kia Seltos",
                NameSuggest = new CompletionField {
                    Input = new[] { "Kia Seltos", "Seltos", "Kia" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Kia Seltos",
                Popularity = 850,
                Region = "in"
            },
            new Car {
                Name = "Toyota Innova Crysta",
                NameSuggest = new CompletionField {
                    Input = new[] { "Toyota Innova Crysta", "Innova", "Toyota", "Crysta" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Toyota Innova Crysta",
                Popularity = 750,
                Region = "in"
            },
            new Car {
                Name = "Maruti Baleno",
                NameSuggest = new CompletionField {
                    Input = new[] { "Maruti Baleno", "Baleno", "Maruti", "Suzuki" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Maruti Baleno",
                Popularity = 1050,
                Region = "in"
            },
            new Car {
                Name = "Honda Amaze",
                NameSuggest = new CompletionField {
                    Input = new[] { "Honda Amaze", "Amaze", "Honda" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Honda Amaze",
                Popularity = 700,
                Region = "in"
            },
            new Car {
                Name = "Tata Harrier",
                NameSuggest = new CompletionField {
                    Input = new[] { "Tata Harrier", "Harrier", "Tata" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "in" } } }
                },
                NameSatype = "Tata Harrier",
                Popularity = 800,
                Region = "in"
            },

            // US Market Cars
            new Car {
                Name = "Ford Mustang",
                NameSuggest = new CompletionField {
                    Input = new[] { "Ford Mustang", "Mustang", "Ford" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Ford Mustang",
                Popularity = 1300,
                Region = "us"
            },
            new Car {
                Name = "Chevrolet Camaro",
                NameSuggest = new CompletionField {
                    Input = new[] { "Chevrolet Camaro", "Camaro", "Chevrolet", "Chevy" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Chevrolet Camaro",
                Popularity = 1100,
                Region = "us"
            },
            new Car {
                Name = "Tesla Model 3",
                NameSuggest = new CompletionField {
                    Input = new[] { "Tesla Model 3", "Model 3", "Tesla" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Tesla Model 3",
                Popularity = 1500,
                Region = "us"
            },
            new Car {
                Name = "BMW X5",
                NameSuggest = new CompletionField {
                    Input = new[] { "BMW X5", "X5", "BMW" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "BMW X5",
                Popularity = 900,
                Region = "us"
            },
            new Car {
                Name = "Mercedes-Benz C-Class",
                NameSuggest = new CompletionField {
                    Input = new[] { "Mercedes-Benz C-Class", "C-Class", "Mercedes", "Benz" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Mercedes-Benz C-Class",
                Popularity = 850,
                Region = "us"
            },
            new Car {
                Name = "Audi A4",
                NameSuggest = new CompletionField {
                    Input = new[] { "Audi A4", "A4", "Audi" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Audi A4",
                Popularity = 800,
                Region = "us"
            },
            new Car {
                Name = "Toyota Camry",
                NameSuggest = new CompletionField {
                    Input = new[] { "Toyota Camry", "Camry", "Toyota" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Toyota Camry",
                Popularity = 1200,
                Region = "us"
            },
            new Car {
                Name = "Honda Accord",
                NameSuggest = new CompletionField {
                    Input = new[] { "Honda Accord", "Accord", "Honda" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Honda Accord",
                Popularity = 1000,
                Region = "us"
            },
            new Car {
                Name = "Ford F-150",
                NameSuggest = new CompletionField {
                    Input = new[] { "Ford F-150", "F-150", "Ford", "F150" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Ford F-150",
                Popularity = 1600,
                Region = "us"
            },
            new Car {
                Name = "Jeep Wrangler",
                NameSuggest = new CompletionField {
                    Input = new[] { "Jeep Wrangler", "Wrangler", "Jeep" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "us" } } }
                },
                NameSatype = "Jeep Wrangler",
                Popularity = 950,
                Region = "us"
            },

            // European Market Cars
            new Car {
                Name = "Volkswagen Golf",
                NameSuggest = new CompletionField {
                    Input = new[] { "Volkswagen Golf", "Golf", "Volkswagen", "VW" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "eu" } } }
                },
                NameSatype = "Volkswagen Golf",
                Popularity = 1400,
                Region = "eu"
            },
            new Car {
                Name = "BMW 3 Series",
                NameSuggest = new CompletionField {
                    Input = new[] { "BMW 3 Series", "3 Series", "BMW" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "eu" } } }
                },
                NameSatype = "BMW 3 Series",
                Popularity = 1100,
                Region = "eu"
            },
            new Car {
                Name = "Audi Q5",
                NameSuggest = new CompletionField {
                    Input = new[] { "Audi Q5", "Q5", "Audi" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "eu" } } }
                },
                NameSatype = "Audi Q5",
                Popularity = 900,
                Region = "eu"
            },
            new Car {
                Name = "Mercedes-Benz E-Class",
                NameSuggest = new CompletionField {
                    Input = new[] { "Mercedes-Benz E-Class", "E-Class", "Mercedes", "Benz" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "eu" } } }
                },
                NameSatype = "Mercedes-Benz E-Class",
                Popularity = 950,
                Region = "eu"
            },
            new Car {
                Name = "Peugeot 208",
                NameSuggest = new CompletionField {
                    Input = new[] { "Peugeot 208", "208", "Peugeot" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "eu" } } }
                },
                NameSatype = "Peugeot 208",
                Popularity = 750,
                Region = "eu"
            },
            new Car {
                Name = "Renault Clio",
                NameSuggest = new CompletionField {
                    Input = new[] { "Renault Clio", "Clio", "Renault" },
                    Contexts = new Dictionary<string, IEnumerable<string>> { { "region", new[] { "eu" } } }
                },
                NameSatype = "Renault Clio",
                Popularity = 800,
                Region = "eu"
            }
        };
        var bulkResponse = await _client.BulkAsync(b => b.Index(_indexName).IndexMany(cars));
        if (!bulkResponse.IsValid)
        {
            var errorMessage = bulkResponse.ServerError?.Error?.Reason ?? "Unknown error";
            var debugInfo = bulkResponse.DebugInformation ?? "No debug information available";
            throw new Exception($"Failed to index sample data: {errorMessage}. Debug info: {debugInfo}");
        }
    }
    public async Task<List<string>> GetSuggestionsAsync(string userInput, string region = "in")
    {
        var suggestResponse = await _client.SearchAsync<Car>(s => s.Index(_indexName).Size(0)
            .Suggest(su => su
                .Completion("car-suggest", c => c
                    .Prefix(userInput)
                    .Field(f => f.NameSuggest)
                    .Fuzzy(fz => fz.Fuzziness(Fuzziness.Auto))
                    .Size(6)
                )
            )
        );

        // FIX (CS8619): This is a safer, more explicit way to extract suggestions and resolve the nullability warning.
        var suggestions = new List<string>();
        if (suggestResponse.Suggest.ContainsKey("car-suggest"))
        {
            var carSuggestions = suggestResponse.Suggest["car-suggest"];
            suggestions = carSuggestions
                .SelectMany(s => s.Options)
                .Select(o => o.Text)
                .Where(t => t is not null) // Filter out null text entries
                .Select(t => t!)           // Assert to the compiler that the text is not null
                .Distinct()
                .ToList();
        }

        if (suggestions.Any()) return suggestions;

        var satResponse = await _client.SearchAsync<Car>(s => s.Index(_indexName).Size(8)
            .Query(q => q
                .MultiMatch(mm => mm
                    .Query(userInput)
                    .Type(TextQueryType.BoolPrefix)
                    .Fields(f => f.Field(p => p.NameSatype).Field(p => p.Name))
                )
            )
        );
        if (satResponse.Documents.Any()) return satResponse.Documents.Select(d => d.Name).Distinct().ToList();

        var fuzzyResponse = await _client.SearchAsync<Car>(s => s.Index(_indexName).Size(10)
            .Query(q => q
                .FunctionScore(fs => fs
                    .Query(inner => inner
                        .MultiMatch(mm => mm
                            .Query(userInput)
                            .Fields(f => f.Field(p => p.Name, 5).Field("name.raw", 10).Field(p => p.NameSatype, 4))
                            .Type(TextQueryType.BestFields)
                            .Operator(Operator.And)
                            .Fuzziness(Fuzziness.Auto)
                            .FuzzyTranspositions()
                        )
                    )
                    .Functions(ff => ff
                        .FieldValueFactor(fvf => fvf
                            .Field(fld => fld.Popularity)
                            .Modifier(FieldValueFactorModifier.Log1P)
                            .Factor(0.1)
                        )
                    )
                    .BoostMode(FunctionBoostMode.Sum)
                )
            )
        );
        return fuzzyResponse.Documents.Select(d => d.Name).Distinct().ToList();
    }
}