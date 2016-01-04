using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using System.Collections.Generic;

namespace Berry.SEOKit.Settings {
    public class OpenGraphSettingsHooks : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "OpenGraphPart")
                yield break;

            var model = definition.Settings.GetModel<OpenGraphPartSettings>();

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "OpenGraphPart")
                yield break;

            var model = new OpenGraphPartSettings();
            updateModel.TryUpdateModel(model, "OpenGraphPartSettings", null, null);
            builder.WithSetting("OpenGraphPartSettings.AllowTitle", model.AllowTitle.ToString());
            builder.WithSetting("OpenGraphPartSettings.AllowDescription", model.AllowDescription.ToString());
            builder.WithSetting("OpenGraphPartSettings.AllowType", model.AllowType.ToString());
            builder.WithSetting("OpenGraphPartSettings.AllowUrl", model.AllowUrl.ToString());
            builder.WithSetting("OpenGraphPartSettings.AllowImage", model.AllowImage.ToString());
            builder.WithSetting("OpenGraphPartSettings.AllowSiteName", model.AllowSiteName.ToString());
            builder.WithSetting("OpenGraphPartSettings.DefaultTitle", model.DefaultTitle);
            builder.WithSetting("OpenGraphPartSettings.DefaultDescription", model.DefaultDescription);
            builder.WithSetting("OpenGraphPartSettings.DefaultType", model.DefaultType);
            builder.WithSetting("OpenGraphPartSettings.DefaultUrl", model.DefaultUrl);
            builder.WithSetting("OpenGraphPartSettings.DefaultImage", model.DefaultImage);
            builder.WithSetting("OpenGraphPartSettings.DefaultSiteName", model.DefaultSiteName);

            yield return DefinitionTemplate(model);
        }
    }
}