using System.Text.Json.Serialization;

namespace GitHubArchaeologist.Models.GitHub
{
    // Modeled based on this GitHub export version:
    // {"version":"1.1.0","github_sha":"dc58518c8d827f9deb259b7a2281c5216035a251"}
    public class CommitComment : GitHubEvent
    {
        public string Type { get; set; }
        public string Repository { get; set; }
        public string User { get; set; }
        public string Body { get; set; }
        public string Formatter { get; set; }
        public string Path { get; set; }
        public int? Position { get; set; }

        [JsonPropertyName("commit_id")]
        public string CommitId { get; set; }
    }
}
