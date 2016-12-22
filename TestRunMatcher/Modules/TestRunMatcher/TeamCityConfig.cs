using System;
using LanguageExt;

namespace TestRunMatcher.Modules.TestRunMatcher
{
	public class TeamCityConfig
	{
		private Uri _teamCityUri;
		private Some<string> _userName;
		private Some<string> _password;

		public TeamCityConfig(Uri teamCityUri, Some<string> userName, Some<string> password) {
			_teamCityUri = teamCityUri;
			_userName = userName;
			_password = password;
		}

		public Some<string> Password {
			get { return _password; }
		}

		public Uri TeamCityUri {
			get { return _teamCityUri; }
		}

		public Some<string> UserName {
			get { return _userName; }
		}
	}
}