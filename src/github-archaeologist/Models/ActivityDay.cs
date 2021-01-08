using System.Collections.Generic;
using GitHubArchaeologist.Models.GitHub;

namespace GitHubArchaeologist.Models
{
    /// <summary>
    /// An activity day represents a day with at least one activity registered in the GitHub data. (e.g. creating an
    /// issue, writing an issue comment, creating/merging/closing a pull request, etc.)
    /// </summary>
    public class ActivityDay
    {
        public IDictionary<string, IList<GitHubEvent>> ActivityByProject { get; } = new Dictionary<string, IList<GitHubEvent>>();
    }
}
