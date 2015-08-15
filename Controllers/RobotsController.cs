using Berry.SEOKit.Services;
using Orchard.Environment.Extensions;
using System.Text;
using System.Web.Mvc;

namespace Berry.SEOKit.Controllers {
    [OrchardFeature("Berry.Robots")]
    public class RobotsController : Controller {
        private readonly IRobotConfigService _robotService;

        public RobotsController(IRobotConfigService robotService) {
            _robotService = robotService;
        }

        public ContentResult Index() {
            return new ContentResult() {
                Content = _robotService.GetRobotsText(),
                ContentType = "text/plain",
                ContentEncoding = Encoding.UTF8
            };
        }
    }
}