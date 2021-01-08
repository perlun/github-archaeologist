using System.Collections.Generic;
using System.Collections.Immutable;
using GitHubArchaeologist.Models.GitHub;

namespace GitHubArchaeologist.Models
{
    public class MonthData
    {
        // TODO: re-add these if we ever find the need to work with individual event types again. If we reimplement
        // them, they should be based on the aggregate data in GitHubEvents
        // public List<IssueComment> IssueComments { get; } = new();
        // public List<CommitComment> CommitComments { get; } = new();

        public List<GitHubEvent> GitHubEvents { get; } = new();

        public IDictionary<int, ActivityDay> ActivityDays => activityDays ??= DetermineActivityDays();

        private IDictionary<int, ActivityDay> activityDays;

        public IDictionary<string, ISet<int>> ActivityByProject =>
            activityByProject ??= DetermineActivityByProject();

        private IDictionary<string, ISet<int>> activityByProject;

        private IDictionary<int, ActivityDay> DetermineActivityDays()
        {
            var result = ImmutableDictionary.CreateBuilder<int, ActivityDay>();

            foreach (GitHubEvent githubActivity in GitHubEvents)
            {
                if (!result.ContainsKey(githubActivity.CreatedAt.Day))
                {
                    result[githubActivity.CreatedAt.Day] = new ActivityDay();
                }

                string project = githubActivity.Project;

                if (!result[githubActivity.CreatedAt.Day].ActivityByProject.ContainsKey(project))
                {
                    result[githubActivity.CreatedAt.Day].ActivityByProject[project] = new List<GitHubEvent>();
                }

                result[githubActivity.CreatedAt.Day].ActivityByProject[project].Add(githubActivity);
            }

            return result.ToImmutable();
        }

        private IDictionary<string, ISet<int>> DetermineActivityByProject()
        {
            var result = ImmutableDictionary.CreateBuilder<string, ISet<int>>();

            // Using the ActivityDays structure here is arguably less efficient, but it has the advantage of avoiding to
            // have to duplicate the logic for various kind of activity events in multiple methods here.
            foreach ((int day, ActivityDay activityDay) in ActivityDays)
            {
                foreach (string project in activityDay.ActivityByProject.Keys)
                {
                    if (!result.ContainsKey(project))
                    {
                        result[project] = new HashSet<int>();
                    }

                    result[project].Add(day);
                }
            }

            return result.ToImmutable();
        }
    }
}
