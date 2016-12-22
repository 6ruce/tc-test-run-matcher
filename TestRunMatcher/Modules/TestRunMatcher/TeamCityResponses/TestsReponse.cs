using System.Collections.Generic;

namespace TestRunMatcher.Modules.TestRunMatcher.TeamCityResponses
{
	public class TestsReponse
	{
	    public TestsReponse() {
	        TestOccurrence = new List<SingleTestResponse>();
	        Href = string.Empty;
	    }

		public int Count { get; set; }
		public string Href { get; set; }
		public List<SingleTestResponse> TestOccurrence { get; set; }
	}

	public class SingleTestResponse
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public int Duration { get; set; }
		public string Href { get; set; }
	}

	public class TestDetailsResponse
	{
		public string Details { get; set; }
		public SingleTestResponse Test { get; set; }
		public BuildResponse Build { get; set; }
	}

	public class BuildResponse
	{
		public string WebUrl { get; set; }
	}
}
