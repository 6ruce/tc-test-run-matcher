using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public interface ITestMatcher
	{
		Either<string, TestsMatchResult> MatchTestRuns(Lst<TestBuildResult> oldTestResults, Lst<TestBuildResult> newTestResults);
	}
}