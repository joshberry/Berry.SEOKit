using Berry.SEOKit.Services;
using Orchard.Environment.Extensions;
using Orchard.Logging;
using Orchard.Mvc.Filters;
using Orchard.Services;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Berry.SEOKit.Filters {
    [OrchardFeature("Berry.Redirects")]
    public class RedirectFilter : FilterProvider, IActionFilter {
        private readonly IRedirectConfigService _redirectService;

        public RedirectFilter(IRedirectConfigService redirectService) {
            _redirectService = redirectService;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext) { }

        public void OnActionExecuting(ActionExecutingContext filterContext) {

            var rules = _redirectService.GetRedirectRules();
            string requestUrl = filterContext.HttpContext.Request.Url.PathAndQuery.ToLower();
            var matchingRule = rules.FirstOrDefault(r => requestUrl.StartsWith(r.MatchUrl));

            if(matchingRule != null) {
                // Append querystring to redirect url
                var query = filterContext.HttpContext.Request.Url.Query;
                filterContext.Result = new RedirectResult(matchingRule.RedirectUrl + query, true);
            }
        }
    }
}