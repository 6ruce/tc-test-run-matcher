using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class TestMatcher : ITestMatcher
	{
		private ITeamCityInfoAquirer _teamCityInfoAquirer;

		public TestMatcher(ITeamCityInfoAquirer teemCityInfoAquirer) {
			_teamCityInfoAquirer = teemCityInfoAquirer;
		}

		public Either<string, TestsMatchResult> MatchTestRuns(Lst<TestBuildResult> oldTestResults, Lst<TestBuildResult> newTestResults) {
			return new TestsMatchResult {
				NewTestResults = newTestResults,
				OldTestResults = oldTestResults,
				OnlyNewTestResults = newTestResults
					.Map(testBuildResult => new TestBuildResult {
						BuildName = testBuildResult.BuildName,
						TestResults = oldTestResults.Find(oldBuildResult => oldBuildResult.BuildName == testBuildResult.BuildName).Match(
							Some: oldBuildresult => testBuildResult.TestResults.Filter(
								buildResult => !oldBuildresult.TestResults.Exists(oldTestResult => oldTestResult.Name == buildResult.Name)),
							None: () => testBuildResult.TestResults).Apply(AddTestDetails)
				})
			};
		}

		private Lst<TestResult> AddTestDetails(Lst<TestResult> tests) {
			return tests.Map(testResult => {
				return
					(from testDetails in _teamCityInfoAquirer.GetTestDetails(testResult.OccurrenceId)
					 select testResult.With(
						 Id: testDetails.Value.Test.Id,
						 Details: testDetails.Value.Details,
						 WebUrl: testDetails.Value.Build.WebUrl))
					 .Match(
						Right: Prelude.identity,
						Left: errMessage => testResult.With(Details: errMessage));
			});
		}
	}
}
