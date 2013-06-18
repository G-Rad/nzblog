using System;
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
	        Action<StylesheetBundle> disableCssMinification = bundle =>
			{
				var index = bundle.Pipeline.IndexOf<MinifyAssets>();
				if (index >= 0)
				{
					bundle.Pipeline.RemoveAt(index);
				}
			};

			
			bundles.AddPerIndividualFile("assets/css", customizeBundle: disableCssMinification);
			bundles.AddPerIndividualFile("areas/admin/assets/css", customizeBundle: disableCssMinification);
        }
    }
}