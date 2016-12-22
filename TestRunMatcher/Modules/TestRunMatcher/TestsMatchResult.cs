using LanguageExt;
using TestRunMatcher.Modules.TestRunMatcher.TeamCityResponses;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class TestsMatchResult
	{
		public Lst<TestBuildResult> OldTestResults { get; set; }
		public Lst<TestBuildResult> NewTestResults { get; set; }
		public Lst<TestBuildResult> OnlyNewTestResults { get; set; }
	}

	public class TestBuildResult
	{
		public string BuildName { get; set; }

		public Lst<TestResult> TestResults { get; set; }
	}

	public class TestResult
	{
		public string Id { get; set; }
		public string OccurrenceId { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string Href { get; set; }
		public string WebUrl { get; set; }
		public string Details { get; set; }

		public TestResult With(string Id = null, string OccurrenceId = null, string Name = null, string Status = null, string Href = null, string WebUrl = null, string Details = null) {
			return new TestResult {
				Id = Id ?? this.Id,
				OccurrenceId = OccurrenceId ?? this.OccurrenceId,
				Name = Name ?? this.Name,
				Status = Status ?? this.Status,
				Href = Href ?? this.Href,
				WebUrl = WebUrl ?? this.WebUrl,
				Details = Details ?? this.Details
			};
		}
		public static TestResult FromSingleTestResponse(SingleTestResponse testResponse) {
			return new TestResult {
				OccurrenceId = testResponse.Id,
				Name = testResponse.Name,
				Status = testResponse.Status,
				Href = testResponse.Href
			};
		}

		public static bool GetIsFailed(TestResult testResult) {
			return testResult.Status == "FAILURE";
		}
	}

}
