using System;
using LanguageExt;
using TestRunMatcher.Modules.TestRunMatcher.TeamCityResponses;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public interface ITeamCityInfoAquirer
	{
		Either<string, Some<TestsReponse>> GetTestResults(int buildId);
		Either<string, Some<TestDetailsResponse>> GetTestDetails(string testOccurrenceId);
		Either<string, Some<BuildDetailsResponse>> GetBuildDetails(int buildId);
		Either<string, Some<BuildDependenciesResponse>> GetBuildDependencies(int buildId);
	}
}