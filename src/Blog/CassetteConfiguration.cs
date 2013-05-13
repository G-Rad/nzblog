using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Web
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
			bundles.AddPerSubDirectory<ScriptBundle>("assets/js");
			bundles.AddPerSubDirectory<StylesheetBundle>("assets/css");
        }
    }
}