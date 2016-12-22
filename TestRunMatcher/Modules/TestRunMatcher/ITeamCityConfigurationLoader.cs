using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public interface ITeamCityConfigurationLoader
	{
		Either<string, TeamCityConfig> LoadConfig();
	}
}