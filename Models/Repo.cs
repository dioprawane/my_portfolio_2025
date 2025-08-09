// Models/Repo.cs
using System.Text.Json.Serialization;

namespace BlazorPortfolio.Models
{

    public class Repo
    {
        public string? Name { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }

        public bool? Private { get; set; }
        public string? Visibility { get; set; }
    }
}
