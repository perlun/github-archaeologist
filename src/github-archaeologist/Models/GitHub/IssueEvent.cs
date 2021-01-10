using System.Text.Json.Serialization;

namespace GitHubArchaeologist.Models.GitHub
{
    // Modeled based on this GitHub export version:
    // {"version":"1.1.0","github_sha":"dc58518c8d827f9deb259b7a2281c5216035a251"}
    public class IssueEvent : GitHubEvent
    {
        public string Type { get; set; }

        [JsonPropertyName("pull_request")]
        public string PullRequest { get; set; }

        public string Actor { get; set; }
        public string Event { get; set; }
        public string Subject { get; set; }
    }
}
