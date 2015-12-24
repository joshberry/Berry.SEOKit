using Berry.SEOKit.Models;
using JetBrains.Annotations;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace Berry.SEOKit.Handlers {
    [UsedImplicitly]
    [OrchardFeature("Berry.Redirects")]
    public class RedirectConfigSettingsPartHandler : ContentHandler {

        const string TRIGGER_KEY = "Berry.SEOKit.RedirectRulesChanged";
        private readonly ISignals _signals;
        
        public RedirectConfigSettingsPartHandler(ISignals signals) {
            _signals = signals;
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<RedirectConfigSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<RedirectConfigSettingsPart>("RedirectConfigSettings", "Parts/RedirectConfigSettings", "Redirects"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Redirects")));
        }

        protected override void Updated(UpdateContentContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.Updated(context);
            _signals.Trigger(TRIGGER_KEY);
        }
    }
}