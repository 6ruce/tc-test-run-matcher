using System;
using Nancy;

namespace TestRunMatcher
{
	class ErrorHandler
	{
		public void HandlePossibleException(Func<ViewRenderer> action)
		{
			try {

			} catch (Exception) {
				
				throw;
			}
		}
	}
}
