using Berry.SEOKit.Models;
using Orchard;
using System.Collections.Generic;

namespace Berry.SEOKit.Services {
    public interface IRedirectConfigService : IDependency {
        List<RedirectRule> GetRedirectRules();
    }
}
