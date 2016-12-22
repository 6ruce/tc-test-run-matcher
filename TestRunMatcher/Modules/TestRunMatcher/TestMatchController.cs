using LanguageExt;
using System;
using System.Text.RegularExpressions;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class TestMatchController : ITestMatchController
	{
		private ITestMatcher _testResultMatcher;
		private IBuildTestsObtainer _buildTestsObtainer;

		public TestMatchController(ITestMatcher testResultsMatcher, IBuildTestsObtainer buidTestsObtainer) {
			_testResultMatcher = testResultsMatcher;
			_buildTestsObtainer = buidTestsObtainer;
		}

		public Either<string, TestsMatchResult> MatchTestRuns(Some<string> oldTestsRunUrl, Some<string> newTestsRunUrl) {
			Func<string, Either<string, Lst<TestBuildResult>>> getBuildTests = testsRunUrl =>
				Uris.ParseUri(testsRunUrl).Bind(ParseBuildId).Bind(_buildTestsObtainer.GetBuildFailedTests);
			return getBuildTests(oldTestsRunUrl)
				.LiftM2(getBuildTests(newTestsRunUrl), _testResultMatcher.MatchTestRuns);
		}

		private Either<string, int> ParseBuildId(Uri buildUri) {
			var match = Regex.Match(buildUri.Query, @"buildId=(\d+)\D");
			if (match.Success) {
				return Prelude.parseInt(match.Groups[1].Value).Match(
					Some: Prelude.Right<string, int>,
					None: () => Prelude.Left<string, int>("Can't parse buildId. Build Id must be a number.")
				);
			}
			return Prelude.Left<string, int>("Wrong test run Uri. Uri should contain `buildId` parameter");
		}
	}
}
