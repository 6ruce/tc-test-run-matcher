using System.Collections.Generic;

namespace TestRunMatcher.Modules.TestRunMatcher.TeamCityResponses
{
	public class BuildDependenciesResponse
	{
	    private List<DependentBuildResponse> _builds;

	    public List<DependentBuildResponse> Build
	    {
	        get { return _builds ?? (_builds = new List<DependentBuildResponse>()); } 
	        set { _builds = value; }
	    }
	}

	public class DependentBuildResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string State { get; set; }

	    public static int GetId(DependentBuildResponse dependentBuildResponse) {
	        return dependentBuildResponse.Id;
	    }
	}

	public class BuildDetailsResponse
	{
		public BuildType BuildType { get; set; }
	}

	public class BuildType
	{
		public string Name { get; set; }
		public string ProjectName { get; set; }
	}
}
