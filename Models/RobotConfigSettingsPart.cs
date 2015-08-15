using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.Models {

    [OrchardFeature("Berry.Robots")]
    public class RobotConfigSettingsPart : ContentPart {
        public string RobotsText {
            get { return this.Retrieve(x => x.RobotsText); }
            set { this.Store(x => x.RobotsText, value); }
        }
    }
}