using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher.ViewModel
{
	public class CheckResultPageModel
	{
		public CheckResultPageModel(TestsMatchResult buildMatchResult) {
			OldFailedTests = buildMatchResult.OldTestResults.Map(buildResult => new BuildMatchResult {
				BuildName = buildResult.BuildName,
				FailedTests = buildResult.TestResults.OrderBy(testResult => testResult.Name).ToList()
			}).ToList();
			NewFailedTests = buildMatchResult.NewTestResults.Map(buildResult => new BuildMatchResult {
				BuildName = buildResult.BuildName,
				FailedTests = buildResult.TestResults.OrderBy(testResult => testResult.Name).ToList()
			}).ToList();
			OnlyNewFailedTests = buildMatchResult.OnlyNewTestResults.Map(buildResult => new BuildMatchResult {
				BuildName = buildResult.BuildName,
				FailedTests = buildResult.TestResults.OrderBy(testResult => testResult.Name).ToList()
			}).ToList();
		}

		public ICollection<BuildMatchResult> OldFailedTests { get; set; }
		public ICollection<BuildMatchResult> NewFailedTests { get; set; }
		public ICollection<BuildMatchResult> OnlyNewFailedTests { get; set; }

	}

	public class BuildMatchResult
	{

		public string BuildName { get; set; }

		private ICollection<TestResult> _failedTests;
		public ICollection<TestResult> FailedTests {
			get { return _failedTests; }
			set { _failedTests = value;
				TotalFailedTests = value.Count();
			}
		}

		public int TotalFailedTests { get; set; }
	}
}
