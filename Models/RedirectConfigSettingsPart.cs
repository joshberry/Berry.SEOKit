using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.Models {

    [OrchardFeature("Berry.Redirects")]
    public class RedirectConfigSettingsPart : ContentPart {
        public string RedirectRules {
            get { return this.Retrieve(x => x.RedirectRules); }
            set { this.Store(x => x.RedirectRules, value); }
        }
    }
}