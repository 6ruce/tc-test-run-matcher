using Nancy;

namespace TestRunMatcher
{
	public interface IRequestLogger
	{
		void LogRequest(Request request);
	}
}
