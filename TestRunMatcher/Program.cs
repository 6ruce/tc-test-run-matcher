using System;
using System.Linq;
using Nancy;
using Nancy.Hosting.Self;
using TestRunMatcher.Modules.TestRunMatcher;

namespace TestRunMatcher
{
	class Program
	{
		static void Main(string[] args) {
			int port = args.Any() ? int.Parse(args.First()) : 6677;
			String hostName = @"http://localhost:" + port.ToString();
			var bootstrapper = new DefaultNancyBootstrapper();
			Console.WriteLine("=== Starting server ===");
			using (var host = new NancyHost(
				new Uri(hostName),
				bootstrapper,
				new HostConfiguration() {
					UrlReservations = new UrlReservations() {
						CreateAutomatically = true
					}
				})) {
				host.Start();
				Console.WriteLine("=== Server started on: " + hostName + " ===");
				while (true) { }
			}
		}
	}
}
