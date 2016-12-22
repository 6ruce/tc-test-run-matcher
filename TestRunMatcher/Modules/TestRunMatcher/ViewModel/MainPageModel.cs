namespace TestRunMatcher.Modules.TestRunMatcher.ViewModel
{
	class MainPageModel
	{

		public MainPageModel(dynamic form) {
			OldRunUrl = form.OldRunUrl;
			NewRunUrl = form.NewRunUrl;
		}

		public string OldRunUrl { get; set; }
		public string NewRunUrl { get; set; }

		public bool IsError { get; set; }
		public string ErrorMessage { get; set; }

		public string OldRunUrlError { get; set; }
		public string NewRunUrlError { get; set; }
	}
}
