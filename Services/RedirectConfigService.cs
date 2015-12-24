using Berry.SEOKit.Models;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Berry.SEOKit.Services {

    [OrchardFeature("Berry.Redirects")]
    public class RedirectConfigService : IRedirectConfigService {

        const string CACHE_KEY = "Berry.SEOKit.RedirectRules";
        const string TRIGGER_KEY = "Berry.SEOKit.RedirectRulesChanged";
        private IOrchardServices _services;
        private ICacheManager _cacheManager;
        private ISignals _signals;
        

        public RedirectConfigService(
            IOrchardServices services,
            ICacheManager cacheManager,
            ISignals signals) {
            _services = services;
            _cacheManager = cacheManager;
            _signals = signals;
        }

        public List<RedirectRule> GetRedirectRules() {

            return _cacheManager.Get(CACHE_KEY, ctx => {

                    ctx.Monitor(_signals.When(TRIGGER_KEY));

                    var redirectSettings = _services.WorkContext.CurrentSite.As<RedirectConfigSettingsPart>();
                    var reader = new StringReader(redirectSettings.RedirectRules ?? "");
                    string ruleLine;
                    var rules = new List<RedirectRule>();

                    while ((ruleLine = reader.ReadLine()) != null) {

                        ruleLine = ruleLine.Trim().ToLower();

                        // Ignore comments
                        if (ruleLine.StartsWith("#")) {
                            continue;
                        }

                        // Ignore invalid rules
                        var urlPair = ruleLine.Split(',');
                        if (urlPair.Length != 2) {
                            continue;
                        }

                        // Ignore invalid urls
                        var urlToMatch = urlPair[0].Trim();
                        var redirectUrl = urlPair[1].Trim();
                        if (!Uri.IsWellFormedUriString(urlToMatch, UriKind.Relative) || !Uri.IsWellFormedUriString(redirectUrl, UriKind.Relative)) {
                            continue;
                        }

                        rules.Add(new RedirectRule { MatchUrl = urlToMatch, RedirectUrl = redirectUrl });
                    }

                    return rules;
                });
        }
    }
}