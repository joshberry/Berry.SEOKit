using Berry.SEOKit.Models;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace Berry.SEOKit.Handlers {
    [UsedImplicitly]
    [OrchardFeature("Berry.Robots")]
    public class RobotConfigSettingsPartHandler : ContentHandler {
        public RobotConfigSettingsPartHandler() {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<RobotConfigSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<RobotConfigSettingsPart>("RobotConfigSettings", "Parts/RobotConfigSettings", "Robots"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Robots")));
        }
    }
}