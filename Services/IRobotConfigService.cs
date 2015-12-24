using Orchard;

namespace Berry.SEOKit.Services {
    public interface IRobotConfigService : IDependency {
        string GetRobotsText();
    }
}
