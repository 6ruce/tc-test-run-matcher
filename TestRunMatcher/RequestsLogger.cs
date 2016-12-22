using System;
using Nancy;

namespace TestRunMatcher
{
	public class RequestsLogger : IRequestLogger
	{
		public void LogRequest(Request request) {
			string httpMethod = GetMethod(request);
			Console.WriteLine(string.Format("[-> {0}] {1}", httpMethod, request.Url));
		}

		private string GetMethod(Request request) {
			return request.Method;
		}
	}
} 