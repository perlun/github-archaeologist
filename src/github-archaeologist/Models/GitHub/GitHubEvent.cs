using System;
using System.Text.Json.Serialization;

namespace GitHubArchaeologist.Models.GitHub
{
    /// <summary>
    /// Common interface for all GitHub-controlled models.
    /// </summary>
    public abstract class GitHubEvent
    {
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        public Uri Url { get; set; }

        public string Project => String.Join('/', Url.AbsolutePath.Split("/")[1..3]);

        public override string ToString()
        {
            return $"{Url}, {CreatedAt}";
        }
    }
}
