namespace GitHubArchaeologist.Models.GitHub
{
    // Modeled based on this GitHub export version:
    // {"version":"1.1.0","github_sha":"dc58518c8d827f9deb259b7a2281c5216035a251"}
    public class IssueComment : GitHubEvent
    {
        public string Type { get; set; }
        public string Issue { get; set; }
        public string Body { get; set; }
        public string Formatter { get; set; }
    }
}
