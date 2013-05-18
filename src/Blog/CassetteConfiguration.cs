using Cassette;
using Cassette.BundleProcessing;
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
			bundles.AddPerIndividualFile<ScriptBundle>("assets/js");
			
			// disable minification for css
			// https://groups.google.com/forum/?fromgroups#!topic/cassette/Kxaf4wpBDik
			bundles.AddPerIndividualFile<StylesheetBundle>("assets/css", customizeBundle: b =>
			{
				var index = b.Pipeline.IndexOf<MinifyAssets>();
				if (index >= 0)
				{
					b.Pipeline.RemoveAt(index);
				}
			});
        }
    }
}