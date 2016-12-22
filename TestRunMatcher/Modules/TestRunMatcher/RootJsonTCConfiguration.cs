using System;
using System.IO;
using LanguageExt;
using Newtonsoft.Json.Linq;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class RootJsonTCConfiguration : ITeamCityConfigurationLoader
	{
		public Either<string, TeamCityConfig> LoadConfig() {
			string configFilePath = Path.Combine("Conf","teamcity.conf.json");
			string configFileFullPath = Path.Combine(Directory.GetCurrentDirectory(), configFilePath);
			return GetFileContent(configFileFullPath).Bind(ParseConfig);
		}

		private Either<string, TeamCityConfig> GetFileContent(string filePath) {
			if (!File.Exists(filePath)) {
				return Prelude.Left<string, TeamCityConfig>(
					string.Format("Configuration file [{0}] not exists", filePath));
			}
			try {
				return File.ReadAllText(filePath);
			} catch (Exception ex) {
				return Prelude.Left<string, TeamCityConfig>(
					string.Format("Error while reading file teamcity.conf.json: {0}", ex.Message));
			}
		}

		private Either<string, TeamCityConfig> ParseConfig(string configContent) {
			try {
				JObject config = JObject.Parse(configContent);
				var url = (string)config["url"];
				var userName = (string)config["userName"];
				var password = (string)config["password"];
				if (url == null || userName == null || password == null) {
					return Prelude.Left<string, TeamCityConfig>(
						"Missing data in config teamcity.conf.json. Required fields: url, userName, password.");
				}
				return CreateConfig(url, userName, password);
			} catch (Exception e) {
				return Prelude.Left<string, TeamCityConfig>(
					string.Format("Configuration file teamcity.conf.json parsing error: {0}", e.Message));
			}
		}

		private Either<string, TeamCityConfig> CreateConfig(string url, string userName, string password) {
			return Uris.ParseUri(url).Map(uri => new TeamCityConfig(uri, userName, password));
		}
	}
}