using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher
{
    public interface IBuildTestsObtainer
    {
        Either<string, Lst<TestBuildResult>> GetBuildFailedTests(int buildId);
    }
}