using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplicationJedi.Startup))]
namespace WebApplicationJedi {
	public partial class Startup {
		public void Configuration(IAppBuilder app) {
			ConfigureAuth(app);
		}
	}
}
