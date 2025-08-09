using System.Text.Json.Serialization;

namespace BlazorPortfolio.Models
{
    public enum IssueState { Open, Closed, All }

    public class Issue
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public IssueState State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public User User { get; set; }
        public List<User> Assignees { get; set; } = new();
        public List<Label> Labels { get; set; } = new();
    }

    public class User
    {
        public string Login { get; set; }
        public string AvatarUrl { get; set; }
    }

    public class Label
    {
        public string Name { get; set; }
        public string Color { get; set; } // hex sans '#', ex: "0E8A16"
    }

    public static class DateTimeExtensions
    {
        public static DateTime EndOfWeek(this DateTime dt)
        {
            // Samedi fin de semaine (adapte si tu veux Dimanche)
            var diff = DayOfWeek.Saturday - dt.DayOfWeek;
            return dt.Date.AddDays(diff);
        }
    }


    public class WorkflowResponse
    {
        [JsonPropertyName("total_count")]
        public int? TotalCount { get; set; }
    }

    public class WorkflowRunsResponse
    {
        [JsonPropertyName("workflow_runs")]
        public List<WorkflowRun>? WorkflowRuns { get; set; }
    }

    public class WorkflowRun
    {
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        public string? Conclusion { get; set; }
    }
}
