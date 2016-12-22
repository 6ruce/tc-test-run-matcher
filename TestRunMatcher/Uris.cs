using LanguageExt;
using System;

namespace TestRunMatcher
{
	public static class Uris
	{
		public static Either<string, Uri> ParseUri(string uriString) {
			Uri outUri;
			try {
				outUri = new Uri(uriString);
				return Prelude.Right<string, Uri>(outUri);
			} catch (Exception ex) {
				return Prelude.Left<string, Uri>(ex.Message);
			}
		}
	}
}
