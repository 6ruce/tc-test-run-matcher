using LanguageExt;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using TestRunMatcher.Modules.TestRunMatcher.TeamCityResponses;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class TeamCityInfoAquirer : ITeamCityInfoAquirer
	{
		private ITeamCityConfigurationLoader _teamCityConfigurationLoader;

		private Either<string, RestClient> RestClient {
			get {
				return _teamCityConfigurationLoader.LoadConfig().Map(config => {
					Uri tcUri = config.TeamCityUri;
					string tcUrl = string.Format("{0}://{1}:{2}/app/rest",
						tcUri.Scheme, tcUri.Host, tcUri.Port);
					var restClient = new RestClient(tcUrl);
					restClient.Authenticator = new HttpBasicAuthenticator(
						config.UserName, config.Password);
					return restClient;
				});
			}
		}

		public TeamCityInfoAquirer(ITeamCityConfigurationLoader configLoader) {
			_teamCityConfigurationLoader = configLoader;
		}

		public Either<string, Some<TestsReponse>> GetTestResults(int buildId) {
			string locator = string.Format("build:(id:{0})", buildId);
			return RequestTests<TestsReponse>(locator);
		}

		public Either<string, Some<BuildDependenciesResponse>> GetBuildDependencies(int buildId) {
			string locator = string.Format("snapshotDependency:(to:(id:{0}),includeInitial:false),defaultFilter:false", buildId);
			return RequestBuilds<BuildDependenciesResponse>(locator);
		}

		public Either<string, Some<TestDetailsResponse>> GetTestDetails(string testOccurrenceId) {
			var subUrl = string.Format("testOccurrences/{0}", testOccurrenceId);
			return RequestServer<TestDetailsResponse>(subUrl);
		}

		public Either<string, Some<BuildDetailsResponse>> GetBuildDetails(int buildId) {
			var subUrl = string.Format("builds/id:{0}", buildId);
			return RequestServer<BuildDetailsResponse>(subUrl);
		}

		private Either<string, Some<TTestResponse>> RequestTests<TTestResponse>(string locator) where TTestResponse: new() {
			var subUrl = string.Format("testOccurrences?locator={0},count:25000", locator);
			return RequestServer<TTestResponse>(subUrl);
		}

		private Either<string, Some<TBuildResponse>> RequestBuilds<TBuildResponse>(string locator) where TBuildResponse: new() {
			var subUrl = string.Format("builds?locator={0},count:25000", locator);
			return RequestServer<TBuildResponse>(subUrl);
		}

		private Either<string, Some<TTestResponse>> RequestServer<TTestResponse>(string subUrl) where TTestResponse : new() {
			return RestClient.Bind(client => {
				var request = new RestRequest(subUrl);
				var response = client.Execute<TTestResponse>(request);
				if (response.StatusCode == HttpStatusCode.OK) {
					return Prelude.Right<string, Some<TTestResponse>>(Some.Create(response.Data));
				}
				return Prelude.Left<string, Some<TTestResponse>>(string.IsNullOrEmpty(response.Content) ? 
					response.ErrorMessage : response.Content);
			});
		}
	}
}
