using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using TestRunMatcher.Modules.TestRunMatcher.TeamCityResponses;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class BuildTestsObtainer : IBuildTestsObtainer
	{
		private ITeamCityInfoAquirer _teamCityInfoAquirer;

		public BuildTestsObtainer(ITeamCityInfoAquirer infoAquirer) {
			_teamCityInfoAquirer = infoAquirer;
		}

		public Either<string, Lst<TestBuildResult>> GetBuildFailedTests(int buildId) {
			return _teamCityInfoAquirer.GetBuildDependencies(buildId).Bind(dependencies => GetBuildTests(buildId, dependencies));
		}

		private Either<string, Lst<TestBuildResult>> GetBuildTests(int buildId, BuildDependenciesResponse dependenciesResponse) {
			if (!dependenciesResponse.Build.Any()) {
				return GetSingleBuildTests(buildId).Map(buildTests => buildTests.Match(
					Some: tests => List.create(tests),
					None: List.empty<TestBuildResult>));
			}
			return
				Prelude.toList(dependenciesResponse.Build)
					.Fold(Prelude.Right<string, Lst<TestBuildResult>>(List.empty<TestBuildResult>()),
						(acc, dependency) => acc.LiftM2(GetSingleBuildTests(dependency.Id),
							(testResults, result) => Prelude.Right<string, Lst<TestBuildResult>>(result.Match(
								Some: res => List.add(testResults, res), 
								None: () => testResults))));
		}

		private Either<string, Option<TestBuildResult>> GetSingleBuildTests(int buildId) {
			return
				from testsResponse in _teamCityInfoAquirer.GetTestResults(buildId)
				from buildName in GetBuildName(buildId)
				select GetBuildTestResult(buildName, testsResponse);
		}

		private Option<TestBuildResult> GetBuildTestResult(string buildName, TestsReponse testsResponse) {
			IEnumerable<TestResult> failedTests =
				testsResponse.TestOccurrence.Map(TestResult.FromSingleTestResponse) .Filter(TestResult.GetIsFailed);
			if (failedTests.Any() && !buildName.Contains("Cyrillic")) {
				return Some.Create(new TestBuildResult {
					BuildName = buildName,
					TestResults = Prelude.toList(failedTests)
				});
			}
			return Option<TestBuildResult>.None;
		}

		private Either<string, string> GetBuildName(int buildId) {
			return _teamCityInfoAquirer.GetBuildDetails(buildId).Map(details => details.Value.BuildType.ProjectName);
		}

	}
}
