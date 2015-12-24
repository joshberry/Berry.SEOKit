using Berry.SEOKit.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.Services {

    [OrchardFeature("Berry.Robots")]
    public class RobotConfigService : IRobotConfigService {

        private IOrchardServices _services;

        public RobotConfigService(IOrchardServices services) {
            _services = services;
        }

        public string GetRobotsText() {
            var robotSettings = _services.WorkContext.CurrentSite.As<RobotConfigSettingsPart>();
            return robotSettings.RobotsText;
        }
    }
}