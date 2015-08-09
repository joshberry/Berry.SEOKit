using Orchard;

namespace Berry.SEOKit.Services {
    public interface IPageTitleService : IDependency {
        bool HasTitle();
        string GetTitle();
    }
}
