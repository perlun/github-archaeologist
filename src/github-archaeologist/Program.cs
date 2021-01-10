using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Json;
using GitHubArchaeologist.Models;
using GitHubArchaeologist.Models.GitHub;

namespace GitHubArchaeologist
{
    public static class Program
    {
        private const int ExitFailure = 1;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                // TODO: Add an --overview flag or something, which will display the number of events per project only
                // (not details about what days each project was being worked on)
                Console.WriteLine("Syntax: github-archaeologist <data-path> <year>");
                Console.WriteLine();
                Console.WriteLine("'data-path' is expected to be a folder with .json files from a GitHub data export.");
                Console.WriteLine("'year' is the year for which data should be extracted, e.g. 2020.");

                return ExitFailure;
            }

            string dataPath = args[0];

            if (!Int32.TryParse(args[1], out int year))
            {
                Console.WriteLine($"Invalid year specified: ${args[1]}");

                return ExitFailure;
            }

            //
            // Phase 1: Create a YearData object with info about all events registered during a particular year.
            //

            var yearData = new YearData();

            ReadIssueComments(dataPath, year, yearData);
            ReadCommitComments(dataPath, year, yearData);
            ReadIssueEvents(dataPath, year, yearData);

            //
            // Phase 2: display the data gathered in phase 1.
            //
            // TODO: This is one way to display it. Think about other ways to visualize this as well.
            foreach (int month in yearData.Keys)
            {
                Console.WriteLine($"{year}-{month:D2}:");

                foreach ((string projectName, ISet<int> activityDays) in yearData[month].ActivityByProject)
                {
                    Console.WriteLine($"    {String.Join(", ", activityDays)}: {projectName}");
                }
            }

            return 0;
        }

        private static void ReadIssueComments(string dataPath, int year, YearData yearData)
        {
            string[] files = Directory.GetFiles(dataPath, "issue_comments_*.json");

            foreach (string fileName in files)
            {
                string json = File.ReadAllText(fileName);

                var issueComments = JsonSerializer.Deserialize<IList<IssueComment>>(json, JsonSerializerOptions);

                FilterAndAddGitHubEvents(yearData, year, issueComments);
            }
        }

        private static void ReadCommitComments(string dataPath, int year, YearData yearData)
        {
            string[] files = Directory.GetFiles(dataPath, "commit_comments_*.json");

            foreach (string fileName in files)
            {
                string json = File.ReadAllText(fileName);

                var commitComments = JsonSerializer.Deserialize<IList<CommitComment>>(json, JsonSerializerOptions);

                FilterAndAddGitHubEvents(yearData, year, commitComments);
            }
        }

        private static void ReadIssueEvents(string dataPath, int year, YearData yearData)
        {
            string[] files = Directory.GetFiles(dataPath, "issue_events_*.json");

            foreach (string fileName in files)
            {
                string json = File.ReadAllText(fileName);

                var issueEvents = JsonSerializer.Deserialize<IList<IssueEvent>>(json, JsonSerializerOptions);

                FilterAndAddGitHubEvents(yearData, year, issueEvents);
            }
        }

        private static void FilterAndAddGitHubEvents(YearData yearData, int year, IEnumerable<GitHubEvent> gitHubEvents)
        {
            var filteredGitHubEvents = gitHubEvents
                .Where(c => c.CreatedAt.Year == year)
                .ToImmutableList();

            foreach (GitHubEvent gitHubEvent in filteredGitHubEvents)
            {
                int month = gitHubEvent.CreatedAt.Month;
                var monthData = yearData.GetValueOrDefault(month, new MonthData());

                monthData.GitHubEvents.Add(gitHubEvent);

                if (!yearData.ContainsKey(month))
                {
                    yearData[month] = monthData;
                }
            }
        }
    }
}
