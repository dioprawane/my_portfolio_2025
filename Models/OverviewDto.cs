using System.Text.Json.Serialization;

namespace BlazorPortfolio.Models
{
    public class OverviewDto
    {
        [JsonPropertyName("owner")] public string Owner { get; set; } = "";
        [JsonPropertyName("kpis")] public KpisDto Kpis { get; set; } = new();
        [JsonPropertyName("collab")] public CollabDto Collab { get; set; } = new();
        [JsonPropertyName("actions30d")] public ActionsDto Actions30d { get; set; } = new();
        [JsonPropertyName("languages")] public List<LanguageDto> Languages { get; set; } = new();
        [JsonPropertyName("topStars")] public List<TopStarDto> TopStars { get; set; } = new();
        [JsonPropertyName("commitsByWeek")] public List<CommitWeekDto> CommitsByWeek { get; set; } = new();
        [JsonPropertyName("commitsByMonth")] public List<CommitMonthDto> CommitsByMonth { get; set; } = new();
        [JsonPropertyName("fetchedAt")] public DateTime FetchedAt { get; set; }
        //[JsonPropertyName("repos")] public RepoDto Repos { get; set; } = new();
        //[JsonPropertyName("repos")] public List<RepoDto> Repos { get; set; } = new();

    }

    public class KpisDto
    {
        [JsonPropertyName("repos")] public int Repos { get; set; }
        [JsonPropertyName("publicRepos")] public int PublicRepos { get; set; }
        [JsonPropertyName("privateRepos")] public int PrivateRepos { get; set; }
        [JsonPropertyName("totalStars")] public int TotalStars { get; set; }
        [JsonPropertyName("releases")] public int Releases { get; set; }

        // Nouveau : total des commits sur 12 semaines
        [JsonPropertyName("commits12wTotal")] public int Commits12wTotal { get; set; }
        [JsonPropertyName("commits12mTotal")] public int Commits12mTotal { get; set; }

        // Nouveau : langage principal
        [JsonPropertyName("topLanguage")] public TopLanguageDto TopLanguage { get; set; } = new();
    }

    public class TopLanguageDto
    {
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("bytes")] public long Bytes { get; set; }
        [JsonPropertyName("sharePct")] public double SharePct { get; set; }
    }

    // 👇 Nouveau bloc retourné par le backend
    public class CollabDto
    {
        [JsonPropertyName("solo")] public int Solo { get; set; }
        [JsonPropertyName("multi")] public int Multi { get; set; }
        [JsonPropertyName("unknown")] public int Unknown { get; set; }
        [JsonPropertyName("soloSharePct")] public double SoloSharePct { get; set; }
        [JsonPropertyName("known")] public int Known { get; set; }
    }

    public class ActionsDto
    {
        [JsonPropertyName("workflows")] public int Workflows { get; set; }
        [JsonPropertyName("runs")] public int Runs { get; set; }
        [JsonPropertyName("success")] public int Success { get; set; }
        [JsonPropertyName("failure")] public int Failure { get; set; }
        [JsonPropertyName("passRate")] public double PassRate { get; set; }
        [JsonPropertyName("windowDays")] public int WindowDays { get; set; }
    }

    public class LanguageDto
    {
        [JsonPropertyName("language")] public string Language { get; set; } = "";
        [JsonPropertyName("bytes")] public long Bytes { get; set; }
    }

    public class TopStarDto
    {
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("stars")] public int Stars { get; set; }
    }

    public class CommitWeekDto
    {
        [JsonPropertyName("week_end")] public string WeekEnd { get; set; } = ""; // ISO yyyy-mm-dd
        [JsonPropertyName("count")] public int Count { get; set; }
    }

    public class CommitMonthDto
    {
        [JsonPropertyName("month_end")] public string MonthEnd { get; set; } = ""; // "YYYY-MM-DD"
        [JsonPropertyName("count")] public int Count { get; set; }
    }

    // DTO minimal(mappe les champs GitHub qu’on utilise ci-dessous)
    /*public sealed class RepoDto
    {
        public string Name { get; set; } = "";
        public string HtmlUrl { get; set; } = "";     // "html_url"
        public string Visibility { get; set; } = "";  // "public" | "private"
        public int StargazersCount { get; set; }      // "stargazers_count"
        public int ForksCount { get; set; }           // "forks_count"
        public int OpenIssuesCount { get; set; }      // "open_issues_count"
        public string? Language { get; set; }
        public string DefaultBranch { get; set; } = "main";
        public string? LicenseName { get; set; }      // map "license?.name"
        public DateTimeOffset UpdatedAt { get; set; } // "updated_at"
        public List<string> Topics { get; set; } = new(); // "topics"
        public OwnerDto Owner { get; set; } = new();
        public sealed class OwnerDto { public string Login { get; set; } = ""; public string AvatarUrl { get; set; } = ""; }
    }*/

    public sealed class RepoDto
    {
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("html_url")] public string HtmlUrl { get; set; } = "";
        [JsonPropertyName("visibility")] public string Visibility { get; set; } = "";
        [JsonPropertyName("stargazers_count")] public int StargazersCount { get; set; }
        [JsonPropertyName("forks_count")] public int ForksCount { get; set; }
        [JsonPropertyName("open_issues_count")] public int OpenIssuesCount { get; set; }
        [JsonPropertyName("language")] public string? Language { get; set; }
        [JsonPropertyName("default_branch")] public string DefaultBranch { get; set; } = "main";
        [JsonPropertyName("license")] public LicenseDto? License { get; set; }
        [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
        [JsonPropertyName("pushed_at")] public DateTimeOffset PushedAt { get; set; }
        [JsonPropertyName("topics")] public List<string> Topics { get; set; } = new();
        [JsonPropertyName("owner")] public OwnerDto Owner { get; set; } = new();

        public sealed class LicenseDto
        {
            [JsonPropertyName("name")] public string? Name { get; set; }
        }

        public sealed class OwnerDto
        {
            [JsonPropertyName("login")] public string Login { get; set; } = "";
            [JsonPropertyName("avatar_url")] public string AvatarUrl { get; set; } = "";
            [JsonPropertyName("html_url")] public string HtmlUrl { get; set; } = "";
        }
    }

}
