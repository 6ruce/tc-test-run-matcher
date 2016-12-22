using System;
using LanguageExt;
using Nancy;
using TestRunMatcher.Modules.TestRunMatcher;
using TestRunMatcher.Modules.TestRunMatcher.ViewModel;

namespace TestRunMatcher.Modules
{
	public class TestRunMatcherModule : NancyModule
	{
		public TestRunMatcherModule(IRequestLogger logger, ITestMatchController testMatcherController) : base("/TestRunMatcher")
		{
			Get["/"] = _ => {
				logger.LogRequest(Request);
				return View[MainView];
			};

			Post["/check"] = _ => {
				logger.LogRequest(Request);
				var mainPageModel = new MainPageModel(Request.Form);
				Tuple<object, string> renderData;
				try {
					var result = testMatcherController.MatchTestRuns(mainPageModel.OldRunUrl, mainPageModel.NewRunUrl);
					renderData = HandleResult(mainPageModel, result);
				} catch (Exception ex) {
					renderData = HandleError(mainPageModel, ex.Message + Environment.NewLine + ex.StackTrace);
				}
				return renderData.Map((model, view) => View[view, model]);
			};
		}

		private Tuple<object, string> HandleResult(MainPageModel mainPageModel, Either<string, TestsMatchResult> result) {
			return result.Match(
				Right: matchResult => Tuple.Create(new CheckResultPageModel(matchResult) as object, CheckView),
				Left: message => HandleError(mainPageModel, message)
			);
		}

		private Tuple<object, string> HandleError(MainPageModel model, string message) {
			model.IsError = true;
			model.ErrorMessage = message;
			return Tuple.Create(model as object, MainView);
		}

		private string CheckView { get { return ModuleView("CheckResult"); } }
		private string MainView { get { return ModuleView("Main"); } }

		private string ModuleView(string viewName) {
			return "Pages/TestRunMatcher/" + viewName + ".html";
		}
	}
}
