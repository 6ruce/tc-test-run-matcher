using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher
{
    public interface ITestMatchController
    {
        Either<string, TestsMatchResult> MatchTestRuns(Some<string> oldTestsRunUrl, Some<string> newTestsRunUrl);
    }
}